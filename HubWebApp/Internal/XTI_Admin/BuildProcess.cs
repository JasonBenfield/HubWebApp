using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_Processes;

namespace XTI_Admin;

public sealed class BuildProcess
{
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly IHubAdministration hubAdmin;
    private readonly BranchVersion branchVersion;

    public BuildProcess(SelectedAppKeys selectedAppKeys, AppVersionNameAccessor versionNameAccessor, IHubAdministration hubAdmin, BranchVersion branchVersion)
    {
        this.selectedAppKeys = selectedAppKeys;
        this.versionNameAccessor = versionNameAccessor;
        this.hubAdmin = hubAdmin;
        this.branchVersion = branchVersion;
    }

    public async Task Run()
    {
        Console.WriteLine("Add or Update Apps");
        var version = await branchVersion.Value();
        var appKeys = selectedAppKeys.Values;
        var versionName = versionNameAccessor.Value;
        var appDefs = appKeys
            .Select(a => new AppDefinitionModel(a))
            .ToArray();
        await hubAdmin.AddOrUpdateApps(versionName, appDefs);
        Console.WriteLine("Building Apps");
        var slnDir = Environment.CurrentDirectory;
        foreach (var appKey in appKeys)
        {
            var appDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
            if (Directory.Exists(appDir))
            {
                Environment.CurrentDirectory = appDir;
            }
            await RunApiGenerator(appKey, version.VersionKey);
            await runTsc(appKey);
            await runWebpack(appKey);
            Environment.CurrentDirectory = slnDir;
        }
        await runDotnetBuild();
    }

    private async Task RunApiGenerator(AppKey appKey, AppVersionKey versionKey)
    {
        var apiGeneratorPath = Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{getAppName(appKey)}ApiGeneratorApp"
        );
        if (Directory.Exists(apiGeneratorPath))
        {
            Console.WriteLine("Generating API");
            var result = await new DotnetRunProcess(apiGeneratorPath)
                .WriteOutputToConsole()
                .AddConfigOptions
                (
                    new
                    {
                        DefaultVersion = versionKey.Value
                    },
                    "Output"
                )
                .Run();
            result.EnsureExitCodeIsZero();
        }
    }

    private async Task runTsc(AppKey appKey)
    {
        var webAppDir = getWebAppDir(appKey);
        await runTsc
        (
            Path.Combine
            (
                webAppDir,
                "Scripts",
                getAppName(appKey),
                "tsconfig.json"
            )
        );
        await runTsc
        (
            Path.Combine
            (
                webAppDir,
                "Scripts",
                "Internal",
                "tsconfig.json"
            )
        );
    }

    private static async Task runTsc(string tsConfigPath)
    {
        if (File.Exists(tsConfigPath))
        {
            Console.WriteLine($"Compiling Typescript '{tsConfigPath}'");
            var tscProcess = new WinProcess("tsc")
                .UseArgumentNameDelimiter("-")
                .AddArgument("p", new Quoted(tsConfigPath));
            var result = await new CmdProcess(tscProcess)
                .WriteOutputToConsole()
                .Run();
            result.EnsureExitCodeIsZero();
        }
    }

    private async Task runWebpack(AppKey appKey)
    {
        var projectDir = getWebAppDir(appKey);
        var webpackConfigPath = Path.Combine(projectDir, "webpack.config.js");
        if (File.Exists(webpackConfigPath))
        {
            Console.WriteLine("Running webpack");
            var webpackProcess = new WinProcess("webpack");
            //.UseArgumentNameDelimiter("--")
            //.AddArgument("config", new Quoted(webpackConfigPath));
            var result = await new CmdProcess(webpackProcess)
                .SetWorkingDirectory(projectDir)
                .WriteOutputToConsole()
                .Run();
            result.EnsureExitCodeIsZero();
        }
    }

    private async Task runDotnetBuild()
    {
        Console.WriteLine("Running dotnet build");
        var result = await new WinProcess("dotnet")
              .WriteOutputToConsole()
              .UseArgumentNameDelimiter("")
              .AddArgument("build")
              .UseArgumentNameDelimiter("-")
              .UseArgumentValueDelimiter("=")
              .AddArgument("p:TypeScriptCompileBlocked", "true")
              .Run();
        result.EnsureExitCodeIsZero();
    }

    private string getWebAppDir(AppKey appKey)
        => Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            new AppDirectoryName(appKey).Value
        );

    private string getAppName(AppKey appKey)
        => appKey.Name.DisplayText.Replace(" ", "");
}