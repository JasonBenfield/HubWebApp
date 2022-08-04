namespace XTI_HubWebAppApi.AppInstall;

public sealed class InstallationRequest
{
    public InstallationRequest() { }

    public InstallationRequest(int installationID)
    {
        InstallationID = installationID;
    }

    public int InstallationID { get; set; }
}