using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_GitHub;
using XTI_HubAppApi;
using XTI_Processes;
using XTI_VersionToolApi;

namespace XTI_PublishTool
{
    public sealed class PublishProcess
    {
        private readonly IHostEnvironment hostEnv;
        private readonly XtiFolder xtiFolder;
        private readonly HubAppApi hubApi;
        private readonly GitFactory gitFactory;

        public PublishProcess(IHostEnvironment hostEnv, HubAppApi hubApi, GitFactory gitFactory)
        {
            this.hostEnv = hostEnv;
            xtiFolder = new XtiFolder(hostEnv);
            this.hubApi = hubApi;
            this.gitFactory = gitFactory;
        }

        public async Task Run(AppKey appKey, string appsToImport, string repoOwner, string repoName)
        {
            var versionToolOutput = await getVersionKey(appKey);
            var versionKey = AppVersionKey.Parse(versionToolOutput.VersionKey);
            if (hostEnv.IsProduction())
            {
                versionToolOutput = await beginPublish(appKey);
            }
            else
            {
                versionKey = AppVersionKey.Current;
            }
            var gitHubRepo = await gitFactory.CreateGitHubRepo(repoOwner, repoName);
            GitHubRelease release = null;
            if (!appKey.Type.Equals(AppType.Values.Package) && hostEnv.IsProduction())
            {
                var tagName = $"v{versionToolOutput.VersionNumber}";
                release = await gitHubRepo.Release(tagName);
                if (release != null)
                {
                    foreach (var asset in release.Assets)
                    {
                        Console.WriteLine($"Deleting release {release.TagName} asset {asset.ID}");
                        await gitHubRepo.DeleteReleaseAsset(asset);
                    }
                    await gitHubRepo.DeleteRelease(release);
                }
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
                await buildWebApp(appKey, versionKey, appsToImport);
                await runDotNetPublish(appKey, versionKey);
                await copyToWebExports(appKey, versionKey);
            }
            else if (appKey.Type.Equals(AppType.Values.Service))
            {
                await runDotnetBuild();
                await runDotNetPublish(appKey, versionKey);
            }
            else
            {
                await runDotnetBuild();
            }
            if (!appKey.Type.Equals(AppType.Values.Package) && hostEnv.IsProduction())
            {
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
            await packLibProjects(appKey, versionToolOutput.VersionNumber);
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
                getXtiDir(),
                "Tools",
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

        private async Task buildWebApp(AppKey appKey, AppVersionKey versionKey, string appsToImport)
        {
            var builder = new BuildWebProcess(appKey, versionKey, hostEnv, appsToImport);
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
                var persistedVersions = new PersistedVersions(hubApi, appKey, versionsPath);
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

        private async Task copyToWebExports(AppKey appKey, AppVersionKey versionKey)
        {
            Console.WriteLine("Copying to web exports");
            var publishDir = getPublishDir(appKey, versionKey);
            var exportBaseDir = Path.Combine(publishDir, "Exports");
            var tempExportDir = Path.Combine(getProjectDir(appKey), "Exports");
            if (Directory.Exists(tempExportDir))
            {
                Directory.Delete(tempExportDir, true);
            }
            var sourceScriptPath = Path.Combine
            (
                getProjectDir(appKey),
                "Scripts",
                getAppName(appKey)
            );
            if (Directory.Exists(sourceScriptPath))
            {
                var tsConfigPath = Path.Combine
                (
                    getProjectDir(appKey),
                    "Scripts",
                    getAppName(appKey),
                    "tsConfig.json"
                );
                var tscProcess = new WinProcess("tsc")
                    .UseArgumentNameDelimiter("-")
                    .AddArgument("p", tsConfigPath)
                    .UseArgumentNameDelimiter("--")
                    .AddArgument("outDir", new Quoted(tempExportDir))
                    .AddArgument("declaration", "true");
                var tscResult = await new CmdProcess(tscProcess).Run();
                tscResult.EnsureExitCodeIsZero();
                await new RobocopyProcess(sourceScriptPath, tempExportDir)
                    .Pattern("*.d.ts")
                    .CopySubdirectoriesIncludingEmpty()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
                await new RobocopyProcess(sourceScriptPath, tempExportDir)
                    .Pattern("*.html")
                    .CopySubdirectoriesIncludingEmpty()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
                await new RobocopyProcess(sourceScriptPath, tempExportDir)
                    .Pattern("*.scss")
                    .CopySubdirectoriesIncludingEmpty()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
                var exportScriptDir = Path.Combine(exportBaseDir, "Scripts");
                await new RobocopyProcess(tempExportDir, exportScriptDir)
                    .CopySubdirectoriesIncludingEmpty()
                    .Purge()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
                Directory.Delete(tempExportDir, true);
            }
            else
            {
                Console.WriteLine($"Source script folder not found '{sourceScriptPath}'");
            }
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

        private async Task packLibProjects(AppKey appKey, string versionNumber)
        {
            Console.WriteLine("Packing Lib Projects");
            var libDir = Path.Combine(Environment.CurrentDirectory, "Lib");
            if (Directory.Exists(libDir))
            {
                string packageVersion;
                string outputPath;
                var envName = hostEnv.IsProduction()
                    ? "Production"
                    : "Development";
                if (hostEnv.IsProduction())
                {
                    packageVersion = versionNumber;
                }
                else
                {
                    packageVersion = await retrieveDevPackageVersion(appKey);
                }
                outputPath = Path.Combine
                (
                    getXtiDir(),
                    "Packages",
                    envName
                );
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
                    var result = await packProcess.Run();
                    result.EnsureExitCodeIsZero();
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

        private static string getXtiDir() => Environment.GetEnvironmentVariable("XTI_Dir");

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
                getXtiDir(),
                "Tools",
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
}
