using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class AddSystemUserAction : AppAction<AddSystemUserRequest, AppUserModel>
{
    private readonly IHubAdministration hubAdmin;

    public AddSystemUserAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppUserModel> Execute(AddSystemUserRequest model) =>
        hubAdmin.AddOrUpdateSystemUser(model.AppKey, model.MachineName, model.Password);
}