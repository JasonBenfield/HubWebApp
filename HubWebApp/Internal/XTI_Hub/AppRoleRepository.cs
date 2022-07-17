using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRoleRepository
{
    private readonly HubFactory factory;

    internal AppRoleRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal async Task<AppRole> AddIfNotFound(App app, AppRoleName name)
    {
        var record = await rolesForApp(app)
            .FirstOrDefaultAsync(r => r.Name == name.Value);
        if (record == null)
        {
            record = new AppRoleEntity
            {
                AppID = app.ID,
                Name = name.Value
            };
            await factory.DB.Roles.Create(record);
        }
        return factory.CreateRole(record);
    }

    internal Task<AppRole[]> RolesForApp(App app)
    {
        return factory.DB.Roles.Retrieve()
            .Where(r => r.AppID == app.ID)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync();
    }

    internal async Task<AppRole> Role(App app, int roleID)
    {
        var record = await rolesForApp(app)
            .Where(r => r.ID == roleID)
            .FirstOrDefaultAsync();
        return factory.CreateRole
        (
            record
            ?? throw new Exception($"Role {roleID} not found for app '{app.ToModel().AppKey.Format()}'")
        );
    }

    internal async Task<AppRole> Role(App app, AppRoleName roleName)
    {
        var record = await rolesForApp(app)
            .Where(r => r.Name == roleName.Value)
            .FirstOrDefaultAsync();
        return factory.CreateRole
        (
            record
            ?? throw new Exception($"Role '{roleName.DisplayText}' not found for app '{app.ToModel().AppKey.Format()}'")
        );
    }

    private IQueryable<AppRoleEntity> rolesForApp(App app)
    {
        return factory.DB.Roles
            .Retrieve()
            .Where(r => r.AppID == app.ID);
    }

    internal Task<AppRole[]> AllowedRolesForResourceGroup(ResourceGroup group)
        => rolesForResourceGroup(group, true);

    internal Task<AppRole[]> DeniedRolesForResourceGroup(ResourceGroup group)
        => rolesForResourceGroup(group, false);

    private Task<AppRole[]> rolesForResourceGroup(ResourceGroup group, bool isAllowed)
    {
        var roleIDs = factory.DB
            .ResourceGroupRoles
            .Retrieve()
            .Where(gr => gr.GroupID == group.ID && gr.IsAllowed == isAllowed)
            .Select(gr => gr.RoleID);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => roleIDs.Any(id => id == r.ID))
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync();
    }

    internal Task<AppRole[]> RolesNotAssignedToUser(AppUser user, Modifier modifier)
    {
        var appID = getAppID(modifier);
        var roleIDs = userRoleIDs(user, modifier);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => appID.Any(id => id == r.AppID) && !roleIDs.Any(id => id == r.ID))
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync();
    }

    internal Task<AppRole[]> RolesAssignedToUser(AppUser user, Modifier modifier)
    {
        var appID = getAppID(modifier);
        var roleIDs = userRoleIDs(user, modifier);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => appID.Any(id => id == r.AppID) && roleIDs.Any(id => id == r.ID))
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync();
    }

    private IQueryable<int> getAppID(Modifier modifier)
    {
        var modCategoryID = factory.DB
            .Modifiers
            .Retrieve()
            .Where(m => m.ID == modifier.ID)
            .Select(m => m.CategoryID);
        var appID = factory.DB
            .ModifierCategories
            .Retrieve()
            .Where(mc => modCategoryID.Any(id => mc.ID == id))
            .Select(mc => mc.AppID);
        return appID;
    }

    private IQueryable<int> userRoleIDs(AppUser user, Modifier modifier)
    {
        return factory.DB
            .UserRoles
            .Retrieve()
            .Where(ur => ur.UserID == user.ID && ur.ModifierID == modifier.ID)
            .Select(ur => ur.RoleID);
    }

    internal Task<AppRole[]> AllowedRolesForResource(Resource resource)
        => rolesForResource(resource, true);

    internal Task<AppRole[]> DeniedRolesForResource(Resource resource)
        => rolesForResource(resource, false);

    private Task<AppRole[]> rolesForResource(Resource resource, bool isAllowed)
    {
        var roleIDs = factory.DB
            .ResourceRoles
            .Retrieve()
            .Where(gr => gr.ResourceID == resource.ID && gr.IsAllowed == isAllowed)
            .Select(gr => gr.RoleID);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => roleIDs.Contains(r.ID) && r.TimeDeactivated == DateTimeOffset.MaxValue)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync();
    }
}