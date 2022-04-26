﻿using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserModifier
{
    private readonly AppFactory factory;
    private readonly AppUser user;
    private readonly Modifier modifier;

    internal AppUserModifier(AppFactory factory, AppUser appUser, Modifier modifier)
    {
        this.factory = factory;
        this.user = appUser;
        this.modifier = modifier;
    }

    public async Task AssignRole(AppRole role)
    {
        var any = await GetUserRole(role).AnyAsync();
        if (!any)
        {
            var record = new AppUserRoleEntity
            {
                UserID = user.ID.Value,
                RoleID = role.ID.Value,
                ModifierID = modifier.ID.Value
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
                ur => ur.UserID == user.ID.Value
                    && ur.ModifierID == modifier.ID.Value
                    && ur.RoleID == role.ID.Value
            );

    public Task<AppRole[]> ExplicitlyUnassignedRoles()
        => factory.Roles.RolesNotAssignedToUser(user, modifier);

    public async Task<AppRole[]> AssignedRoles()
    {
        var roles = await ExplicitlyAssignedRoles();
        if (!roles.Any() && !modifier.ModKey().Equals(ModifierKey.Default))
        {
            var defaultModifier = await modifier.DefaultModifier();
            roles = await new AppUserModifier(factory, user, defaultModifier).AssignedRoles();
        }
        return roles;
    }

    public Task<AppRole[]> ExplicitlyAssignedRoles()
        => factory.Roles.RolesAssignedToUser(user, modifier);

}