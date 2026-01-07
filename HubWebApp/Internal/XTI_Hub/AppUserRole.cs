using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserRole
{
    private readonly HubFactory factory;
    private readonly AppUserRoleEntity userRole;

    public AppUserRole(HubFactory factory, AppUserRoleEntity userRole)
    {
        this.factory = factory;
        this.userRole = userRole;
    }

    public int ID { get => userRole.ID; }

    public Task<AppUser> User(CancellationToken ct) => factory.Users.User(userRole.UserID, ct);

    public Task<Modifier> Modifier(CancellationToken ct) => factory.Modifiers.Modifier(userRole.ModifierID, ct);

    public Task<AppRole> Role(CancellationToken ct) => factory.Roles.Role(userRole.RoleID, ct);

    public Task Delete(CancellationToken ct) =>
        factory.DB.UserRoles.Delete(userRole, ct);
}
