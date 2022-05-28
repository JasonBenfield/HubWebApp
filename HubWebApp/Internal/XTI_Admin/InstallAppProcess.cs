using XTI_App.Abstractions;

namespace XTI_Admin;

internal interface InstallAppProcess
{
    Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey);
}
