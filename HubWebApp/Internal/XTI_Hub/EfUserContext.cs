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

    public async Task<AppUserModel> User()
    {
        var userName = await currentUserName.Value();
        var user = await User(userName);
        return user;
    }

    public async Task<AppUserModel> User(AppUserName userName)
    {
        var user = await hubFactory.Users.UserByUserName(userName);
        return user.ToModel();
    }

    public async Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier)
    {
        var appUser = await hubFactory.Users.User(user.ID);
        var appMod = await hubFactory.Modifiers.Modifier(modifier.ID);
        var roles = await appUser.Modifier(appMod).AssignedRoles();
        return roles.Select(r => r.ToModel()).ToArray();
    }
}