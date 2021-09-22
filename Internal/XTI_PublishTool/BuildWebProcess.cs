using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Processes;
using XTI_VersionToolApi;

namespace XTI_PublishTool
{
    public sealed class BuildWebProcess
    {
        private readonly AppKey appKey;
        private readonly string versionKey;
        private readonly IHostEnvironment hostEnv;
        private readonly string imports;

        public BuildWebProcess(AppKey appKey, string versionKey, IHostEnvironment hostEnv, string imports)
        {
            this.appKey = appKey;
            this.versionKey = versionKey;
            this.hostEnv = hostEnv;
            this.imports = imports;
        }

        public async Task Build()
        {
            Console.WriteLine("Generating API");
            await runApiGenerator();
            await runTsc();
            Console.WriteLine("Copying imports");
            await copyWebImports();
            Console.WriteLine("Running webpack");
            await runWebpack();
            Console.WriteLine("Building web app");
            await runDotnetBuild();
        }

        private async Task runApiGenerator()
        {
            var apiGeneratorPath = Path.Combine
            (
                Environment.CurrentDirectory,
                "Apps",
                $"{getAppName(appKey)}ApiGeneratorApp"
            );
            if (Directory.Exists(apiGeneratorPath))
            {
                var apiGeneratorProcess = new DotnetRunProcess(apiGeneratorPath);
                apiGeneratorProcess.WriteOutputToConsole();
                apiGeneratorProcess.AddConfigOptions
                (
                    new
                    {
                        DefaultVersion = versionKey
                    },
                    "Output"
                );
                var result = await apiGeneratorProcess.Run();
                result.EnsureExitCodeIsZero();
            }
            else
            {
                Console.WriteLine($"API Generator not Found at '{apiGeneratorPath}'");
            }
        }

        private async Task runTsc()
        {
            var projectDir = getProjectDir();
            var tsConfigPath = Path.Combine
            (
                projectDir,
                "Scripts",
                getAppName(appKey),
                "tsconfig.json"
            );
            Console.WriteLine($"Compiling Typescript '{tsConfigPath}'");
            if (File.Exists(tsConfigPath))
            {
                var tscProcess = new WinProcess("tsc")
                    .UseArgumentNameDelimiter("-")
                    .AddArgument("p", new Quoted(tsConfigPath));
                var result = await new CmdProcess(tscProcess)
                    .WriteOutputToConsole()
                    .Run();
                result.EnsureExitCodeIsZero();
            }
            else
            {
                Console.WriteLine($"tsconfig file not found '{tsConfigPath}'");
            }
        }

        private async Task copyWebImports()
        {
            var appsToImport = (imports ?? "")
                .Split(",")
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .ToArray();
            foreach (var appToImport in appsToImport)
            {
                string versionDir;
                var importAppKey = new AppKey(new AppName(appToImport), AppType.Values.WebApp);
                if (hostEnv.IsProduction())
                {
                    var currentVersion = await retrieveCurrentVersion(importAppKey);
                    versionDir = currentVersion.VersionKey;
                }
                else
                {
                    versionDir = AppVersionKey.Current.DisplayText;
                }
                Console.WriteLine($"Importing {appToImport} {versionDir}");
                var baseSourcePath = Path.Combine
                (
                    getXtiDir(),
                    "Published",
                    hostEnv.EnvironmentName,
                    "WebApps",
                    getAppName(importAppKey),
                    versionDir,
                    "Exports"
                );
                var sourceScriptPath = Path.Combine(baseSourcePath, "Scripts");
                if (Directory.Exists(sourceScriptPath))
                {
                    var targetScriptPath = Path.Combine
                    (
                        getProjectDir(),
                        "Imports",
                        appToImport
                    );
                    await new RobocopyProcess(sourceScriptPath, targetScriptPath)
                        .CopySubdirectoriesIncludingEmpty()
                        .Purge()
                        .NoFileClassLogging()
                        .NoFileLogging()
                        .NoDirectoryLogging()
                        .NoJobHeader()
                        .NoJobSummary()
                        .AddReadonlyAttributeToTarget()
                        .Run();
                }
                else
                {
                    Console.WriteLine($"Script imports not found for {appToImport} '{sourceScriptPath}'");
                }
                var sourceViewPath = Path.Combine(baseSourcePath, "Views");
                if (Directory.Exists(sourceViewPath))
                {
                    var targetViewPath = Path.Combine
                    (
                        getProjectDir(),
                        "Views",
                        "Exports",
                        appToImport
                    );
                    await new RobocopyProcess(sourceViewPath, targetViewPath)
                        .CopySubdirectoriesIncludingEmpty()
                        .Purge()
                        .NoFileClassLogging()
                        .NoFileLogging()
                        .NoDirectoryLogging()
                        .NoJobHeader()
                        .NoJobSummary()
                        .AddReadonlyAttributeToTarget()
                        .Run();
                }
                else
                {
                    Console.WriteLine($"View imports not found for {appToImport} '{sourceViewPath}'");
                }
            }
        }

        private static async Task<VersionToolOutput> retrieveCurrentVersion(AppKey appKey)
        {
            var versionToolProcess = new XtiProcess
            (
                Path.Combine
                (
                    getXtiDir(),
                    "Tools",
                    "XTI_VersionTool",
                    "XTI_VersionTool.exe"
                )
            );
            versionToolProcess.UseProductionEnvironment();
            var versionOptions = new VersionToolOptions();
            versionOptions.CommandGetCurrentVersion(appKey.Name.Value, appKey.Type.DisplayText);
            versionToolProcess.AddConfigOptions(versionOptions);
            var versionResult = await versionToolProcess.Run();
            var currentVersion = versionResult.Data<VersionToolOutput>();
            return currentVersion;
        }

        private static string getXtiDir() => Environment.GetEnvironmentVariable("XTI_Dir");

        private string getAppName(AppKey appKey)
            => appKey.Name.DisplayText.Replace(" ", "");

        private async Task runWebpack()
        {
            var projectDir = getProjectDir();
            var webpackConfigPath = Path.Combine(projectDir, "webpack.config.js");
            var webpackProcess = new WinProcess("webpack")
                .UseArgumentNameDelimiter("--")
                .AddArgument("config", new Quoted(webpackConfigPath));
            var result = await new CmdProcess(webpackProcess)
                .WriteOutputToConsole()
                .Run();
            result.EnsureExitCodeIsZero();
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

        private string getProjectDir()
            => Path.Combine
            (
                Environment.CurrentDirectory,
                "Apps",
                $"{getAppName(appKey)}WebApp"
            );

    }
}
