using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfResourceGroup
{
    private readonly EfHubDB db;
    private readonly ResourceGroupEntity group;

    internal EfResourceGroup(EfHubDB db, ResourceGroupEntity group)
    {
        this.db = db;
        this.group = group;
    }

    public int ID { get => group.ID; }

    public bool NameEquals(ResourceGroupName name) => Name().Equals(name);

    public Task<EfResource> AddOrUpdateResource(ResourceName name, ResourceResultType resultType, CancellationToken ct) =>
        db.Resources.AddOrUpdate(this, name, resultType, ct);

    public Task<EfResource> ResourceByName(ResourceName name, CancellationToken ct) =>
        db.Resources.ResourceByName(this, name, ct);

    public Task<EfResource> ResourceOrDefault(ResourceName name, CancellationToken ct) =>
        db.Resources.ResourceOrDefault(this, name, ct);

    public Task<EfResource[]> Resources(CancellationToken ct) => db.Resources.Resources(this, ct);

    public async Task<EfModifier[]> Modifiers(CancellationToken ct)
    {
        var efCategory = await db.ModCategories.Category(group.ModCategoryID, ct);
        var efModifiers = await efCategory.Modifiers(ct);
        return efModifiers;
    }

    public Task<EfModifierCategory> ModCategory(CancellationToken ct) =>
        db.ModCategories.Category(group.ModCategoryID, ct);

    public Task AllowAnonymous(CancellationToken ct) => setIsAnonymousAllowed(true, ct);

    public Task DenyAnonymous(CancellationToken ct) => setIsAnonymousAllowed(false, ct);

    private Task setIsAnonymousAllowed(bool isAllowed, CancellationToken ct) =>
        db.Context.ResourceGroups.Update
        (
            group,
            r =>
            {
                r.IsAnonymousAllowed = isAllowed;
            },
            ct
        );

    public Task<EfAppRole[]> AllowedRoles(CancellationToken ct) =>
        db.Roles.AllowedRolesForResourceGroup(this, ct);

    public async Task SetRoleAccess(EfAppRole[] efAllowedRoles, CancellationToken ct)
    {
        await DeleteExistingRoles(efAllowedRoles, ct);
        var efExistingAllowedRoles = await AllowedRoles(ct);
        foreach (var allowedRole in efAllowedRoles)
        {
            if (!efExistingAllowedRoles.Any(r => r.ID.Equals(allowedRole.ID)))
            {
                await addGroupRole(allowedRole, true, ct);
            }
        }
    }

    private async Task DeleteExistingRoles(EfAppRole[] efAllowedRoles, CancellationToken ct)
    {
        var allowedRoleIDs = efAllowedRoles.Select(r => r.ID);
        var rolesToDelete = await db.Context.ResourceGroupRoles.Retrieve()
            .Where
            (
                gr => gr.GroupID == ID
                    &&
                    (
                        !allowedRoleIDs.Contains(gr.RoleID) && gr.IsAllowed
                    )
            )
            .ToArrayAsync(ct);
        foreach (var groupRole in rolesToDelete)
        {
            await db.Context.ResourceGroupRoles.Delete(groupRole, ct);
        }
    }

    private Task addGroupRole(EfAppRole role, bool isAllowed, CancellationToken ct) =>
        db.Context.ResourceGroupRoles.Create
        (
            new ResourceGroupRoleEntity
            {
                GroupID = ID,
                RoleID = role.ID,
                IsAllowed = isAllowed
            },
            ct
        );

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        db.Requests.MostRecentForResourceGroup(this, howMany, ct);

    public Task<EfLogEntry[]> MostRecentErrorEvents(int howMany, CancellationToken ct) =>
        db.LogEntries.MostRecentErrorsForResourceGroup(this, howMany, ct);

    public ResourceGroupModel ToModel() =>
        new ResourceGroupModel
        (
            ID: ID,
            Name: Name(),
            IsAnonymousAllowed: group.IsAnonymousAllowed,
            ModCategoryID: group.ModCategoryID
        );

    private ResourceGroupName Name() => new ResourceGroupName(group.DisplayText);

    public override string ToString() => $"{nameof(EfResourceGroup)} {ID}";
}