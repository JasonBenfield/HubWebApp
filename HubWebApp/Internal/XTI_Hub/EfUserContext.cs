using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfUserContext : ISourceUserContext
{
    private readonly HubFactory hubFactory;
    private readonly ICurrentUserName currentUserName;

    public EfUserContext(HubFactory hubFactory, ICurrentUserName currentUserName)
    {
        this.hubFactory = hubFactory;
        this.currentUserName = currentUserName;
    }

    public async Task<AppUserModel> User(CancellationToken ct)
    {
        var userName = await currentUserName.Value();
        var user = await User(userName, ct);
        return user;
    }

    public async Task<AppUserModel> User(AppUserName userName, CancellationToken ct)
    {
        var user = await hubFactory.Users.UserByUserName(userName, ct);
        return user.ToModel();
    }

    public async Task<AppUserModel> UserOrAnon(AppUserName userName, CancellationToken ct)
    {
        var user = await hubFactory.Users.UserOrAnon(userName, ct);
        return user.ToModel();
    }

    public async Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier, CancellationToken ct)
    {
        var appUser = await hubFactory.Users.User(user.ID, ct);
        var appMod = await hubFactory.Modifiers.Modifier(modifier.ID, ct);
        var roles = await appUser.Modifier(appMod).AssignedRoles(ct);
        return roles.Select(r => r.ToModel()).ToArray();
    }
}