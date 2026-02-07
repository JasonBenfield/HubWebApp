using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Installation;

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
        var process = new RobocopyProcess(sourceDir, installDir)
            .CopySubdirectoriesIncludingEmpty()
            .NoDirectoryLogging()
            .NoFileClassLogging()
            .NoFileLogging()
            .NoFileSizeLogging()
            .NoJobHeader()
            .NoJobSummary()
            .NoProgressDisplayed()
            .MultiThreaded(32)
            .NumberOfRetries(5)
            .WaitTimeBetweenRetries(5);
        if (purge)
        {
            process.Purge();
        }
        return process.Run();
    }
}