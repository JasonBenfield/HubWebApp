using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

internal sealed class AddOrUpdateAppsAction : AppAction<AddOrUpdateAppsRequest, AppModel[]>
{
    private readonly IHubAdministration hubAdmin;

    public AddOrUpdateAppsAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppModel[]> Execute(AddOrUpdateAppsRequest model) =>
        hubAdmin.AddOrUpdateApps(model.VersionName, model.Apps);
}
