namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddSystemUserAction : AppAction<AddSystemUserRequest, AppUserModel>
{
    private readonly IHubService hubAdmin;

    public AddSystemUserAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppUserModel> Execute(AddSystemUserRequest addRequest, CancellationToken stoppingToken) =>
        hubAdmin.AddOrUpdateSystemUser
        (
            addRequest.AppKey.ToAppKey(), 
            addRequest.MachineName, 
            addRequest.Password,
            stoppingToken
        );
}