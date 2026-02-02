using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallAppProcessFactory
{
    private readonly InstallWebAppProcess installWebAppProcess;
    private readonly InstallServiceAppProcess installServiceAppProcess;
    private readonly InstallDefaultAppProcess installDefaultAppProcess;

    public InstallAppProcessFactory(IHubAdministration hubAdministration, XtiFolder xtiFolder, XtiEnvironment xtiEnv, ISecretCredentialsFactory credentialsFactory)
    {
        installWebAppProcess = new InstallWebAppProcess(xtiFolder, hubAdministration, xtiEnv, credentialsFactory);
        installServiceAppProcess = new InstallServiceAppProcess(xtiFolder, hubAdministration, xtiEnv, credentialsFactory);
        installDefaultAppProcess = new InstallDefaultAppProcess(xtiFolder, hubAdministration);
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
