namespace XTI_HubAppApi.AppInstall;

public sealed class AddOrUpdateAppsRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppDefinitionModel[] Apps { get; set; } = new AppDefinitionModel[0];
}
