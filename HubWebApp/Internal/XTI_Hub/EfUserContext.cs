using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfUserContext : ISourceUserContext
{
    private readonly EfHubDB db;
    private readonly ICurrentUserName currentUserName;

    public EfUserContext(EfHubDB db, ICurrentUserName currentUserName)
    {
        this.db = db;
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
        var user = await db.Users.UserByUserName(userName, ct);
        return user.ToModel();
    }

    public async Task<AppUserModel> UserOrAnon(AppUserName userName, CancellationToken ct)
    {
        var user = await db.Users.UserOrAnon(userName, ct);
        return user.ToModel();
    }

    public async Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier, CancellationToken ct)
    {
        var efUser = await db.Users.User(user.ID, ct);
        var efModifier = await db.Modifiers.Modifier(modifier.ID, ct);
        var efRoles = await efUser.Modifier(efModifier).AssignedRoles(ct);
        return efRoles.Select(r => r.ToModel()).ToArray();
    }
}