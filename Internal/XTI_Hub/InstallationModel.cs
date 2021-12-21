namespace XTI_Hub;

public sealed class InstallationModel
{
    public int ID { get; set; }

    public InstallStatus Status { get; set; } = InstallStatus.Values.NotSet;
}