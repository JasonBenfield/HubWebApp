namespace XTI_HubWebAppApi.AppPublish;

internal sealed class NewVersionAction : AppAction<NewVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public NewVersionAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<XtiVersionModel> Execute(NewVersionRequest newVersionRequest, CancellationToken stoppingToken) =>
        hubAdministration.StartNewVersion
        (
            newVersionRequest.ToAppVersionName(),
            newVersionRequest.ToAppVersionType()
        );
}