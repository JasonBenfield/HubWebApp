namespace XTI_Hub.Abstractions;

public sealed class GetAppContextRequest
{
    public GetAppContextRequest()
        : this(0)
    {
    }

    public GetAppContextRequest(int installationID)
    {
        InstallationID = installationID;
    }

    public int InstallationID { get; set; }
}
