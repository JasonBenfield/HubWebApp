namespace XTI_HubWebAppApiActions.AppPublish;

public sealed class NewVersionAction : AppAction<NewVersionRequest, XtiVersionModel>
{
    private readonly IHubService hubAdministration;

    public NewVersionAction(IHubService hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<XtiVersionModel> Execute(NewVersionRequest newVersionRequest, CancellationToken stoppingToken) =>
        hubAdministration.StartNewVersion
        (
            newVersionRequest.ToAppVersionName(),
            newVersionRequest.ToAppVersionType(),
            stoppingToken
        );
}