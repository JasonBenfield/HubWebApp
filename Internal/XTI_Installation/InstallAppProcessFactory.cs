using XTI_App.Abstractions;
using XTI_Core;
using XTI_Internal.Abstractions;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallAppProcessFactory
{
    private readonly InstallWebAppProcess installWebAppProcess;
    private readonly InstallServiceAppProcess installServiceAppProcess;
    private readonly InstallDefaultAppProcess installDefaultAppProcess;

    public InstallAppProcessFactory(IHubService hubService, XtiFolder xtiFolder, XtiEnvironment xtiEnv, ISecretCredentialsFactory credentialsFactory)
    {
        installWebAppProcess = new InstallWebAppProcess(xtiEnv, xtiFolder, credentialsFactory);
        installServiceAppProcess = new InstallServiceAppProcess(xtiEnv, xtiFolder, credentialsFactory);
        installDefaultAppProcess = new InstallDefaultAppProcess(xtiFolder);
    }

    public InstallAppProcess Create(AppKey appKey)
    {
        InstallAppProcess installAppProcess;
        if (appKey.IsAppType(AppType.Values.WebApp))
        {
            installAppProcess = installWebAppProcess;
        }
        else if (appKey.IsAppType(AppType.Values.ServiceApp))
        {
            installAppProcess = installServiceAppProcess;
        }
        else
        {
            installAppProcess = installDefaultAppProcess;
        }
        return installAppProcess;
    }
}
