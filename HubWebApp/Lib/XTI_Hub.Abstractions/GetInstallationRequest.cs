namespace XTI_Hub.Abstractions;

public sealed class GetInstallationRequest
{
    public GetInstallationRequest()
        : this(0)
    {
    }

    public GetInstallationRequest(int installationID)
    {
        InstallationID = installationID;
    }

    public int InstallationID { get; set; }
}
