using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed record AdminInstallOptions
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
    string DestinationMachineName,
    string Domain,
    string SiteName
)
{
    public AdminInstallOptions()
        : this(AppKey.Unknown, AppVersionKey.None, "", "", "", 0, 0, "", "", "", "", "")
    {
    }
}