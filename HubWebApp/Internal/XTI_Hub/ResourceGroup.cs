using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ResourceGroup
{
    private readonly HubFactory factory;
    private readonly ResourceGroupEntity record;

    internal ResourceGroup(HubFactory factory, ResourceGroupEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ResourceGroupEntity();
        ID = this.record.ID;
    }

    public int ID { get; }

    public bool NameEquals(ResourceGroupName name) => Name().Equals(name);

    public Task<Resource> AddOrUpdateResource(ResourceName name, ResourceResultType resultType) =>
        factory.Resources.AddOrUpdate(this, name, resultType);

    public Task<Resource> ResourceByName(ResourceName name) =>
        factory.Resources.ResourceByName(this, name);

    public Task<Resource> ResourceOrDefault(ResourceName name) =>
        factory.Resources.ResourceOrDefault(this, name);

    public Task<Resource[]> Resources() => factory.Resources.Resources(this);

    public async Task<IEnumerable<Modifier>> Modifiers(CancellationToken ct)
    {
        var modCategory = await factory.ModCategories.Category(record.ModCategoryID, ct);
        var modifiers = await modCategory.Modifiers(ct);
        return modifiers;
    }

    public Task<ModifierCategory> ModCategory(CancellationToken ct) =>
        factory.ModCategories.Category(record.ModCategoryID, ct);

    public Task AllowAnonymous(CancellationToken ct) => setIsAnonymousAllowed(true, ct);

    public Task DenyAnonymous(CancellationToken ct) => setIsAnonymousAllowed(false, ct);

    private Task setIsAnonymousAllowed(bool isAllowed, CancellationToken ct)
        => factory.DB
            .ResourceGroups
            .Update
            (
                record,
                r =>
                {
                    r.IsAnonymousAllowed = isAllowed;
                },
                ct
            );

    public Task<AppRole[]> AllowedRoles(CancellationToken ct)
        => factory.Roles.AllowedRolesForResourceGroup(this, ct);

    public Task SetRoleAccess(IEnumerable<AppRole> allowedRoles, CancellationToken ct)
        => factory.DB.Transaction(() => setRoleAccess(allowedRoles, ct));

    private async Task setRoleAccess(IEnumerable<AppRole> allowedRoles, CancellationToken ct)
    {
        await deleteExistingRoles(allowedRoles, ct);
        var existingAllowedRoles = await AllowedRoles(ct);
        foreach (var allowedRole in allowedRoles)
        {
            if (!existingAllowedRoles.Any(r => r.ID.Equals(allowedRole.ID)))
            {
                await addGroupRole(allowedRole, true, ct);
            }
        }
    }

    private async Task deleteExistingRoles(IEnumerable<AppRole> allowedRoles, CancellationToken ct)
    {
        var allowedRoleIDs = allowedRoles.Select(r => r.ID);
        var rolesToDelete = await factory.DB
            .ResourceGroupRoles
            .Retrieve()
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
            await factory.DB.ResourceGroupRoles.Delete(groupRole, ct);
        }
    }

    private Task addGroupRole(AppRole role, bool isAllowed, CancellationToken ct)
        => factory.DB
            .ResourceGroupRoles
            .Create
            (
                new ResourceGroupRoleEntity
                {
                    GroupID = ID,
                    RoleID = role.ID,
                    IsAllowed = isAllowed
                },
                ct
            );

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany)
        => factory.Requests.MostRecentForResourceGroup(this, howMany);

    public Task<LogEntry[]> MostRecentErrorEvents(int howMany)
        => factory.LogEntries.MostRecentErrorsForResourceGroup(this, howMany);

    public ResourceGroupModel ToModel()
        => new ResourceGroupModel
        {
            ID = ID,
            Name = Name(),
            IsAnonymousAllowed = record.IsAnonymousAllowed,
            ModCategoryID = record.ModCategoryID
        };

    private ResourceGroupName Name() => new ResourceGroupName(record.DisplayText);

    public override string ToString() => $"{nameof(ResourceGroup)} {ID}";
}