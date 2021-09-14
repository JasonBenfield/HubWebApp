using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_HubAppApi;
using XTI_Processes;
using XTI_PublishTool;
using XTI_VersionToolApi;

namespace PublishApp
{
    public sealed class PublishHostedService : IHostedService
    {
        private readonly IServiceProvider services;

        public PublishHostedService(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;
            try
            {
                var options = sp.GetService<IOptions<PublishOptions>>().Value;
                var versionToolOutput = await getVersionKey(sp);
                var versionKey = AppVersionKey.Parse(versionToolOutput.VersionKey);
                var appKey = new AppKey(new AppName(options.AppName), AppType.Values.Value(options.AppType));
                var hostEnv = sp.GetService<IHostEnvironment>();
                if (hostEnv.IsProduction())
                {
                    versionToolOutput = await beginPublish(appKey);
                }
                else
                {
                    versionKey = AppVersionKey.Current;
                }
                var publishDir = getPublishDir(sp, appKey, versionKey);
                if (Directory.Exists(publishDir))
                {
                    Directory.Delete(publishDir, true);
                }
                await publishSetup(sp, appKey, versionKey);
                if (appKey.Type.Equals(AppType.Values.WebApp))
                {
                    await buildWebApp(sp, versionKey, appKey);
                    await runDotNetPublish(sp, appKey, versionKey);
                    await copyToWebExports(sp, appKey, versionKey);
                }
                else if (appKey.Type.Equals(AppType.Values.Service))
                {
                    await runDotnetBuild();
                    await runDotNetPublish(sp, appKey, versionKey);
                }
                else
                {
                    await runDotnetBuild();
                }
                await packLibProjects(sp, versionToolOutput.VersionNumber);
                await completeVersion(sp, appKey);
                await runInstall(sp, appKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.ExitCode = 999;
            }
            var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
            lifetime.StopApplication();
        }

        private async Task buildWebApp(IServiceProvider sp, AppVersionKey versionKey, AppKey appKey)
        {
            var hostEnv = sp.GetService<IHostEnvironment>();
            var options = sp.GetService<IOptions<PublishOptions>>().Value;
            var builder = new BuildWebProcess(appKey, versionKey, hostEnv, options.AppsToImport);
            await builder.Build();
        }

        private async Task publishSetup(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
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
                var hubApi = sp.GetService<HubAppApi>();
                var publishDir = getPublishDir(sp, appKey, versionKey);
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
                    .AddArgument("c", getConfiguration(sp))
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

        private async Task<VersionToolOutput> getVersionKey(IServiceProvider sp)
        {
            var versionOptions = new VersionToolOptions();
            versionOptions.CommandGetVersion();
            var options = sp.GetService<IOptions<PublishOptions>>().Value;
            versionOptions.AppName = options.AppName;
            versionOptions.AppType = options.AppType;
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

        private async Task runDotNetPublish(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
        {
            var publishDir = getPublishDir(sp, appKey, versionKey);
            var publishAppDir = Path.Combine(publishDir, "App");
            Console.WriteLine($"Publishing web app to '{publishAppDir}'");
            var publishProcess = new WinProcess("dotnet")
                .WriteOutputToConsole()
                .UseArgumentNameDelimiter("")
                .AddArgument("publish")
                .AddArgument(new Quoted(getProjectDir(appKey)))
                .UseArgumentNameDelimiter("-")
                .AddArgument("c", getConfiguration(sp))
                .UseArgumentValueDelimiter("=")
                .AddArgument("p:PublishProfile", "Default")
                .AddArgument("p:PublishDir", publishAppDir);
            var result = await publishProcess.Run();
            result.EnsureExitCodeIsZero();
        }

        private async Task copyToWebExports(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
        {
            Console.WriteLine("Copying to web exports");
            var publishDir = getPublishDir(sp, appKey, versionKey);
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
            var sourceViewDir = Path.Combine
            (
                getProjectDir(appKey),
                "Views",
                "Exports",
                getAppName(appKey)
            );
            if (Directory.Exists(sourceViewDir))
            {
                var exportViewDir = Path.Combine(exportBaseDir, "Views");
                await new RobocopyProcess(sourceViewDir, exportViewDir)
                    .CopySubdirectoriesIncludingEmpty()
                    .Purge()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
            }
            else
            {
                Console.WriteLine($"Source view folder not found '{sourceViewDir}'");
            }
        }

        private string getPublishDir(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
        {
            var hostEnv = sp.GetService<IHostEnvironment>();
            string versionDir;
            if (hostEnv.IsProduction())
            {
                versionDir = versionKey.DisplayText;
            }
            else
            {
                versionDir = AppVersionKey.Current.DisplayText;
            }
            return Path.Combine
            (
                getXtiDir(),
                "Published",
                hostEnv.EnvironmentName,
                $"{getAppType(appKey)}s",
                getAppName(appKey),
                versionDir
            );
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

        private async Task packLibProjects(IServiceProvider sp, string versionNumber)
        {
            Console.WriteLine("Packing Lib Projects");
            var hostEnv = sp.GetService<IHostEnvironment>();
            var options = sp.GetService<IOptions<PublishOptions>>().Value;
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
                    packageVersion = await retrieveDevPackageVersion(options);
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
                        .AddArgument("c", getConfiguration(sp))
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

        private string getConfiguration(IServiceProvider sp)
        {
            var hostEnv = sp.GetService<IHostEnvironment>();
            return hostEnv.IsProduction()
                ? "Release"
                : "Debug";
        }

        private async Task<string> retrieveDevPackageVersion(PublishOptions options)
        {
            var appKey = new AppKey(new AppName(options.AppName), AppType.Values.Value(options.AppType));
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

        private async Task completeVersion(IServiceProvider sp, AppKey appKey)
        {
            var hostEnv = sp.GetService<IHostEnvironment>();
            if (hostEnv.IsProduction())
            {
                var options = sp.GetService<IOptions<PublishOptions>>().Value;
                var versionOptions = new VersionToolOptions();
                versionOptions.CommandCompleteVersion(options.RepoOwner, options.RepoName, appKey.Name.Value, appKey.Type.DisplayText);
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

        private async Task runInstall(IServiceProvider sp, AppKey appKey)
        {
            var options = sp.GetService<IOptions<PublishOptions>>().Value;
            if (!appKey.Type.Equals(AppType.Values.Package) && !options.NoInstall)
            {
                Console.WriteLine("Installing");
                var installPath = Path.Combine
                (
                    getXtiDir(),
                    "Tools",
                    "InstallApp",
                    "InstallApp.exe"
                );
                var hostEnv = sp.GetService<IHostEnvironment>();
                var result = await new XtiProcess(installPath)
                    .WriteOutputToConsole()
                    .UseEnvironment(hostEnv.EnvironmentName)
                    .AddConfigOptions
                    (
                        new
                        {
                            AppName = appKey.Name.DisplayText,
                            AppType = appKey.Type.DisplayText,
                            DestinationMachine = options.DestinationMachine
                        }
                    )
                    .Run();
                result.EnsureExitCodeIsZero();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
