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

    internal async Task<AppRole> AddOrUpdate(App app, AppRoleName name, CancellationToken ct)
    {
        var record = await rolesForApp(app)
            .FirstOrDefaultAsync(r => r.Name == name.Value);
        if (record == null)
        {
            record = new AppRoleEntity
            {
                AppID = app.ID,
                Name = name.Value,
                DisplayText = name.DisplayText
            };
            await factory.DB.Roles.Create(record, ct);
        }
        else
        {
            await factory.DB.Roles.Update(record, r => r.DisplayText = name.DisplayText, ct);
        }
        return factory.CreateRole(record);
    }

    internal Task<AppRole[]> RolesForApp(App app, AppRoleName[] roleNames, CancellationToken ct)
    {
        var roleNameValues = roleNames.Select(rn => rn.Value).ToArray();
        return factory.DB.Roles.Retrieve()
            .Where(r => r.AppID == app.ID && roleNameValues.Contains(r.Name))
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync(ct);
    }

    internal Task<AppRole[]> RolesForApp(App app, CancellationToken ct) =>
        factory.DB.Roles.Retrieve()
            .Where(r => r.AppID == app.ID)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync(ct);

    internal async Task<AppRole> Role(int roleID, CancellationToken ct)
    {
        var record = await factory.DB.Roles.Retrieve()
            .Where(r => r.ID == roleID)
            .FirstOrDefaultAsync(ct);
        return factory.CreateRole
        (
            record
            ?? throw new Exception($"Role {roleID} not found.")
        );
    }

    internal async Task<AppRole> Role(App app, int roleID, CancellationToken ct)
    {
        var record = await rolesForApp(app)
            .Where(r => r.ID == roleID)
            .FirstOrDefaultAsync(ct);
        return factory.CreateRole
        (
            record
            ?? throw new Exception($"Role {roleID} not found for app '{app.ToModel().AppKey.Format()}'")
        );
    }

    internal async Task<AppRole> Role(App app, AppRoleName roleName, CancellationToken ct)
    {
        var record = await rolesForApp(app)
            .Where(r => r.Name == roleName.Value)
            .FirstOrDefaultAsync(ct);
        return factory.CreateRole
        (
            record
            ?? throw new Exception($"Role '{roleName.DisplayText}' not found for app '{app.ToModel().AppKey.Format()}'")
        );
    }

    private IQueryable<AppRoleEntity> rolesForApp(App app) =>
        factory.DB.Roles
            .Retrieve()
            .Where(r => r.AppID == app.ID);

    internal Task<AppRole[]> AllowedRolesForResourceGroup(ResourceGroup group, CancellationToken ct)
        => rolesForResourceGroup(group, true, ct);

    internal Task<AppRole[]> DeniedRolesForResourceGroup(ResourceGroup group, CancellationToken ct)
        => rolesForResourceGroup(group, false, ct);

    private Task<AppRole[]> rolesForResourceGroup(ResourceGroup group, bool isAllowed, CancellationToken ct)
    {
        var roleIDs = factory.DB
            .ResourceGroupRoles
            .Retrieve()
            .Where(gr => gr.GroupID == group.ID && gr.IsAllowed == isAllowed)
            .Select(gr => gr.RoleID);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == DateTimeOffset.MaxValue.Year)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync(ct);
    }

    internal Task<AppRole[]> RolesNotAssignedToUser(AppUser user, Modifier modifier, CancellationToken ct)
    {
        var appID = getAppID(modifier);
        var roleIDs = userRoleIDs(user, modifier);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => appID.Contains(r.AppID) && !roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == 9999)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync(ct);
    }

    internal Task<AppRole[]> RolesAssignedToUser(AppUser user, Modifier modifier, CancellationToken ct)
    {
        var appID = getAppID(modifier);
        var roleIDs = userRoleIDs(user, modifier);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => appID.Contains(r.AppID) && roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == 9999)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync(ct);
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
            .Where(mc => modCategoryID.Contains(mc.ID))
            .Select(mc => mc.AppID);
        return appID;
    }

    private IQueryable<int> userRoleIDs(AppUser user, Modifier modifier) =>
        factory.DB
            .UserRoles
            .Retrieve()
            .Where(ur => ur.UserID == user.ID && ur.ModifierID == modifier.ID)
            .Select(ur => ur.RoleID);

    internal Task<AppRole[]> AllowedRolesForResource(Resource resource, CancellationToken ct)
        => rolesForResource(resource, true, ct);

    internal Task<AppRole[]> DeniedRolesForResource(Resource resource, CancellationToken ct)
        => rolesForResource(resource, false, ct);

    private Task<AppRole[]> rolesForResource(Resource resource, bool isAllowed, CancellationToken ct)
    {
        var roleIDs = factory.DB
            .ResourceRoles
            .Retrieve()
            .Where(gr => gr.ResourceID == resource.ID && gr.IsAllowed == isAllowed)
            .Select(gr => gr.RoleID);
        return factory.DB
            .Roles
            .Retrieve()
            .Where(r => roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == DateTimeOffset.MaxValue.Year)
            .OrderBy(r => r.Name)
            .Select(r => factory.CreateRole(r))
            .ToArrayAsync(ct);
    }
}