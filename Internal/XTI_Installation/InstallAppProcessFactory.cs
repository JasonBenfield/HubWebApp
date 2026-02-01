using System;
using System.Collections.Generic;
using System.Text;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallAppProcessFactory
{
    private readonly InstallWebAppProcess installWebAppProcess;
    private readonly InstallServiceAppProcess installServiceAppProcess;
    private readonly InstallDefaultAppProcess installDefaultAppProcess;

    public InstallAppProcessFactory(XtiFolder xtiFolder, XtiEnvironment xtiEnv, ISecretCredentialsFactory credentialsFactory)
    {
        installWebAppProcess = new InstallWebAppProcess(xtiFolder, xtiEnv, credentialsFactory);
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
