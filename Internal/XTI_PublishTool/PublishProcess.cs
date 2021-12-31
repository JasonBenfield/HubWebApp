using Microsoft.Extensions.Hosting;
using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_GitHub;
using XTI_Hub;
using XTI_Processes;
using XTI_Secrets;
using XTI_VersionToolApi;

namespace XTI_PublishTool;

public sealed class PublishProcess
{
    private readonly IHostEnvironment hostEnv;
    private readonly XtiFolder xtiFolder;
    private readonly AppFactory appFactory;
    private readonly GitFactory gitFactory;
    private readonly ISecretCredentialsFactory credentialsFactory;
    private readonly string repoOwner;

    public PublishProcess(IHostEnvironment hostEnv, AppFactory appFactory, GitFactory gitFactory, ISecretCredentialsFactory credentialsFactory, string repoOwner)
    {
        this.hostEnv = hostEnv;
        xtiFolder = new XtiFolder(hostEnv);
        this.appFactory = appFactory;
        this.gitFactory = gitFactory;
        this.credentialsFactory = credentialsFactory;
        this.repoOwner = repoOwner;
    }

    public async Task Run(AppKey appKey, string repoOwner, string repoName)
    {
        var versionToolOutput = await getVersionKey(appKey);
        var buildVersionKey = AppVersionKey.Parse(versionToolOutput.VersionKey);
        var versionKey = buildVersionKey;
        if (hostEnv.IsProduction())
        {
            versionToolOutput = await beginPublish(appKey);
        }
        else
        {
            versionKey = AppVersionKey.Current;
        }
        string packageVersion;
        if (hostEnv.IsProduction())
        {
            packageVersion = versionToolOutput.VersionNumber;
        }
        else
        {
            packageVersion = await retrieveDevPackageVersion(appKey);
        }
        var gitHubRepo = await gitFactory.CreateGitHubRepo(repoOwner, repoName);
        GitHubRelease? release = null;
        if (!appKey.Type.Equals(AppType.Values.Package) && hostEnv.IsProduction())
        {
            var tagName = $"v{versionToolOutput.VersionNumber}";
            await gitHubRepo.DeleteReleaseIfExists(tagName);
            Console.WriteLine($"Creating release {tagName}");
            release = await gitHubRepo.CreateRelease(tagName, versionToolOutput.VersionKey, "");
        }
        var publishDir = getPublishDir(appKey, versionKey);
        if (Directory.Exists(publishDir))
        {
            Directory.Delete(publishDir, true);
        }
        await publishSetup(appKey, versionKey);
        if (appKey.Type.Equals(AppType.Values.WebApp))
        {
            await buildWebApp(appKey, buildVersionKey);
            await runDotnetBuild();
            await runDotNetPublish(appKey, versionKey);
        }
        else if (appKey.Type.Equals(AppType.Values.Service))
        {
            await runDotnetBuild();
            await runDotNetPublish(appKey, versionKey);
        }
        else
        {
            await buildWebApp(appKey, buildVersionKey);
            await runDotnetBuild();
        }
        await new PublishNpmProcess(hostEnv, xtiFolder).Run(appKey, versionKey, packageVersion);
        if (!appKey.Type.Equals(AppType.Values.Package) && hostEnv.IsProduction())
        {
            if (release == null) { throw new Exception("Release not found"); }
            await uploadReleaseAssets(appKey, versionKey, gitHubRepo, release);
            var versionsPath = Path.Combine(publishDir, "versions.json");
            if (File.Exists(versionsPath))
            {
                Console.WriteLine("Uploading versions.json");
                using var versionStream = new MemoryStream(File.ReadAllBytes(versionsPath));
                await gitHubRepo.UploadReleaseAsset(release, new FileUpload(versionStream, "versions.json", "text/plain"));
            }
            Console.WriteLine($"Finalizing release {release.TagName}");
            await gitHubRepo.FinalizeRelease(release);
        }
        await packLibProjects(packageVersion);
        await completeVersion(appKey, repoOwner, repoName);
    }

    private async Task uploadReleaseAssets(AppKey appKey, AppVersionKey versionKey, XtiGitHubRepository gitHubRepo, GitHubRelease release)
    {
        Console.WriteLine("Uploading app.zip");
        var publishDir = getPublishDir(appKey, versionKey);
        var appZipPath = Path.Combine(publishDir, "app.zip");
        if (File.Exists(appZipPath))
        {
            File.Delete(appZipPath);
        }
        ZipFile.CreateFromDirectory(Path.Combine(publishDir, "App"), appZipPath);
        using (var appStream = new MemoryStream(File.ReadAllBytes(appZipPath)))
        {
            appStream.Seek(0, SeekOrigin.Begin);
            await gitHubRepo.UploadReleaseAsset(release, new FileUpload(appStream, "app.zip", "application/zip"));
        }
        var publishSetupDir = Path.Combine(publishDir, "Setup");
        if (Directory.Exists(publishSetupDir))
        {
            Console.WriteLine("Uploading setup.zip");
            var setupZipPath = Path.Combine(publishDir, "setup.zip");
            if (File.Exists(setupZipPath))
            {
                File.Delete(setupZipPath);
            }
            ZipFile.CreateFromDirectory(publishSetupDir, setupZipPath);
            using (var setupStream = new MemoryStream(File.ReadAllBytes(setupZipPath)))
            {
                setupStream.Seek(0, SeekOrigin.Begin);
                await gitHubRepo.UploadReleaseAsset(release, new FileUpload(setupStream, "setup.zip", "application/zip"));
            }
        }
    }

    public async Task RunInstall(AppKey appKey, string destinationMachine)
    {
        Console.WriteLine("Installing");
        var installPath = Path.Combine
        (
            xtiFolder.ToolsPath(),
            "InstallApp",
            "InstallApp.exe"
        );
        var result = await new XtiProcess(installPath)
            .WriteOutputToConsole()
            .UseEnvironment(hostEnv.EnvironmentName)
            .AddConfigOptions
            (
                new
                {
                    AppName = appKey.Name.DisplayText,
                    AppType = appKey.Type.DisplayText,
                    DestinationMachine = destinationMachine
                }
            )
            .Run();
        result.EnsureExitCodeIsZero();
    }

    private async Task buildWebApp(AppKey appKey, AppVersionKey versionKey)
    {
        var builder = new BuildWebProcess(appKey, versionKey);
        await builder.Build();
    }

    private async Task publishSetup(AppKey appKey, AppVersionKey versionKey)
    {
        Console.WriteLine("Publishing setup");
        var setupAppDir = Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{getAppName(appKey)}SetupApp"
        );
        if (Directory.Exists(setupAppDir))
        {
            var publishDir = getPublishDir(appKey, versionKey);
            var versionsPath = Path.Combine(publishDir, "versions.json");
            var persistedVersions = new PersistedVersions(appFactory, appKey, versionsPath);
            await persistedVersions.Store();
            var publishSetupDir = Path.Combine(publishDir, "Setup");
            Console.WriteLine($"Publishing setup to '{publishSetupDir}'");
            var publishProcess = new WinProcess("dotnet")
                .WriteOutputToConsole()
                .UseArgumentNameDelimiter("")
                .AddArgument("publish")
                .AddArgument(new Quoted(setupAppDir))
                .UseArgumentNameDelimiter("-")
                .AddArgument("c", getConfiguration())
                .UseArgumentValueDelimiter("=")
                .AddArgument("p:PublishProfile", "Default")
                .AddArgument("p:PublishDir", publishSetupDir);
            var result = await publishProcess.Run();
            result.EnsureExitCodeIsZero();
        }
        else
        {
            Console.WriteLine($"Setup App Not Found at '{setupAppDir}'");
        }
    }

    private async Task<VersionToolOutput> getVersionKey(AppKey appKey)
    {
        var versionOptions = new VersionToolOptions();
        versionOptions.CommandGetVersion();
        versionOptions.AppName = appKey.Name.Value;
        versionOptions.AppType = appKey.Type.DisplayText;
        var result = await runVersionTool(versionOptions);
        var output = result.Data<VersionToolOutput>();
        return output;
    }

    private async Task<VersionToolOutput> beginPublish(AppKey appKey)
    {
        Console.WriteLine("Begin Publishing");
        var versionOptions = new VersionToolOptions();
        versionOptions.CommandBeginPublish(appKey.Name.Value, appKey.Type.DisplayText);
        var result = await runVersionTool(versionOptions);
        var output = result.Data<VersionToolOutput>();
        return output;
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
            .AddArgument("p:PublishDir", publishAppDir);
        var result = await publishProcess.Run();
        result.EnsureExitCodeIsZero();
    }

    private string getPublishDir(AppKey appKey, AppVersionKey versionKey)
    {
        if (!hostEnv.IsProduction())
        {
            versionKey = AppVersionKey.Current;
        }
        return xtiFolder.PublishPath(appKey, versionKey);
    }

    private static async Task runDotnetBuild()
    {
        var result = await new WinProcess("dotnet")
              .WriteOutputToConsole()
              .UseArgumentNameDelimiter("")
              .AddArgument("build")
              .Run();
        result.EnsureExitCodeIsZero();
    }

    private static string getProjectDir(AppKey appKey)
    {
        string appType = getAppType(appKey);
        return Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{getAppName(appKey)}{appType}"
        );
    }

    private static string getAppName(AppKey appKey)
        => appKey.Name.DisplayText.Replace(" ", "");

    private static string getAppType(AppKey appKey)
    {
        var appType = appKey.Type.DisplayText.Replace(" ", "");
        if (appKey.Type.Equals(AppType.Values.Service))
        {
            appType = "ServiceApp";
        }
        return appType;
    }

    private async Task packLibProjects(string packageVersion)
    {
        Console.WriteLine("Packing Lib Projects");
        var libDir = Path.Combine(Environment.CurrentDirectory, "Lib");
        if (Directory.Exists(libDir))
        {
            string outputPath;
            var envName = hostEnv.IsProduction()
                ? "Production"
                : "Development";
            outputPath = Path.Combine
            (
                xtiFolder.FolderPath(),
                "Packages",
                envName
            );
            var credentials = credentialsFactory.Create("GitHub");
            var credentialsValue = await credentials.Value();
            foreach (var dir in Directory.GetDirectories(libDir))
            {
                var packProcess = new WinProcess("dotnet")
                    .WriteOutputToConsole()
                    .UseArgumentNameDelimiter("")
                    .AddArgument("pack")
                    .AddArgument(dir)
                    .UseArgumentNameDelimiter("-")
                    .AddArgument("c", getConfiguration())
                    .AddArgument("o", new Quoted(outputPath))
                    .UseArgumentValueDelimiter("=")
                    .AddArgument("p:PackageVersion", packageVersion);
                if (!hostEnv.IsProduction())
                {
                    packProcess
                        .UseArgumentNameDelimiter("--")
                        .AddArgument("include-source")
                        .AddArgument("include-symbols");
                }
                var packResult = await packProcess.Run();
                packResult.EnsureExitCodeIsZero();
                if (hostEnv.IsProduction())
                {
                    var publishProcess = new WinProcess("dotnet")
                        .WriteOutputToConsole()
                        .UseArgumentNameDelimiter("")
                        .AddArgument("nuget")
                        .AddArgument("push")
                        .AddArgument(new Quoted(Path.Combine(outputPath, $"{new DirectoryInfo(dir).Name}.{packageVersion}.nupkg")))
                        .UseArgumentNameDelimiter("--")
                        .AddArgument("api-key", credentialsValue.Password)
                        .AddArgument("source", $"https://nuget.pkg.github.com/{repoOwner}/index.json");
                    var publishResult = await publishProcess.Run();
                    publishResult.EnsureExitCodeIsZero();
                }
            }
        }
    }

    private string getConfiguration()
    {
        return hostEnv.IsProduction()
            ? "Release"
            : "Debug";
    }

    private async Task<string> retrieveDevPackageVersion(AppKey appKey)
    {
        var currentVersion = await retrieveCurrentVersion(appKey);
        return $"{currentVersion.DevVersionNumber}-dev{DateTime.Now:yyMMddHHmmssfff}";
    }

    private async Task<VersionToolOutput> retrieveCurrentVersion(AppKey appKey)
    {
        var versionOptions = new VersionToolOptions();
        versionOptions.CommandGetCurrentVersion(appKey.Name.Value, appKey.Type.DisplayText);
        var versionResult = await runVersionTool(versionOptions);
        var currentVersion = versionResult.Data<VersionToolOutput>();
        return currentVersion;
    }

    private async Task completeVersion(AppKey appKey, string repoOwner, string repoName)
    {
        if (hostEnv.IsProduction())
        {
            var versionOptions = new VersionToolOptions();
            versionOptions.CommandCompleteVersion(repoOwner, repoName, appKey.Name.Value, appKey.Type.DisplayText);
            await runVersionTool(versionOptions);
        }
    }

    private async Task<WinProcessResult> runVersionTool(VersionToolOptions options)
    {
        var path = Path.Combine
        (
            xtiFolder.ToolsPath(),
            "XTI_VersionTool",
            "XTI_VersionTool.exe"
        );
        var result = await new XtiProcess(path)
            .UseProductionEnvironment()
            .WriteOutputToConsole()
            .AddConfigOptions(options)
            .Run();
        result.EnsureExitCodeIsZero();
        return result;
    }
}