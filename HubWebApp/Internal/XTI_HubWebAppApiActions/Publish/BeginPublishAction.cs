namespace XTI_HubWebAppApiActions.AppPublish;

public sealed class BeginPublishAction : AppAction<PublishVersionRequest, XtiVersionModel>
{
    private readonly IHubService hubAdministration;

    public BeginPublishAction(IHubService hubAdministration)
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