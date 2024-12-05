using XTI_App.Api;

namespace XTI_HubAppClient;

public sealed class HcUserContext : ISourceUserContext
{
    private readonly HubAppClient hubClient;
    private readonly ICurrentUserName currentUserName;

    public HcUserContext(HubAppClient hubClient, ICurrentUserName currentUserName)
    {
        this.hubClient = hubClient;
        this.currentUserName = currentUserName;
    }

    public async Task<AppUserModel> User()
    {
        var userName = await currentUserName.Value();
        var user = await User(userName);
        return user;
    }

    public Task<AppUserModel> User(AppUserName userName) =>
        hubClient.System.GetUserByUserName
        (
            new AppUserNameRequest(userName)
        );

    public Task<AppUserModel> UserOrAnon(AppUserName userName) =>
        hubClient.System.GetUserOrAnon
        (
            new AppUserNameRequest(userName)
        );

    public Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier) =>
        hubClient.System.GetUserRoles
        (
            new GetUserRolesRequest(user.ID, modifier.ID)
        );
}
