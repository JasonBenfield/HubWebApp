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

    public BuildWebProcess(AppKey appKey, AppVersionKey versionKey)
    {
        this.appKey = appKey;
        this.versionKey = versionKey;
    }

    public async Task Build()
    {
        Console.WriteLine("Generating API");
        await runApiGenerator();
        await runTsc();
        Console.WriteLine("Running webpack");
        await runWebpack();
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

    private async Task runWebpack()
    {
        var projectDir = getProjectDir();
        var webpackConfigPath = Path.Combine(projectDir, "webpack.config.js");
        if (File.Exists(webpackConfigPath))
        {
            var webpackProcess = new WinProcess("webpack")
                .UseArgumentNameDelimiter("--")
                .AddArgument("config", new Quoted(webpackConfigPath));
            var result = await new CmdProcess(webpackProcess)
                .WriteOutputToConsole()
                .Run();
            result.EnsureExitCodeIsZero();
        }
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