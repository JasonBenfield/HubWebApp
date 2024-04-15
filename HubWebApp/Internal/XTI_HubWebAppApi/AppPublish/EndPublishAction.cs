namespace XTI_HubWebAppApi.AppPublish;

public sealed class EndPublishAction : AppAction<PublishVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public EndPublishAction(IHubAdministration hubAdministration)
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