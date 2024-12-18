namespace XTI_Hub.Abstractions;

public sealed class InstallationViewRequest
{
    public InstallationViewRequest()
        : this(0)
    {
    }

    public InstallationViewRequest(int installationID)
    {
        InstallationID = installationID;
    }

    public int InstallationID { get; set; }
}
