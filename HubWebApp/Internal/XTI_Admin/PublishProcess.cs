using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Git;
using XTI_GitHub;
using XTI_Hub;
using XTI_Processes;

namespace XTI_Admin;

public sealed class PublishProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly SlnFolder slnFolder;
    private readonly AdminOptions options;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly CurrentVersion currentVersionAccessor;
    private readonly IXtiGitRepository gitRepo;
    private readonly BeginPublishProcess beginPublishProcess;
    private readonly PublishedFolder publishFolder;
    private readonly PublishLibProcess publishLibProcess;
    private readonly PublishSetupProcess publishSetupProcess;
    private readonly ProductionHubAdmin prodHubAdmin;
    private readonly CompleteVersionProcess completeVersionProcess;

    public PublishProcess(XtiEnvironment xtiEnv, XtiFolder xtiFolder, SlnFolder slnFolder, AdminOptions options, AppVersionNameAccessor versionNameAccessor, XtiGitHubRepository gitHubRepo, CurrentVersion currentVersionAccessor, IXtiGitRepository gitRepo, BeginPublishProcess beginPublishProcess, PublishedFolder publishFolder, PublishLibProcess publishLibProcess, PublishSetupProcess publishSetupProcess, ProductionHubAdmin prodHubAdmin, CompleteVersionProcess completeVersionProcess)
    {
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.slnFolder = slnFolder;
        this.options = options;
        this.versionNameAccessor = versionNameAccessor;
        this.gitHubRepo = gitHubRepo;
        this.currentVersionAccessor = currentVersionAccessor;
        this.gitRepo = gitRepo;
        this.beginPublishProcess = beginPublishProcess;
        this.publishFolder = publishFolder;
        this.publishLibProcess = publishLibProcess;
        this.publishSetupProcess = publishSetupProcess;
        this.prodHubAdmin = prodHubAdmin;
        this.completeVersionProcess = completeVersionProcess;
    }

    public async Task Run(CancellationToken ct)
    {
        var appKeys = slnFolder.AppKeys();
        if (!appKeys.Any())
        {
            throw new ArgumentException("App keys are required");
        }
        var versionKey = xtiEnv.IsProduction()
            ? new VersionKeyFromCurrentBranch(gitRepo).Value()
            : AppVersionKey.Current;
        string semanticVersion;
        if (xtiEnv.IsProduction())
        {
            var version = await beginPublishProcess.Run(ct);
            semanticVersion = version.VersionNumber.Format();
            Console.WriteLine($"Publishing Version '{semanticVersion}'");
        }
        else
        {
            var currentVersion = await currentVersionAccessor.Value(ct);
            semanticVersion = currentVersion.NextPatch().FormatAsDev();
        }
        var release = await CreateGitHubRelease(versionKey, semanticVersion);
        var slnDir = Environment.CurrentDirectory;
        foreach (var appKey in appKeys)
        {
            SetCurrentDirectory(slnDir, appKey);
            var publishDir = GetPublishDir(appKey, versionKey);
            if (Directory.Exists(publishDir))
            {
                Directory.Delete(publishDir, true);
            }
            await publishSetupProcess.Run(appKey, versionKey);
            await new PublishToolsProcess(publishFolder).Run(appKey, versionKey);
            if (!appKey.Type.Equals(AppType.Values.Package))
            {
                await RunDotNetPublish(appKey, versionKey);
            }
            await new PublishNpmProcess(xtiEnv, xtiFolder, publishFolder).Run(appKey, versionKey, semanticVersion);
            if (!appKey.Type.Equals(AppType.Values.Package) && !appKey.Type.Equals(AppType.Values.WebPackage) && release != null)
            {
                await UploadReleaseAssets(appKey, versionKey, release);
            }
            await publishLibProcess.Run(appKey, versionKey, semanticVersion);
            Environment.CurrentDirectory = slnDir;
        }
        if (xtiEnv.IsProduction())
        {
            await completeVersionProcess.Run(ct);
        }
        await PublishVersions(release, ct);
        if (xtiEnv.IsProduction() && release != null)
        {
            Console.WriteLine($"Finalizing release {release.TagName}");
            await gitHubRepo.FinalizeRelease(release);
            options.VersionNumber = semanticVersion;
        }
    }

    private async Task<GitHubRelease?> CreateGitHubRelease(AppVersionKey versionKey, string semanticVersion)
    {
        var appKeys = slnFolder.AppKeys();
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

    private async Task PublishVersions(GitHubRelease? release, CancellationToken ct)
    {
        var appKeys = slnFolder.AppKeys();
        var versionsPath = publishFolder.VersionsPath();
        if (appKeys.Any(appKey => !appKey.Type.Equals(AppType.Values.Package)))
        {
            publishFolder.TryCreateVersionDir();
            if (File.Exists(versionsPath)) { File.Delete(versionsPath); }
            var persistedVersions = new PersistedVersions(versionsPath);
            var versions = await prodHubAdmin.Value.Versions(versionNameAccessor.Value, ct);
            await persistedVersions.Store(versions);
        }
        if (release != null)
        {
            Console.WriteLine("Uploading versions.json");
            using var versionStream = new MemoryStream(File.ReadAllBytes(versionsPath));
            await gitHubRepo.UploadReleaseAsset(release, new GitHubFileUpload(versionStream, "versions.json", "text/plain"));
        }
    }

    private static void SetCurrentDirectory(string slnDir, AppKey appKey)
    {
        var projectDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
        if (Directory.Exists(projectDir))
        {
            Environment.CurrentDirectory = projectDir;
        }
    }

    private async Task UploadReleaseAssets(AppKey appKey, AppVersionKey versionKey, GitHubRelease release)
    {
        Console.WriteLine("Uploading app.zip");
        var publishDir = GetPublishDir(appKey, versionKey);
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
            await gitHubRepo.UploadReleaseAsset(release, new GitHubFileUpload(appStream, $"{appKeyText}.zip", "application/zip"));
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
                await gitHubRepo.UploadReleaseAsset(release, new GitHubFileUpload(setupStream, $"{appKeyText}Setup.zip", "application/zip"));
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
                await gitHubRepo.UploadReleaseAsset(release, new GitHubFileUpload(toolsStream, $"{appKeyText}Tools.zip", "application/zip"));
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
                await gitHubRepo.UploadReleaseAsset(release, new GitHubFileUpload(toolsStream, $"{appKeyText}Powershell.zip", "application/zip"));
            }
        }
    }

    private async Task RunDotNetPublish(AppKey appKey, AppVersionKey versionKey)
    {
        var publishDir = GetPublishDir(appKey, versionKey);
        var publishAppDir = Path.Combine(publishDir, "App");
        Console.WriteLine($"Publishing web app to '{publishAppDir}'");
        var publishProcess = new WinProcess("dotnet")
            .WriteOutputToConsole()
            .UseArgumentNameDelimiter("")
            .AddArgument("publish")
            .AddArgument(new Quoted(GetProjectDir(appKey)))
            .UseArgumentNameDelimiter("-")
            .AddArgument("c", GetConfiguration())
            .UseArgumentValueDelimiter("=")
            .AddArgument("p:PublishProfile", "Default")
            .AddArgument("p:PublishDir", publishAppDir)
            .AddArgument("p:TypeScriptCompileBlocked", "true");
        var result = await publishProcess.Run();
        result.EnsureExitCodeIsZero();
        var privateFiles = Directory.GetFiles(publishAppDir, "*.private.*")
            .Where(f => !f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase));
        foreach (var privateFile in privateFiles)
        {
            File.Delete(privateFile);
        }
    }

    private string GetPublishDir(AppKey appKey, AppVersionKey versionKey) =>
        publishFolder.AppDir(appKey, versionKey);

    private static string GetProjectDir(AppKey appKey) =>
        Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            new AppDirectoryName(appKey).Value
        );

    private string GetConfiguration() =>
        xtiEnv.IsProduction() ? "Release" : "Debug";
}