namespace XTI_HubAppApi.AppPublish;

public sealed class BeginPublishAction : AppAction<PublishVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public BeginPublishAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<XtiVersionModel> Execute(PublishVersionRequest model, CancellationToken stoppingToken) => hubAdministration.BeginPublish(model.VersionName, model.VersionKey);
}