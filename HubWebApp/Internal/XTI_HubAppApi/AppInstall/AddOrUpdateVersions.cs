using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

internal sealed class AddOrUpdateVersions : AppAction<AddOrUpdateVersionsRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdmin;

    public AddOrUpdateVersions(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public async Task<EmptyActionResult> Execute(AddOrUpdateVersionsRequest model)
    {
        await hubAdmin.AddOrUpdateVersions(model.App, model.Versions);
        return new EmptyActionResult();
    }
}
