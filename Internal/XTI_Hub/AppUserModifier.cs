using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserModifier
{
    private readonly AppFactory factory;
    private readonly AppUser appUser;
    private readonly Modifier modifier;

    internal AppUserModifier(AppFactory factory, AppUser appUser, Modifier modifier)
    {
        this.factory = factory;
        this.appUser = appUser;
        this.modifier = modifier;
    }

    public Task AddRole(AppRole role)
    {
        var record = new AppUserRoleEntity
        {
            UserID = appUser.ID.Value,
            RoleID = role.ID.Value,
            ModifierID = modifier.ID.Value
        };
        return factory.DB.UserRoles.Create(record);
    }

    public async Task RemoveRole(AppRole role)
    {
        var userRole = await factory.DB
            .UserRoles
            .Retrieve()
            .Where
            (
                ur => ur.UserID == appUser.ID.Value
                    && ur.ModifierID == modifier.ID.Value
                    && ur.RoleID == role.ID.Value
            )
            .FirstOrDefaultAsync();
        if (userRole != null)
        {
            await factory.DB.UserRoles.Delete(userRole);
        }
    }

    public Task<AppRole[]> ExplicitlyUnassignedRoles()
        => factory.Roles.RolesNotAssignedToUser(appUser, modifier);

    public async Task<AppRole[]> AssignedRoles()
    {
        var roles = await ExplicitlyAssignedRoles();
        if (!roles.Any() && !modifier.ModKey().Equals(ModifierKey.Default))
        {
            var defaultModifier = await modifier.DefaultModifier();
            roles = await new AppUserModifier(factory, appUser, defaultModifier).AssignedRoles();
        }
        return roles;
    }

    public Task<AppRole[]> ExplicitlyAssignedRoles()
        => factory.Roles.RolesAssignedToUser(appUser, modifier);

}