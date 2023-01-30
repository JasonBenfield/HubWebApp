using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class Resource
{
    private readonly HubFactory factory;
    private readonly ResourceEntity record;

    internal Resource(HubFactory factory, ResourceEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ResourceEntity();
        ID = this.record.ID;
    }

    public int ID { get; }

    public ResourceName Name() => new ResourceName(record.Name);

    public Task AllowAnonymous() => setIsAnonymousAllowed(true);

    public Task DenyAnonymous() => setIsAnonymousAllowed(false);

    private Task setIsAnonymousAllowed(bool isAllowed) =>
        factory.DB
            .Resources
            .Update
            (
                record,
                r =>
                {
                    r.IsAnonymousAllowed = isAllowed;
                }
            );

    public Task<AppRole[]> AllowedRoles() => factory.Roles.AllowedRolesForResource(this);

    public Task SetRoleAccess(IEnumerable<AppRole> allowedRoles) =>
        factory.DB.Transaction(() => setRoleAccess(allowedRoles));

    private async Task setRoleAccess(IEnumerable<AppRole> allowedRoles)
    {
        await deleteExistingRoles(allowedRoles);
        var existingAllowedRoles = await AllowedRoles();
        foreach (var allowedRole in allowedRoles)
        {
            if (!existingAllowedRoles.Any(r => r.ID.Equals(allowedRole.ID)))
            {
                await addResourceRole(allowedRole, true);
            }
        }
    }

    private async Task deleteExistingRoles(IEnumerable<AppRole> allowedRoles)
    {
        var allowedRoleIDs = allowedRoles.Select(r => r.ID);
        var rolesToDelete = await factory.DB
            .ResourceRoles
            .Retrieve()
            .Where
            (
                rr => rr.ResourceID == ID
                    &&
                    (
                        !allowedRoleIDs.Contains(rr.RoleID) && rr.IsAllowed
                    )
            )
            .ToArrayAsync();
        foreach (var resourceRole in rolesToDelete)
        {
            await factory.DB.ResourceRoles.Delete(resourceRole);
        }
    }

    private Task addResourceRole(AppRole role, bool isAllowed) =>
        factory.DB
            .ResourceRoles
            .Create
            (
                new ResourceRoleEntity
                {
                    ResourceID = ID,
                    RoleID = role.ID,
                    IsAllowed = isAllowed
                }
            );

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany) =>
        factory.Requests.MostRecentForResource(this, howMany);

    public Task<LogEntry[]> MostRecentErrorEvents(int howMany) =>
        factory.LogEntries.MostRecentErrorsForResource(this, howMany);

    public Task<ResourceGroup> Group() => factory.Groups.Group(record.GroupID);

    public ResourceModel ToModel() =>
        new ResourceModel
        {
            ID = ID,
            Name = Name(),
            IsAnonymousAllowed = record.IsAnonymousAllowed,
            ResultType = ResourceResultType.Values.Value(record.ResultType)
        };

    public override string ToString() => $"{nameof(Resource)} {ID}";
}