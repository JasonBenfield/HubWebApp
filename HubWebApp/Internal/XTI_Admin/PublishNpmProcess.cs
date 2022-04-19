using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class PublishNpmProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;

    public PublishNpmProcess(XtiEnvironment xtiEnv, XtiFolder xtiFolder)
    {
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
    }

    public async Task Run(AppKey appKey, AppVersionKey versionKey, string versionNumber)
    {
        var publishDir = getPublishDir(appKey, versionKey);
        var projectDir = getProjectDir(appKey);
        var sourceScriptPath = Path.Combine
        (
            projectDir,
            "Scripts",
            getAppName(appKey)
        );
        if (Directory.Exists(sourceScriptPath))
        {
            var tempExportDir = Path.Combine(Path.GetTempPath(), getAppName(appKey), "NpmExports");
            var exportScriptDir = Path.Combine(publishDir, "npm");
            if (Directory.Exists(tempExportDir))
            {
                Directory.Delete(tempExportDir, true);
            }
            var tempExportSrcDir = Path.Combine(tempExportDir, "src");
            var tempExportDistDir = Path.Combine(tempExportDir, "dist");
            try
            {
                Console.WriteLine($"Copying '{sourceScriptPath}' to '{tempExportSrcDir}'");
                await new RobocopyProcess(sourceScriptPath, tempExportSrcDir)
                    .CopySubdirectoriesIncludingEmpty()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();

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

                new IndexFile(tempExportSrcDir).Write();
                var npmrcPath = Path.Combine(projectDir, ".npmrc");
                if (File.Exists(npmrcPath))
                {
                    File.Copy
                    (
                        npmrcPath,
                        Path.Combine(tempExportSrcDir, ".npmrc")
                    );
                }
                File.Copy
                (
                    Path.Combine(projectDir, "package.json"),
                    Path.Combine(tempExportSrcDir, "package.json")
                );
                Console.WriteLine($"Running npm install at '{tempExportSrcDir}'");
                var npmProcess = new WinProcess("npm")
                    .UseArgumentNameDelimiter("")
                    .AddArgument("install");
                var npmResult = await new CmdProcess(npmProcess)
                    .WriteOutputToConsole()
                    .SetWorkingDirectory(tempExportSrcDir)
                    .Run();
                npmResult.EnsureExitCodeIsZero();
                File.Copy
                (
                    Path.Combine(projectDir, "package.json"),
                    Path.Combine(tempExportDistDir, "package.json")
                );
                Console.WriteLine($"Running tsc at '{tempExportSrcDir}'");
                var tscProcess = new WinProcess("tsc")
                    .UseArgumentNameDelimiter("--")
                    .AddArgument("declaration", "true")
                    .AddArgument("outDir", "../dist");
                var tscResult = await new CmdProcess(tscProcess)
                    .SetWorkingDirectory(tempExportSrcDir)
                    .WriteOutputToConsole()
                    .Run();
                tscResult.EnsureExitCodeIsZero();

                File.Delete(Path.Combine(tempExportDistDir, "main.d.ts"));
                File.Delete(Path.Combine(tempExportDistDir, "_references.d.ts"));

                new IndexDeclarationFile(tempExportDistDir).Write();

                var npmVersionProcess = new WinProcess("npm")
                    .UseArgumentNameDelimiter("")
                    .UseArgumentValueDelimiter(" ")
                    .AddArgument("version", versionNumber);
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
            }
            finally
            {
                Directory.Delete(tempExportDir, true);
            }
            if (xtiEnv.IsProduction())
            {
                var npmPublishProcess = new WinProcess("npm")
                    .UseArgumentNameDelimiter("--")
                    .UseArgumentValueDelimiter(" ")
                    .AddArgument("publish")
                    .AddArgument("registry", "https://npm.pkg.github.com");
                var npmPublishResult = await new CmdProcess(npmPublishProcess)
                    .SetWorkingDirectory(exportScriptDir)
                    .WriteOutputToConsole()
                    .Run();
                npmPublishResult.EnsureExitCodeIsZero();
            }
        }
        else
        {
            Console.WriteLine($"Source script folder not found '{sourceScriptPath}'");
        }
    }

    private string getPublishDir(AppKey appKey, AppVersionKey versionKey)
    {
        if (!xtiEnv.IsProduction())
        {
            versionKey = AppVersionKey.Current;
        }
        return xtiFolder.PublishPath(appKey, versionKey);
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