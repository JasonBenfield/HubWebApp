using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed record AdminInstallOptions
(
    AppVersionKey VersionKey,
    string Release,
    int CurrentInstallationID,
    int VersionInstallationID
)
{
    public AdminInstallOptions()
        : this(AppVersionKey.None, "", 0, 0)
    {
    }
}