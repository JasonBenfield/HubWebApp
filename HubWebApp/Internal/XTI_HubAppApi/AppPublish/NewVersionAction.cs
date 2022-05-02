namespace XTI_HubAppApi.AppPublish;

public sealed class NewVersionAction : AppAction<NewVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;

    public NewVersionAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<XtiVersionModel> Execute(NewVersionRequest model)
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