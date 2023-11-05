using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class PublishNpmProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly PublishedFolder publishFolder;

    public PublishNpmProcess(XtiEnvironment xtiEnv, XtiFolder xtiFolder, PublishedFolder publishFolder)
    {
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.publishFolder = publishFolder;
    }

    public async Task Run(AppKey appKey, AppVersionKey versionKey, string versionNumber)
    {
        var publishDir = publishFolder.AppDir(appKey, versionKey);
        var projectDir = getProjectDir(appKey);
        var sourceScriptPath = Path.Combine
        (
            projectDir,
            "Scripts",
            "Lib"
        );
        if (Directory.Exists(sourceScriptPath))
        {
            var exportScriptDir = Path.Combine(publishDir, "npm");
            if (Directory.Exists(exportScriptDir))
            {
                Directory.Delete(exportScriptDir, true);
            }
            Console.WriteLine($"Running tsc at '{sourceScriptPath}'");
            var tscProcess = new WinProcess("tsc")
                .UseArgumentNameDelimiter("--")
                .AddArgument("declaration", "true")
                .AddArgument("outDir", exportScriptDir);
            var tscResult = await new CmdProcess(tscProcess)
                .SetWorkingDirectory(sourceScriptPath)
                .WriteOutputToConsole()
                .Run();
            tscResult.EnsureExitCodeIsZero();

            Console.WriteLine($"Copying *.d.ts from '{sourceScriptPath}' to '{exportScriptDir}'");

            await new RobocopyProcess(sourceScriptPath, exportScriptDir)
                .CopySubdirectoriesIncludingEmpty()
                .Pattern("*.d.ts")
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .MultiThreaded(DefaultRobocopyThreads.Value)
                .NumberOfRetries(1)
                .WaitTimeBetweenRetries(1)
                .Run();

            Console.WriteLine($"Copying *.html from '{sourceScriptPath}' to '{exportScriptDir}'");

            await new RobocopyProcess(sourceScriptPath, exportScriptDir)
                .CopySubdirectoriesIncludingEmpty()
                .Pattern("*.html")
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .MultiThreaded(DefaultRobocopyThreads.Value)
                .NumberOfRetries(1)
                .WaitTimeBetweenRetries(1)
                .Run();

            Console.WriteLine($"Copying *.scss from '{sourceScriptPath}' to '{exportScriptDir}'");

            await new RobocopyProcess(sourceScriptPath, exportScriptDir)
                .CopySubdirectoriesIncludingEmpty()
                .Pattern("*.scss")
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .MultiThreaded(DefaultRobocopyThreads.Value)
                .NumberOfRetries(1)
                .WaitTimeBetweenRetries(1)
                .Run();

            new IndexFile(exportScriptDir).Write();
            var npmrcPath = Path.Combine(projectDir, ".npmrc");
            if (File.Exists(npmrcPath))
            {
                File.Copy
                (
                    npmrcPath,
                    Path.Combine(exportScriptDir, ".npmrc")
                );
            }
            File.Copy
            (
                Path.Combine(projectDir, "package.json"),
                Path.Combine(exportScriptDir, "package.json")
            );
            Console.WriteLine($"Running npm install at '{exportScriptDir}'");
            var npmProcess = new WinProcess("npm")
                .UseArgumentNameDelimiter("")
                .AddArgument("install");
            var npmResult = await new CmdProcess(npmProcess)
                .WriteOutputToConsole()
                .SetWorkingDirectory(exportScriptDir)
                .Run();
            npmResult.EnsureExitCodeIsZero();
            File.Delete(Path.Combine(exportScriptDir, "main.d.ts"));
            File.Delete(Path.Combine(exportScriptDir, "_references.d.ts"));
            new IndexDeclarationFile(exportScriptDir).Write();
            var npmVersionProcess = new WinProcess("npm")
                .UseArgumentNameDelimiter("")
                .UseArgumentValueDelimiter(" ")
                .AddArgument("version", versionNumber)
                .UseArgumentNameDelimiter("--")
                .AddArgument("allow-same-version");
            var npmVersionResult = await new CmdProcess(npmVersionProcess)
                .SetWorkingDirectory(exportScriptDir)
                .WriteOutputToConsole()
                .Run();
            npmVersionResult.EnsureExitCodeIsZero();

            if (xtiEnv.IsProduction())
            {
                Console.WriteLine("Publishing scripts to GitHub");

                var npmPublishProcess = new WinProcess("npm")
                    .AddArgument("publish")
                    .UseArgumentNameDelimiter("--")
                    .UseArgumentValueDelimiter(" ")
                    .AddArgument("registry", "https://npm.pkg.github.com");
                var cmdPublishProcess = new CmdProcess(npmPublishProcess)
                    .SetWorkingDirectory(exportScriptDir)
                    .WriteOutputToConsole();
                Console.WriteLine(cmdPublishProcess.CommandText());
                var npmPublishResult = await cmdPublishProcess.Run();
                npmPublishResult.EnsureExitCodeIsZero();
            }
        }
        else
        {
            Console.WriteLine($"Source script folder not found '{sourceScriptPath}'");
        }
    }

    private static string getProjectDir(AppKey appKey) =>
        Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{getAppName(appKey)}WebApp"
        );

    private static string getAppName(AppKey appKey)
        => appKey.Name.DisplayText.Replace(" ", "");

}