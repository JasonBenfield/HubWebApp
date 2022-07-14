using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_GitHub;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class PublishProcess
{
    private readonly Scopes scopes;

    internal PublishProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run()
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var appKeys = scopes.GetRequiredService<SlnFolder>().AppKeys();
        if (!appKeys.Any())
        {
            throw new ArgumentException("App keys are required");
        }
        var versionKey = xtiEnv.IsProduction()
            ? new VersionKeyFromCurrentBranch(scopes).Value()
            : AppVersionKey.Current;
        string semanticVersion;
        var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
        if (xtiEnv.IsProduction())
        {
            var version = await new BeginPublishProcess(scopes).Run();
            semanticVersion = version.VersionNumber.Format();
            Console.WriteLine($"Publishing Version '{semanticVersion}'");
        }
        else
        {
            var currentVersion = await new CurrentVersion(scopes, versionName).Value();
            semanticVersion = currentVersion.NextPatch().FormatAsDev();
        }
        var release = await createGitHubRelease(versionKey, semanticVersion);
        var publishFolder = scopes.GetRequiredService<PublishedFolder>();
        var slnDir = Environment.CurrentDirectory;
        foreach (var appKey in appKeys)
        {
            setCurrentDirectory(slnDir, appKey);
            var publishDir = getPublishDir(appKey, versionKey);
            if (Directory.Exists(publishDir))
            {
                Directory.Delete(publishDir, true);
            }
            await new PublishSetupProcess(scopes).Run(appKey, versionKey);
            await new PublishToolsProcess(scopes).Run(appKey, versionKey);
            if (!appKey.Type.Equals(AppType.Values.Package))
            {
                await runDotNetPublish(appKey, versionKey);
            }
            var xtiFolder = scopes.GetRequiredService<XtiFolder>();
            await new PublishNpmProcess(xtiEnv, xtiFolder, publishFolder).Run(appKey, versionKey, semanticVersion);
            if (!appKey.Type.Equals(AppType.Values.Package) && !appKey.Type.Equals(AppType.Values.WebPackage) && release != null)
            {
                await uploadReleaseAssets(appKey, versionKey, release);
            }
            await new PublishLibProcess(scopes).Run(semanticVersion);
            Environment.CurrentDirectory = slnDir;
        }
        if (xtiEnv.IsProduction())
        {
            await new CompleteVersionProcess(scopes).Run();
        }
        await publishVersions(release);
        if (xtiEnv.IsProduction() && release != null)
        {
            Console.WriteLine($"Finalizing release {release.TagName}");
            var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
            await gitHubRepo.FinalizeRelease(release);
        }
    }

    private async Task<GitHubRelease?> createGitHubRelease(AppVersionKey versionKey, string semanticVersion)
    {
        var appKeys = scopes.GetRequiredService<SlnFolder>().AppKeys();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        GitHubRelease? release = null;
        if (appKeys.Any(appKey => !appKey.Type.Equals(AppType.Values.Package)) && xtiEnv.IsProduction())
        {
            var tagName = $"v{semanticVersion}";
            await gitHubRepo.DeleteReleaseIfExists(tagName);
            Console.WriteLine($"Creating release {tagName}");
            release = await gitHubRepo.CreateRelease(tagName, versionKey.DisplayText, "");
        }
        return release;
    }

    private async Task publishVersions(GitHubRelease? release)
    {
        var appKeys = scopes.GetRequiredService<SlnFolder>().AppKeys();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var publishFolder = scopes.GetRequiredService<PublishedFolder>();
        var versionsPath = publishFolder.VersionsPath();
        if (appKeys.Any(appKey => !appKey.Type.Equals(AppType.Values.Package)))
        {
            publishFolder.TryCreateVersionDir();
            if (File.Exists(versionsPath)) { File.Delete(versionsPath); }
            var persistedVersions = new PersistedVersions(versionsPath);
            var hubAdmin = scopes.Production().GetRequiredService<IHubAdministration>();
            var versions = await hubAdmin.Versions(scopes.GetRequiredService<AppVersionNameAccessor>().Value);
            await persistedVersions.Store(versions);
        }
        if (release != null)
        {
            Console.WriteLine("Uploading versions.json");
            using var versionStream = new MemoryStream(File.ReadAllBytes(versionsPath));
            var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
            await gitHubRepo.UploadReleaseAsset(release, new FileUpload(versionStream, "versions.json", "text/plain"));
        }
    }

    private static void setCurrentDirectory(string slnDir, AppKey appKey)
    {
        var projectDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
        if (Directory.Exists(projectDir))
        {
            Environment.CurrentDirectory = projectDir;
        }
    }

    private async Task uploadReleaseAssets(AppKey appKey, AppVersionKey versionKey, GitHubRelease release)
    {
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        Console.WriteLine("Uploading app.zip");
        var publishDir = getPublishDir(appKey, versionKey);
        var appKeyText = $"{appKey.Name.DisplayText}{appKey.Type.DisplayText}".Replace(" ", "");
        var appZipPath = Path.Combine(publishDir, $"{appKeyText}.zip");
        if (File.Exists(appZipPath))
        {
            File.Delete(appZipPath);
        }
        ZipFile.CreateFromDirectory(Path.Combine(publishDir, "App"), appZipPath);
        using (var appStream = new MemoryStream(File.ReadAllBytes(appZipPath)))
        {
            appStream.Seek(0, SeekOrigin.Begin);
            await gitHubRepo.UploadReleaseAsset(release, new FileUpload(appStream, $"{appKeyText}.zip", "application/zip"));
        }
        var publishSetupDir = Path.Combine(publishDir, "Setup");
        if (Directory.Exists(publishSetupDir))
        {
            Console.WriteLine("Uploading setup.zip");
            var setupZipPath = Path.Combine(publishDir, $"{appKeyText}Setup.zip");
            if (File.Exists(setupZipPath))
            {
                File.Delete(setupZipPath);
            }
            ZipFile.CreateFromDirectory(publishSetupDir, setupZipPath);
            using (var setupStream = new MemoryStream(File.ReadAllBytes(setupZipPath)))
            {
                setupStream.Seek(0, SeekOrigin.Begin);
                await gitHubRepo.UploadReleaseAsset(release, new FileUpload(setupStream, $"{appKeyText}Setup.zip", "application/zip"));
            }
        }
        var toolsPath = Path.Combine(publishDir, "Tools");
        if (Directory.Exists(toolsPath))
        {
            var toolsZipPath = Path.Combine(publishDir, $"{appKeyText}Tools.zip");
            if (File.Exists(toolsZipPath))
            {
                File.Delete(toolsZipPath);
            }
            ZipFile.CreateFromDirectory(toolsPath, toolsZipPath);
            using (var toolsStream = new MemoryStream(File.ReadAllBytes(toolsZipPath)))
            {
                toolsStream.Seek(0, SeekOrigin.Begin);
                await gitHubRepo.UploadReleaseAsset(release, new FileUpload(toolsStream, $"{appKeyText}Tools.zip", "application/zip"));
            }
        }
        var psPath = Path.Combine(publishDir, "Powershell");
        if (Directory.Exists(psPath))
        {
            var psZipPath = Path.Combine(publishDir, $"{appKeyText}Powershell.zip");
            if (File.Exists(psZipPath))
            {
                File.Delete(psZipPath);
            }
            ZipFile.CreateFromDirectory(psPath, psZipPath);
            using (var toolsStream = new MemoryStream(File.ReadAllBytes(psZipPath)))
            {
                toolsStream.Seek(0, SeekOrigin.Begin);
                await gitHubRepo.UploadReleaseAsset(release, new FileUpload(toolsStream, $"{appKeyText}Powershell.zip", "application/zip"));
            }
        }
    }

    private async Task runDotNetPublish(AppKey appKey, AppVersionKey versionKey)
    {
        var publishDir = getPublishDir(appKey, versionKey);
        var publishAppDir = Path.Combine(publishDir, "App");
        Console.WriteLine($"Publishing web app to '{publishAppDir}'");
        var publishProcess = new WinProcess("dotnet")
            .WriteOutputToConsole()
            .UseArgumentNameDelimiter("")
            .AddArgument("publish")
            .AddArgument(new Quoted(getProjectDir(appKey)))
            .UseArgumentNameDelimiter("-")
            .AddArgument("c", getConfiguration())
            .UseArgumentValueDelimiter("=")
            .AddArgument("p:PublishProfile", "Default")
            .AddArgument("p:PublishDir", publishAppDir)
            .AddArgument("p:TypeScriptCompileBlocked", "true");
        var result = await publishProcess.Run();
        result.EnsureExitCodeIsZero();
        var privateAppSettings = Path.Combine(publishDir, "appsettings.private.json");
        if (File.Exists(privateAppSettings))
        {
            File.Delete(privateAppSettings);
        }
    }

    private string getPublishDir(AppKey appKey, AppVersionKey versionKey) =>
        scopes.GetRequiredService<PublishedFolder>().AppDir(appKey, versionKey);

    private static string getProjectDir(AppKey appKey)
    {
        return Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            new AppDirectoryName(appKey).Value
        );
    }

    private string getConfiguration()
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        return xtiEnv.IsProduction()
            ? "Release"
            : "Debug";
    }
}