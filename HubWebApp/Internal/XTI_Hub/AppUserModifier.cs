using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserModifier
{
    private readonly HubFactory factory;
    private readonly AppUser appUser;

    internal AppUserModifier(HubFactory factory, AppUser appUser, Modifier modifier)
    {
        this.factory = factory;
        this.appUser = appUser;
        this.Modifier = modifier;
    }

    public Modifier Modifier { get; }

    public async Task AssignRole(AppRole role, CancellationToken ct)
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
            await factory.DB.UserRoles.Create(record, ct);
        }
    }

    public async Task UnassignRole(AppRole role, CancellationToken ct)
    {
        var userRole = await GetUserRole(role).FirstOrDefaultAsync(ct);
        if (userRole != null)
        {
            await factory.DB.UserRoles.Delete(userRole, ct);
        }
    }

    private IQueryable<AppUserRoleEntity> GetUserRole(AppRole role) =>
        factory.DB.UserRoles.Retrieve()
            .Where
            (
                ur => ur.UserID == appUser.ID
                    && ur.ModifierID == Modifier.ID
                    && ur.RoleID == role.ID
            );

    public Task<AppRole[]> ExplicitlyUnassignedRoles(CancellationToken ct) => 
        factory.Roles.RolesNotAssignedToUser(appUser, Modifier, ct);

    public async Task<AppRole[]> AssignedRoles(CancellationToken ct)
    {
        var roles = await ExplicitlyAssignedRoles(ct);
        if (!roles.Any() && !Modifier.IsDefault())
        {
            var defaultModifier = await Modifier.DefaultModifier(ct);
            roles = await new AppUserModifier(factory, appUser, defaultModifier).AssignedRoles(ct);
        }
        return roles;
    }

    public Task<AppRole[]> ExplicitlyAssignedRoles(CancellationToken ct) => 
        factory.Roles.RolesAssignedToUser(appUser, Modifier, ct);


}