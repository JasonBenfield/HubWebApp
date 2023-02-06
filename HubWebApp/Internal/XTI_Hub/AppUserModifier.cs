using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
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

    public async Task AssignRole(AppRole role)
    {
        var any = await GetUserRole(role).AnyAsync();
        if (!any)
        {
            var record = new AppUserRoleEntity
            {
                UserID = appUser.ID,
                RoleID = role.ID,
                ModifierID = Modifier.ID
            };
            await factory.DB.UserRoles.Create(record);
        }
    }

    public async Task UnassignRole(AppRole role)
    {
        var userRole = await GetUserRole(role).FirstOrDefaultAsync();
        if (userRole != null)
        {
            await factory.DB.UserRoles.Delete(userRole);
        }
    }

    private IQueryable<AppUserRoleEntity> GetUserRole(AppRole role) =>
        factory.DB
            .UserRoles
            .Retrieve()
            .Where
            (
                ur => ur.UserID == appUser.ID
                    && ur.ModifierID == Modifier.ID
                    && ur.RoleID == role.ID
            );

    public Task<AppRole[]> ExplicitlyUnassignedRoles()
        => factory.Roles.RolesNotAssignedToUser(appUser, Modifier);

    public async Task<AppRole[]> AssignedRoles()
    {
        var roles = await ExplicitlyAssignedRoles();
        if (!roles.Any() && !Modifier.IsDefault())
        {
            var defaultModifier = await Modifier.DefaultModifier();
            roles = await new AppUserModifier(factory, appUser, defaultModifier).AssignedRoles();
        }
        return roles;
    }

    public Task<AppRole[]> ExplicitlyAssignedRoles()
        => factory.Roles.RolesAssignedToUser(appUser, Modifier);


}