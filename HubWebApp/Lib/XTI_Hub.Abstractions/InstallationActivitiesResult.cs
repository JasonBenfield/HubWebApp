namespace XTI_Hub.Abstractions;

public sealed record InstallationActivitiesResult
(
    AppVersionInstallationModel[] Deletions,
    AppVersionInstallationModel[] Installations
)
{
    public InstallationActivitiesResult()
        : this([], [])
    {
    }
}
