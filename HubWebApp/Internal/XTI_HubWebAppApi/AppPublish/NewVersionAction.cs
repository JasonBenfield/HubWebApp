namespace XTI_HubWebAppApi.AppPublish;

public sealed class NewVersionAction : AppAction<NewVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public NewVersionAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<XtiVersionModel> Execute(NewVersionRequest model, CancellationToken stoppingToken)
    {
        var version = await hubAdministration.StartNewVersion
        (
            model.VersionName,
            model.VersionType,
            model.AppKeys
        );
        return version;
    }
}