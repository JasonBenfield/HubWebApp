namespace XTI_HubAppApi.AppInstall;

public sealed class InstalledRequest
{
    public InstalledRequest() { }

    public InstalledRequest(int installationID)
    {
        InstallationID = installationID;
    }

    public int InstallationID { get; set; }
}