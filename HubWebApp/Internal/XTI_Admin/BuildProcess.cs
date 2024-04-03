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

    public async Task Run(CancellationToken ct)
    {
        Console.WriteLine("Add or Update Apps");
        var version = await branchVersion.Value(ct);
        var appKeys = selectedAppKeys.Values;
        var versionName = versionNameAccessor.Value;
        await hubAdmin.AddOrUpdateApps(versionName, appKeys, ct);
        Console.WriteLine("Building Apps");
        var slnDir = Environment.CurrentDirectory;
        foreach (var appKey in appKeys)
        {
            var appDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
            if (Directory.Exists(appDir))
            {
                Environment.CurrentDirectory = appDir;
            }
            if (appKey.IsAppType(AppType.Values.WebApp))
            {
                await RunApiGenerator(appKey, version.VersionKey);
            }
            if (appKey.IsAnyAppType(AppType.Values.WebApp, AppType.Values.WebPackage))
            {
                await RunTsc(appKey);
                await RunWebpack(appKey);
            }
            Environment.CurrentDirectory = slnDir;
        }
        await RunDotnetBuild();
    }

    private async Task RunApiGenerator(AppKey appKey, AppVersionKey versionKey)
    {
        var apiGeneratorPath = Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{GetAppName(appKey)}ApiGeneratorApp"
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
        else
        {
            Console.WriteLine($"API Generator app not found at '{apiGeneratorPath}'.");
        }
    }

    private async Task RunTsc(AppKey appKey)
    {
        var webAppDir = GetWebAppDir(appKey);
        await RunTsc
        (
            Path.Combine
            (
                webAppDir,
                "Scripts",
                GetAppName(appKey),
                "tsconfig.json"
            )
        );
        await RunTsc
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

    private static async Task RunTsc(string tsConfigPath)
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

    private async Task RunWebpack(AppKey appKey)
    {
        var projectDir = GetWebAppDir(appKey);
        var webpackConfigPath = Path.Combine(projectDir, "webpack.config.js");
        if (File.Exists(webpackConfigPath))
        {
            Console.WriteLine("Running webpack");
            var webpackProcess = new WinProcess("webpack");
            var result = await new CmdProcess(webpackProcess)
                .SetWorkingDirectory(projectDir)
                .WriteOutputToConsole()
                .Run();
            result.EnsureExitCodeIsZero();
        }
        else
        {
            Console.WriteLine($"webpack file '{webpackConfigPath}' not found.");
        }
    }

    private async Task RunDotnetBuild()
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

    private string GetWebAppDir(AppKey appKey)
        => Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            new AppDirectoryName(appKey).Value
        );

    private string GetAppName(AppKey appKey)
        => appKey.Name.DisplayText.Replace(" ", "");
}