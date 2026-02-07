namespace XTI_HubWebAppApiActions.AppPublish;

public sealed class EndPublishAction : AppAction<PublishVersionRequest, XtiVersionModel>
{
    private readonly IHubService hubAdministration;

    public EndPublishAction(IHubService hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<XtiVersionModel> Execute(PublishVersionRequest publishRequest, CancellationToken stoppingToken) => 
        hubAdministration.EndPublish
        (
            publishRequest.ToAppVersionName(), 
            publishRequest.ToAppVersionKey(),
            stoppingToken
        );
}