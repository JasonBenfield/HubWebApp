namespace XTI_Hub;

public sealed record InstallationModel(int ID, InstallStatus Status, bool IsCurrent, string Domain)
{
    public InstallationModel()
        :this(0,InstallStatus.Values.GetDefault(), false, "")
    {
    }
}