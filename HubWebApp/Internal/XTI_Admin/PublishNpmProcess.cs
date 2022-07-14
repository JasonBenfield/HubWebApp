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
            var tempExportDistDir = Path.Combine(Path.GetTempPath(), getAppName(appKey), "NpmExports");
            var exportScriptDir = Path.Combine(publishDir, "npm");
            if (Directory.Exists(tempExportDistDir))
            {
                Directory.Delete(tempExportDistDir, true);
            }
            Console.WriteLine($"Running tsc at '{sourceScriptPath}'");
            var tscProcess = new WinProcess("tsc")
                .UseArgumentNameDelimiter("--")
                .AddArgument("declaration", "true")
                .AddArgument("outDir", tempExportDistDir);
            var tscResult = await new CmdProcess(tscProcess)
                .SetWorkingDirectory(sourceScriptPath)
                .WriteOutputToConsole()
                .Run();
            tscResult.EnsureExitCodeIsZero();

            await new RobocopyProcess(sourceScriptPath, tempExportDistDir)
                .CopySubdirectoriesIncludingEmpty()
                .Pattern("*.d.ts")
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .Run();

            await new RobocopyProcess(sourceScriptPath, tempExportDistDir)
                .CopySubdirectoriesIncludingEmpty()
                .Pattern("*.html")
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .Run();

            await new RobocopyProcess(sourceScriptPath, tempExportDistDir)
                .CopySubdirectoriesIncludingEmpty()
                .Pattern("*.scss")
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .Run();

            new IndexFile(tempExportDistDir).Write();
            var npmrcPath = Path.Combine(projectDir, ".npmrc");
            if (File.Exists(npmrcPath))
            {
                File.Copy
                (
                    npmrcPath,
                    Path.Combine(tempExportDistDir, ".npmrc")
                );
            }
            File.Copy
            (
                Path.Combine(projectDir, "package.json"),
                Path.Combine(tempExportDistDir, "package.json")
            );
            Console.WriteLine($"Running npm install at '{tempExportDistDir}'");
            var npmProcess = new WinProcess("npm")
                .UseArgumentNameDelimiter("")
                .AddArgument("install");
            var npmResult = await new CmdProcess(npmProcess)
                .WriteOutputToConsole()
                .SetWorkingDirectory(tempExportDistDir)
                .Run();
            npmResult.EnsureExitCodeIsZero();
            File.Delete(Path.Combine(tempExportDistDir, "main.d.ts"));
            File.Delete(Path.Combine(tempExportDistDir, "_references.d.ts"));
            new IndexDeclarationFile(tempExportDistDir).Write();
            var npmVersionProcess = new WinProcess("npm")
                .UseArgumentNameDelimiter("")
                .UseArgumentValueDelimiter(" ")
                .AddArgument("version", versionNumber)
                .UseArgumentNameDelimiter("--")
                .AddArgument("allow-same-version");
            var npmVersionResult = await new CmdProcess(npmVersionProcess)
                .SetWorkingDirectory(tempExportDistDir)
                .WriteOutputToConsole()
                .Run();
            npmVersionResult.EnsureExitCodeIsZero();

            await new RobocopyProcess(tempExportDistDir, exportScriptDir)
                .CopySubdirectoriesIncludingEmpty()
                .Purge()
                .NoFileClassLogging()
                .NoFileLogging()
                .NoDirectoryLogging()
                .NoJobHeader()
                .NoJobSummary()
                .Run();
            if (xtiEnv.IsProduction())
            {
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