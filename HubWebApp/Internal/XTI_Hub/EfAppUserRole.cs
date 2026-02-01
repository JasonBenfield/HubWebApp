using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppUserRole
{
    private readonly EfHubDB factory;
    private readonly AppUserRoleEntity userRole;

    public EfAppUserRole(EfHubDB factory, AppUserRoleEntity userRole)
    {
        this.factory = factory;
        this.userRole = userRole;
    }

    public int ID { get => userRole.ID; }

    public Task<EfAppUser> User(CancellationToken ct) => factory.Users.User(userRole.UserID, ct);

    public Task<EfModifier> Modifier(CancellationToken ct) => factory.Modifiers.Modifier(userRole.ModifierID, ct);

    public Task<EfAppRole> Role(CancellationToken ct) => factory.Roles.Role(userRole.RoleID, ct);

    public Task Delete(CancellationToken ct) =>
        factory.Context.UserRoles.Delete(userRole, ct);
}
