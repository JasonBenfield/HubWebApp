namespace XTI_HubWebAppApi.AppPublish;

public sealed class BeginPublishAction : AppAction<PublishVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public BeginPublishAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<XtiVersionModel> Execute(PublishVersionRequest publishRequest, CancellationToken stoppingToken) => 
        hubAdministration.BeginPublish
        (
            publishRequest.ToAppVersionName(), 
            publishRequest.ToAppVersionKey(),
            stoppingToken
        );
}