using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfResource
{
    private readonly EfHubDB db;
    private readonly ResourceEntity resource;

    internal EfResource(EfHubDB db, ResourceEntity resource)
    {
        this.db = db;
        this.resource = resource;
    }

    public int ID { get => resource.ID; }

    public Task AllowAnonymous(CancellationToken ct) => SetIsAnonymousAllowed(true, ct);

    public Task DenyAnonymous(CancellationToken ct) => SetIsAnonymousAllowed(false, ct);

    private Task SetIsAnonymousAllowed(bool isAllowed, CancellationToken ct) =>
        db.Context.Resources.Update
        (
            resource,
            r =>
            {
                r.IsAnonymousAllowed = isAllowed;
            },
            ct
        );

    public Task<EfAppRole[]> AllowedRoles(CancellationToken ct) => db.Roles.AllowedRolesForResource(this, ct);

    public async Task SetRoleAccess(EfAppRole[] efAllowedRoles, CancellationToken ct)
    {
        await DeleteExistingRoles(efAllowedRoles, ct);
        var efExistingAllowedRoles = await AllowedRoles(ct);
        foreach (var efAllowedRole in efAllowedRoles)
        {
            if (!efExistingAllowedRoles.Any(r => r.ID.Equals(efAllowedRole.ID)))
            {
                await AddResourceRole(efAllowedRole, true, ct);
            }
        }
    }

    private async Task DeleteExistingRoles(EfAppRole[] efAllowedRoles, CancellationToken ct)
    {
        var allowedRoleIDs = efAllowedRoles.Select(r => r.ID);
        var rolesToDelete = await db.Context.ResourceRoles.Retrieve()
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
            await db.Context.ResourceRoles.Delete(resourceRole, ct);
        }
    }

    private Task AddResourceRole(EfAppRole role, bool isAllowed, CancellationToken ct) =>
        db.Context.ResourceRoles.Create
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
        db.Requests.MostRecentForResource(this, howMany, ct);

    public Task<EfLogEntry[]> MostRecentErrorEvents(int howMany, CancellationToken ct) =>
        db.LogEntries.MostRecentErrorsForResource(this, howMany, ct);

    public Task<EfResourceGroup> Group(CancellationToken ct) => db.Groups.Group(resource.GroupID, ct);

    public ResourceModel ToModel() =>
        new ResourceModel
        (
            ID: ID,
            Name: Name(),
            IsAnonymousAllowed: resource.IsAnonymousAllowed,
            ResultType: ResourceResultType.Values.Value(resource.ResultType)
        );

    private ResourceName Name() => new ResourceName(resource.DisplayText);

    public override string ToString() => $"{nameof(EfResource)} {ID}";
}