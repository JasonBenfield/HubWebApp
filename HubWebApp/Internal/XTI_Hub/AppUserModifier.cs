using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserModifier
{
    private readonly EfHubDB factory;
    private readonly EfAppUser appUser;

    internal AppUserModifier(EfHubDB factory, EfAppUser appUser, EfModifier modifier)
    {
        this.factory = factory;
        this.appUser = appUser;
        this.Modifier = modifier;
    }

    public EfModifier Modifier { get; }

    public async Task AssignRole(EfAppRole role, CancellationToken ct)
    {
        var any = await GetUserRole(role).AnyAsync(ct);
        if (!any)
        {
            var record = new AppUserRoleEntity
            {
                UserID = appUser.ID,
                RoleID = role.ID,
                ModifierID = Modifier.ID
            };
            await factory.Context.UserRoles.Create(record, ct);
        }
    }

    public async Task UnassignRole(EfAppRole role, CancellationToken ct)
    {
        var userRole = await GetUserRole(role).FirstOrDefaultAsync(ct);
        if (userRole != null)
        {
            await factory.Context.UserRoles.Delete(userRole, ct);
        }
    }

    private IQueryable<AppUserRoleEntity> GetUserRole(EfAppRole role) =>
        factory.Context.UserRoles.Retrieve()
            .Where
            (
                ur => ur.UserID == appUser.ID
                    && ur.ModifierID == Modifier.ID
                    && ur.RoleID == role.ID
            );

    public Task<EfAppRole[]> ExplicitlyUnassignedRoles(CancellationToken ct) => 
        factory.Roles.RolesNotAssignedToUser(appUser, Modifier, ct);

    public async Task<EfAppRole[]> AssignedRoles(CancellationToken ct)
    {
        var roles = await ExplicitlyAssignedRoles(ct);
        if (!roles.Any() && !Modifier.IsDefault())
        {
            var defaultModifier = await Modifier.DefaultModifier(ct);
            roles = await new AppUserModifier(factory, appUser, defaultModifier).AssignedRoles(ct);
        }
        return roles;
    }

    public Task<EfAppRole[]> ExplicitlyAssignedRoles(CancellationToken ct) => 
        factory.Roles.RolesAssignedToUser(appUser, Modifier, ct);


}