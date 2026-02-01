using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfResourceGroups
{
    private readonly EfHubDB db;

    internal EfResourceGroups(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfResourceGroup> AddOrUpdateResourceGroup(EfApp app, EfVersion version, ResourceGroupName name, EfModifierCategory modCategory, CancellationToken ct)
    {
        var group = await GetGroup(app, version, name, ct);
        if (group == null)
        {
            group = await Add(app, version, name, modCategory, ct);
        }
        else
        {
            await db.Context
                .ResourceGroups
                .Update
                (
                    group,
                    r =>
                    {
                        r.ModCategoryID = modCategory.ID;
                        r.DisplayText = name.DisplayText;
                    },
                    ct
                );
        }
        return new EfResourceGroup(db, group);
    }

    private async Task<ResourceGroupEntity> Add(EfApp app, EfVersion version, ResourceGroupName name, EfModifierCategory modCategory, CancellationToken ct)
    {
        var appVersionIDs = db.Versions.QueryAppVersionID(app, version);
        var appVersionID = await appVersionIDs.FirstAsync(ct);
        var record = new ResourceGroupEntity
        {
            AppVersionID = appVersionID,
            Name = name.Value,
            DisplayText = name.DisplayText,
            ModCategoryID = modCategory.ID
        };
        await db.Context.ResourceGroups.Create(record, ct);
        return record;
    }

    internal Task<EfResourceGroup[]> Groups(EfApp app, EfVersion version, CancellationToken ct)
    {
        var appVersionIDs = db.Versions.QueryAppVersionID(app, version);
        return db.Context
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID))
            .OrderBy(g => g.Name)
            .Select(g => new EfResourceGroup(db, g))
            .ToArrayAsync(ct);
    }

    internal async Task<EfResourceGroup> Group(int id, CancellationToken ct)
    {
        var group = await db.Context.ResourceGroups.Retrieve()
            .Where(rg => rg.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfResourceGroup(db, group ?? throw new Exception($"Group not found with ID {id}"));
    }

    internal async Task<EfResourceGroup> GroupOrDefault(EfApp app, EfVersion version, ResourceGroupName name, CancellationToken ct)
    {
        var group = await GetGroup(app, version, name, ct);
        if (group == null)
        {
            group = await GetDefault(ct);
        }
        return new EfResourceGroup(db, group ?? throw new ArgumentNullException(nameof(group)));
    }

    private Task<ResourceGroupEntity?> GetDefault(CancellationToken ct) =>
        db.Context.ResourceGroups.Retrieve()
            .Where(g => g.Name == ResourceGroupName.Unknown.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<EfResourceGroup> GroupByName(EfApp app, EfVersion version, ResourceGroupName name, CancellationToken ct)
    {
        var group = await GetGroup(app, version, name, ct);
        return new EfResourceGroup(db, group ?? throw new Exception($"Group '{name.DisplayText}' not found"));
    }

    private Task<ResourceGroupEntity?> GetGroup(EfApp app, EfVersion version, ResourceGroupName name, CancellationToken ct)
    {
        var appVersionIDs = db.Versions.QueryAppVersionID(app, version);
        return db.Context.ResourceGroups.Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.Name == name.Value)
            .FirstOrDefaultAsync(ct);
    }

    internal async Task<EfResourceGroup> GroupOrDefault(int appVersionID, ResourceGroupName name, CancellationToken ct)
    {
        var group = await GetGroup(appVersionID, name, ct);
        if (group == null)
        {
            group = await GetDefault(ct);
        }
        return new EfResourceGroup(db, group ?? throw new ArgumentNullException(nameof(group)));
    }

    private Task<ResourceGroupEntity?> GetGroup(int appVersionID, ResourceGroupName name, CancellationToken ct) =>
        db.Context.ResourceGroups.Retrieve()
            .Where(g => g.AppVersionID == appVersionID && g.Name == name.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<EfResourceGroup> GroupForVersion(EfApp app, EfVersion version, int id, CancellationToken ct)
    {
        var appVersionIDs = db.Versions.QueryAppVersionID(app, version);
        var group = await db.Context.ResourceGroups.Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfResourceGroup(db, group ?? throw new Exception($"Group {id} not found for version '{version.Key().DisplayText}"));
    }

    internal Task<EfResourceGroup[]> Groups(EfAppVersion appVersion, EfModifierCategory modCategory, CancellationToken ct)
    {
        var appVersionIDs = appVersion.QueryAppVersionID();
        return db.Context.ResourceGroups.Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.ModCategoryID == modCategory.ID)
            .OrderBy(g => g.Name)
            .Select(g => new EfResourceGroup(db, g))
            .ToArrayAsync(ct);
    }
}