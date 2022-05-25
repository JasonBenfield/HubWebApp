namespace XTI_HubAppApi.AppInstall;

public sealed class NewInstallationRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public string QualifiedMachineName { get; set; } = "";
    public string Domain { get; set; } = "";
}