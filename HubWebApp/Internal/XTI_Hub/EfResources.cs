using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfResources
{
    private readonly EfHubDB db;

    internal EfResources(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfResource> Resource(int id, CancellationToken ct)
    {
        var resource = await db.Context.Resources.Retrieve()
            .Where(r => r.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfResource(db, resource ?? throw new Exception($"Resource not found with ID {id}"));
    }

    public async Task<EfResource> AddOrUpdate(EfResourceGroup group, ResourceName name, ResourceResultType resultType, CancellationToken ct)
    {
        var resource = await db.Context.Resources.Retrieve()
            .Where(r => r.GroupID == group.ID && r.Name == name.Value)
            .FirstOrDefaultAsync(ct);
        if (resource == null)
        {
            resource = new ResourceEntity
            {
                GroupID = group.ID,
                Name = name.Value,
                DisplayText = name.DisplayText,
                ResultType = resultType.Value
            };
            await db.Context.Resources.Create(resource, ct);
        }
        else
        {
            await db.Context.Resources.Update
            (
                resource,
                r =>
                {
                    r.DisplayText = name.DisplayText;
                    r.ResultType = resultType.Value;
                },
                ct
            );
        }
        return new EfResource(db, resource);
    }

    public Task<EfResource[]> Resources(EfResourceGroup group, CancellationToken ct) =>
        db.Context.Resources.Retrieve()
            .Where(r => r.GroupID == group.ID)
            .OrderBy(r => r.ResultType)
            .ThenBy(r => r.Name)
            .Select(r => new EfResource(db, r))
            .ToArrayAsync(ct);

    internal async Task<EfResource> ResourceOrDefault(EfResourceGroup group, ResourceName name, CancellationToken ct)
    {
        var resource = await GetResource(group, name, ct);
        if (resource == null)
        {
            resource = await GetResource(group, ResourceName.Unknown, ct);
            if (resource == null)
            {
                resource = await db.Context.Resources.Retrieve()
                    .Where(r => r.Name == ResourceName.Unknown.Value)
                    .FirstOrDefaultAsync(ct);
            }
        }
        return new EfResource(db, resource ?? throw new ArgumentNullException(nameof(resource)));
    }

    internal async Task<EfResource> ResourceByName(EfResourceGroup group, ResourceName name, CancellationToken ct)
    {
        var resource = await GetResource(group, name, ct);
        return new EfResource
        (
            db,
            resource ??
            throw new Exception($"Resource '{name.DisplayText}' not found for group '{group.ToModel().Name.DisplayText}'")
        );
    }

    private Task<ResourceEntity?> GetResource(EfResourceGroup group, ResourceName name, CancellationToken ct) =>
        db.Context.Resources.Retrieve()
            .Where(r => r.GroupID == group.ID && r.Name == name.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<EfResource> ResourceForVersion(EfApp app, EfVersion version, int id, CancellationToken ct)
    {
        var appVersionIDs = db.Versions.QueryAppVersionID(app, version);
        var groupIDs = db.Context.ResourceGroups.Retrieve()
            .Where(rg => appVersionIDs.Contains(rg.AppVersionID))
            .Select(rg => rg.ID);
        var resource = await db.Context.Resources.Retrieve()
            .Where(r => groupIDs.Contains(r.GroupID) && r.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfResource(db, resource ?? throw new Exception($"Resource {id} not found for version '{version.Key().DisplayText}"));
    }

}