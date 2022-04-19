using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class CopyToInstallDirProcess
{
    private readonly Scopes scopes;

    public CopyToInstallDirProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string tempDir, AppKey appKey, AppVersionKey versionKey, AppVersionKey installVersionKey, bool purge)
    {
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
        string sourceDir;
        if (xtiEnv.IsDevelopmentOrTest())
        {
            sourceDir = getSourceAppDir(xtiFolder, appKey, versionKey);
        }
        else
        {
            sourceDir = Path.Combine(tempDir, "App");
        }
        Console.WriteLine($"Copying from '{sourceDir}' to '{installDir}'");
        var process = new RobocopyProcess(sourceDir, installDir)
            .CopySubdirectoriesIncludingEmpty()
            .NoDirectoryLogging()
            .NoFileClassLogging()
            .NoFileLogging()
            .NoFileSizeLogging()
            .NoJobHeader()
            .NoJobSummary()
            .NoProgressDisplayed();
        if (purge)
        {
            process.Purge();
        }
        await process.Run();
    }

    private static string getSourceAppDir(XtiFolder xtiFolder, AppKey appKey, AppVersionKey versionKey) => 
        Path.Combine
        (
            xtiFolder.PublishPath(appKey, versionKey),
            "App"
        );

}