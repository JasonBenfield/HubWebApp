namespace XTI_HubAppApi.AppInstall;

public sealed class GetVersionsRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
}
