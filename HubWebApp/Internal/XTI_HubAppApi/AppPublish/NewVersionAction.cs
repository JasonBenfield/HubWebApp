using XTI_App.Api;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class NewVersionAction : AppAction<NewVersionRequest, XtiVersionModel>
{
    private readonly IHubAdministration hubAdministration;
    private readonly IClock clock;

    public NewVersionAction(IHubAdministration hubAdministration, IClock clock)
    {
        this.hubAdministration = hubAdministration;
        this.clock = clock;
    }

    public async Task<XtiVersionModel> Execute(NewVersionRequest model)
    {
        var version = await hubAdministration.StartNewVersion
        (
            model.VersionName,
            model.VersionType,
            model.AppDefinitions
        );
        return version;
    }
}