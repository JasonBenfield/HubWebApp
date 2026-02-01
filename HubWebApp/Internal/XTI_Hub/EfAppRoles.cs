using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppRoles
{
    private readonly EfHubDB db;

    internal EfAppRoles(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfAppRole> AddOrUpdate(EfApp app, AppRoleName name, CancellationToken ct)
    {
        var role = await RolesForAppQuery(app)
            .FirstOrDefaultAsync(r => r.Name == name.Value);
        if (role == null)
        {
            role = new AppRoleEntity
            {
                AppID = app.ID,
                Name = name.Value,
                DisplayText = name.DisplayText
            };
            await db.Context.Roles.Create(role, ct);
        }
        else
        {
            await db.Context.Roles.Update(role, r => r.DisplayText = name.DisplayText, ct);
        }
        return new EfAppRole(db, role);
    }

    internal Task<EfAppRole[]> RolesForApp(EfApp app, AppRoleName[] roleNames, CancellationToken ct)
    {
        var roleNameValues = roleNames.Select(rn => rn.Value).ToArray();
        return db.Context.Roles.Retrieve()
            .Where(r => r.AppID == app.ID && roleNameValues.Contains(r.Name))
            .OrderBy(r => r.Name)
            .Select(r => new EfAppRole(db, r))
            .ToArrayAsync(ct);
    }

    internal Task<EfAppRole[]> RolesForApp(EfApp app, CancellationToken ct) =>
        db.Context.Roles.Retrieve()
            .Where(r => r.AppID == app.ID)
            .OrderBy(r => r.Name)
            .Select(r => new EfAppRole(db, r))
            .ToArrayAsync(ct);

    internal async Task<EfAppRole> Role(int roleID, CancellationToken ct)
    {
        var role = await db.Context.Roles.Retrieve()
            .Where(r => r.ID == roleID)
            .FirstOrDefaultAsync(ct);
        return new EfAppRole
        (
            db,
            role
            ?? throw new Exception($"Role {roleID} not found.")
        );
    }

    internal async Task<EfAppRole> Role(EfApp app, int roleID, CancellationToken ct)
    {
        var role = await RolesForAppQuery(app)
            .Where(r => r.ID == roleID)
            .FirstOrDefaultAsync(ct);
        return new EfAppRole
        (
            db,
            role
            ?? throw new Exception($"Role {roleID} not found for app '{app.ToModel().AppKey.Format()}'")
        );
    }

    internal async Task<EfAppRole> Role(EfApp app, AppRoleName roleName, CancellationToken ct)
    {
        var role = await RolesForAppQuery(app)
            .Where(r => r.Name == roleName.Value)
            .FirstOrDefaultAsync(ct);
        return new EfAppRole
        (
            db,
            role
            ?? throw new Exception($"Role '{roleName.DisplayText}' not found for app '{app.ToModel().AppKey.Format()}'")
        );
    }

    private IQueryable<AppRoleEntity> RolesForAppQuery(EfApp app) =>
        db.Context.Roles.Retrieve()
            .Where(r => r.AppID == app.ID);

    internal Task<EfAppRole[]> AllowedRolesForResourceGroup(EfResourceGroup group, CancellationToken ct) =>
        RolesForResourceGroup(group, true, ct);

    internal Task<EfAppRole[]> DeniedRolesForResourceGroup(EfResourceGroup group, CancellationToken ct) =>
        RolesForResourceGroup(group, false, ct);

    private Task<EfAppRole[]> RolesForResourceGroup(EfResourceGroup group, bool isAllowed, CancellationToken ct)
    {
        var roleIDs = db.Context.ResourceGroupRoles.Retrieve()
            .Where(gr => gr.GroupID == group.ID && gr.IsAllowed == isAllowed)
            .Select(gr => gr.RoleID);
        return db.Context.Roles.Retrieve()
            .Where(r => roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == DateTimeOffset.MaxValue.Year)
            .OrderBy(r => r.Name)
            .Select(r => new EfAppRole(db, r))
            .ToArrayAsync(ct);
    }

    internal Task<EfAppRole[]> RolesNotAssignedToUser(EfAppUser user, EfModifier modifier, CancellationToken ct)
    {
        var appID = AppIDQuery(modifier);
        var roleIDs = UserRoleIDsQuery(user, modifier);
        return db.Context.Roles.Retrieve()
            .Where(r => appID.Contains(r.AppID) && !roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == 9999)
            .OrderBy(r => r.Name)
            .Select(r => new EfAppRole(db, r))
            .ToArrayAsync(ct);
    }

    internal Task<EfAppRole[]> RolesAssignedToUser(EfAppUser user, EfModifier modifier, CancellationToken ct)
    {
        var appID = AppIDQuery(modifier);
        var roleIDs = UserRoleIDsQuery(user, modifier);
        return db.Context.Roles.Retrieve()
            .Where(r => appID.Contains(r.AppID) && roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == 9999)
            .OrderBy(r => r.Name)
            .Select(r => new EfAppRole(db, r))
            .ToArrayAsync(ct);
    }

    private IQueryable<int> AppIDQuery(EfModifier modifier)
    {
        var modCategoryID = db.Context.Modifiers.Retrieve()
            .Where(m => m.ID == modifier.ID)
            .Select(m => m.CategoryID);
        var appIDs = db.Context.ModifierCategories.Retrieve()
            .Where(mc => modCategoryID.Contains(mc.ID))
            .Select(mc => mc.AppID);
        return appIDs;
    }

    private IQueryable<int> UserRoleIDsQuery(EfAppUser user, EfModifier modifier) =>
        db.Context.UserRoles.Retrieve()
            .Where(ur => ur.UserID == user.ID && ur.ModifierID == modifier.ID)
            .Select(ur => ur.RoleID);

    internal Task<EfAppRole[]> AllowedRolesForResource(EfResource resource, CancellationToken ct) =>
        RolesForResource(resource, true, ct);

    internal Task<EfAppRole[]> DeniedRolesForResource(EfResource resource, CancellationToken ct) =>
        RolesForResource(resource, false, ct);

    private Task<EfAppRole[]> RolesForResource(EfResource resource, bool isAllowed, CancellationToken ct)
    {
        var roleIDs = db.Context.ResourceRoles.Retrieve()
            .Where(gr => gr.ResourceID == resource.ID && gr.IsAllowed == isAllowed)
            .Select(gr => gr.RoleID);
        return db.Context.Roles.Retrieve()
            .Where(r => roleIDs.Contains(r.ID) && r.TimeDeactivated.Year == DateTimeOffset.MaxValue.Year)
            .OrderBy(r => r.Name)
            .Select(r => new EfAppRole(db, r))
            .ToArrayAsync(ct);
    }
}