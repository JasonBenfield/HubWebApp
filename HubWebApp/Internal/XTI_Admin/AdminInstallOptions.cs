using XTI_App.Abstractions;

namespace XTI_Admin;

internal sealed record AdminInstallOptions
(
    AppKey AppKey,
    AppVersionKey VersionKey,
    string RepoOwner,
    string RepoName,
    string Release,
    int CurrentInstallationID,
    int VersionInstallationID,
    string InstallerUserName,
    string InstallerPassword,
    InstallationOptions Options
)
{
    public AdminInstallOptions()
        : this(AppKey.Unknown, AppVersionKey.None, "", "", "", 0, 0, "", "", new InstallationOptions())
    {
    }
}