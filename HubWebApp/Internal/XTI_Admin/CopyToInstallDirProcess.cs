using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class CopyToInstallDirProcess
{
    private readonly XtiFolder xtiFolder;

    public CopyToInstallDirProcess(XtiFolder xtiFolder)
    {
        this.xtiFolder = xtiFolder;
    }

    public Task Run(string sourceDir, AppKey appKey, AppVersionKey installVersionKey, bool purge)
    {
        var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
        Console.WriteLine($"Copying from '{sourceDir}' to '{installDir}'");
        var process = new RobocopyProcess(sourceDir, installDir)
            .CopySubdirectoriesIncludingEmpty()
            .NoDirectoryLogging()
            .NoFileClassLogging()
            .NoFileLogging()
            .NoFileSizeLogging()
            .NoJobHeader()
            .NoJobSummary()
            .NoProgressDisplayed()
            .MultiThreaded(DefaultRobocopyThreads.Value)
            .NumberOfRetries(1)
            .WaitTimeBetweenRetries(1);
        if (purge)
        {
            process.Purge();
        }
        return process.Run();
    }
}