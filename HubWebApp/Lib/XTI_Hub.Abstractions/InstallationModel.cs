namespace XTI_Hub.Abstractions;

public sealed record InstallationModel(int ID, InstallStatus Status, bool IsCurrent, string Domain, string SiteName)
{
    public InstallationModel()
        : this(0, InstallStatus.Values.GetDefault(), false, "", "")
{
}
}
