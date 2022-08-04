namespace XTI_HubWebAppApi.AppInstall;

public sealed class GetVersionsRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
}
