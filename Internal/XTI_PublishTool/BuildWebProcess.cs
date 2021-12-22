using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;
using XTI_VersionToolApi;

namespace XTI_PublishTool;

public sealed class BuildWebProcess
{
    private readonly AppKey appKey;
    private readonly AppVersionKey versionKey;
    private readonly IHostEnvironment hostEnv;
    private readonly XtiFolder xtiFolder;
    private readonly string imports;

    public BuildWebProcess(AppKey appKey, AppVersionKey versionKey, IHostEnvironment hostEnv, string imports)
    {
        this.appKey = appKey;
        this.versionKey = versionKey;
        this.hostEnv = hostEnv;
        xtiFolder = new XtiFolder(hostEnv);
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
                    DefaultVersion = versionKey.Value
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
            var importAppKey = new AppKey(new AppName(appToImport), AppType.Values.WebApp);
            AppVersionKey appVersionKey;
            if (hostEnv.IsProduction())
            {
                var currentVersion = await retrieveCurrentVersion(importAppKey);
                appVersionKey = AppVersionKey.Parse(currentVersion.VersionKey);
            }
            else
            {
                appVersionKey = AppVersionKey.Current;
            }
            Console.WriteLine($"Importing {appToImport} {appVersionKey.DisplayText}");
            var baseSourcePath = Path.Combine
            (
                xtiFolder.PublishPath(importAppKey, appVersionKey),
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
        }
    }

    private async Task<VersionToolOutput> retrieveCurrentVersion(AppKey appKey)
    {
        var versionToolProcess = new XtiProcess
        (
            Path.Combine
            (
                xtiFolder.ToolsPath(),
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

    private string getAppName(AppKey appKey)
        => appKey.Name.DisplayText.Replace(" ", "");
}