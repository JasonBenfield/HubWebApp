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

    public ResourceGroupName Name() => new ResourceGroupName(record.Name);

    public Task<Resource> AddOrUpdateResource(ResourceName name, ResourceResultType resultType) =>
        factory.Resources.AddOrUpdate(this, name, resultType);

    public Task<Resource> ResourceByName(ResourceName name) =>
        factory.Resources.ResourceByName(this, name);

    public Task<Resource> ResourceOrDefault(ResourceName name) => 
        factory.Resources.ResourceOrDefault(this, name);

    public Task<Resource[]> Resources() => factory.Resources.Resources(this);

    public async Task<IEnumerable<Modifier>> Modifiers()
    {
        var modCategory = await factory.ModCategories.Category(record.ModCategoryID);
        var modifiers = await modCategory.Modifiers();
        return modifiers;
    }

    public Task<ModifierCategory> ModCategory() => 
        factory.ModCategories.Category(record.ModCategoryID);

    public Task AllowAnonymous() => setIsAnonymousAllowed(true);
    public Task DenyAnonymous() => setIsAnonymousAllowed(false);
    private Task setIsAnonymousAllowed(bool isAllowed)
        => factory.DB
            .ResourceGroups
            .Update
            (
                record,
                r =>
                {
                    r.IsAnonymousAllowed = isAllowed;
                }
            );

    public Task<AppRole[]> AllowedRoles()
        => factory.Roles.AllowedRolesForResourceGroup(this);

    public Task SetRoleAccess(IEnumerable<AppRole> allowedRoles)
        => factory.DB.Transaction(() => setRoleAccess(allowedRoles));

    private async Task setRoleAccess(IEnumerable<AppRole> allowedRoles)
    {
        await deleteExistingRoles(allowedRoles);
        var existingAllowedRoles = await AllowedRoles();
        foreach (var allowedRole in allowedRoles)
        {
            if (!existingAllowedRoles.Any(r => r.ID.Equals(allowedRole.ID)))
            {
                await addGroupRole(allowedRole, true);
            }
        }
    }

    private async Task deleteExistingRoles(IEnumerable<AppRole> allowedRoles)
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
            .ToArrayAsync();
        foreach (var groupRole in rolesToDelete)
        {
            await factory.DB.ResourceGroupRoles.Delete(groupRole);
        }
    }

    private Task addGroupRole(AppRole role, bool isAllowed)
        => factory.DB
            .ResourceGroupRoles
            .Create
            (
                new ResourceGroupRoleEntity
                {
                    GroupID = ID,
                    RoleID = role.ID,
                    IsAllowed = isAllowed
                }
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

    public override string ToString() => $"{nameof(ResourceGroup)} {ID}";
}