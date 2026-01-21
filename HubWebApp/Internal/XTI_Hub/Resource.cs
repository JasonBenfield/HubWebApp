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
        this.record = record;
        ID = this.record.ID;
    }

    public int ID { get; }

    public Task AllowAnonymous(CancellationToken ct) => setIsAnonymousAllowed(true, ct);

    public Task DenyAnonymous(CancellationToken ct) => setIsAnonymousAllowed(false, ct);

    private Task setIsAnonymousAllowed(bool isAllowed, CancellationToken ct) =>
        factory.DB
            .Resources
            .Update
            (
                record,
                r =>
                {
                    r.IsAnonymousAllowed = isAllowed;
                },
                ct
            );

    public Task<AppRole[]> AllowedRoles(CancellationToken ct) => factory.Roles.AllowedRolesForResource(this, ct);

    public Task SetRoleAccess(IEnumerable<AppRole> allowedRoles, CancellationToken ct) =>
        factory.DB.Transaction(() => setRoleAccess(allowedRoles, ct));

    private async Task setRoleAccess(IEnumerable<AppRole> allowedRoles, CancellationToken ct)
    {
        await deleteExistingRoles(allowedRoles, ct);
        var existingAllowedRoles = await AllowedRoles(ct);
        foreach (var allowedRole in allowedRoles)
        {
            if (!existingAllowedRoles.Any(r => r.ID.Equals(allowedRole.ID)))
            {
                await addResourceRole(allowedRole, true, ct);
            }
        }
    }

    private async Task deleteExistingRoles(IEnumerable<AppRole> allowedRoles, CancellationToken ct)
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
            .ToArrayAsync(ct);
        foreach (var resourceRole in rolesToDelete)
        {
            await factory.DB.ResourceRoles.Delete(resourceRole, ct);
        }
    }

    private Task addResourceRole(AppRole role, bool isAllowed, CancellationToken ct) =>
        factory.DB
            .ResourceRoles
            .Create
            (
                new ResourceRoleEntity
                {
                    ResourceID = ID,
                    RoleID = role.ID,
                    IsAllowed = isAllowed
                },
                ct
            );

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        factory.Requests.MostRecentForResource(this, howMany, ct);

    public Task<LogEntry[]> MostRecentErrorEvents(int howMany, CancellationToken ct) =>
        factory.LogEntries.MostRecentErrorsForResource(this, howMany, ct);

    public Task<ResourceGroup> Group(CancellationToken ct) => factory.Groups.Group(record.GroupID, ct);

    public ResourceModel ToModel() =>
        new ResourceModel
        {
            ID = ID,
            Name = Name(),
            IsAnonymousAllowed = record.IsAnonymousAllowed,
            ResultType = ResourceResultType.Values.Value(record.ResultType)
        };

    private ResourceName Name() => new ResourceName(record.DisplayText);

    public override string ToString() => $"{nameof(Resource)} {ID}";
}