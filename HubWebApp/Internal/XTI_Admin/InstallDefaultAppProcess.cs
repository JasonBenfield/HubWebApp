using XTI_App.Abstractions;

namespace XTI_Admin;

internal sealed class InstallDefaultAppProcess :  InstallAppProcess
{
    private readonly Scopes scopes;

    public InstallDefaultAppProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey)=>
        new CopyToInstallDirProcess(scopes).Run(publishedAppDir, adminInstOptions.AppKey, installVersionKey, true);

}