namespace XTI_HubWebAppApi.AppInstall;

public sealed class AddOrUpdateVersionsRequest
{
    public AppKey[] Apps { get; set; } = new AppKey[0];
    public XtiVersionModel[] Versions { get; set; } = new XtiVersionModel[0];
}
