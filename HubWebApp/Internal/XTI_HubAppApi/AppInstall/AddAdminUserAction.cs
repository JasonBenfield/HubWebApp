namespace XTI_HubAppApi.AppInstall;

internal sealed class AddAdminUserAction : AppAction<AddAdminUserRequest, AppUserModel>
{
    private readonly IHubAdministration hubAdmin;

    public AddAdminUserAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppUserModel> Execute(AddAdminUserRequest model, CancellationToken stoppingToken) =>
        hubAdmin.AddOrUpdateAdminUser(new AppUserName(model.UserName), model.Password);
}
