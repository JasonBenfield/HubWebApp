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

    public async Task<AppUserModel> User(CancellationToken ct)
    {
        var userName = await currentUserName.Value();
        var user = await User(userName, ct);
        return user;
    }

    public Task<AppUserModel> User(AppUserName userName, CancellationToken ct) =>
        hubClient.System.GetUserByUserName
        (
            new AppUserNameRequest(userName),
            ct
        );

    public Task<AppUserModel> UserOrAnon(AppUserName userName, CancellationToken ct) =>
        hubClient.System.GetUserOrAnon
        (
            new AppUserNameRequest(userName),
            ct
        );

    public Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier, CancellationToken ct) =>
        hubClient.System.GetUserRoles
        (
            new GetUserRolesRequest(user.ID, modifier.ID),
            ct
        );
}
