using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Admin;

public sealed class InstallDefaultAppProcess :  InstallAppProcess
{
    private readonly XtiFolder xtiFolder;

    public InstallDefaultAppProcess(XtiFolder xtiFolder)
    {
        this.xtiFolder = xtiFolder;
    }

    public Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey)=>
        new CopyToInstallDirProcess(xtiFolder).Run
        (
            publishedAppDir, 
            adminInstOptions.AppKey, 
            installVersionKey, 
            true
        );

}