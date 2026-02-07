namespace XTI_Hub.Abstractions;

public sealed class InstallationIDRequest
{
    public InstallationIDRequest()
        : this(0)
    {
    }

    public InstallationIDRequest(int installationID)
    {
        InstallationID = installationID;
    }

    public int InstallationID { get; set; }
}
