using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ResourceGroupRepository
{
    private readonly HubFactory factory;

    internal ResourceGroupRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal async Task<ResourceGroup> AddOrUpdateResourceGroup(App app, XtiVersion version, ResourceGroupName name, ModifierCategory modCategory, CancellationToken ct)
    {
        var record = await GetGroup(app, version, name, ct);
        if (record == null)
        {
            record = await Add(app, version, name, modCategory, ct);
        }
        else
        {
            await factory.DB
                .ResourceGroups
                .Update
                (
                    record, 
                    r =>
                    {
                        r.ModCategoryID = modCategory.ID;
                        r.DisplayText = name.DisplayText;
                    },
                    ct
                );
        }
        return factory.CreateGroup(record);
    }

    private async Task<ResourceGroupEntity> Add(App app, XtiVersion version, ResourceGroupName name, ModifierCategory modCategory, CancellationToken ct)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        var appVersionID = await appVersionIDs.FirstAsync(ct);
        var record = new ResourceGroupEntity
        {
            AppVersionID = appVersionID,
            Name = name.Value,
            DisplayText = name.DisplayText,
            ModCategoryID = modCategory.ID
        };
        await factory.DB.ResourceGroups.Create(record, ct);
        return record;
    }

    internal Task<ResourceGroup[]> Groups(App app, XtiVersion version, CancellationToken ct)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        return factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID))
            .OrderBy(g => g.Name)
            .Select(g => factory.CreateGroup(g))
            .ToArrayAsync(ct);
    }

    internal async Task<ResourceGroup> Group(int id, CancellationToken ct)
    {
        var entity = await factory.DB.ResourceGroups.Retrieve()
            .Where(rg=>rg.ID == id)
            .FirstOrDefaultAsync(ct);
        return factory.CreateGroup(entity ?? throw new Exception($"Group not found with ID {id}"));
    }

    internal async Task<ResourceGroup> GroupOrDefault(App app, XtiVersion version, ResourceGroupName name, CancellationToken ct)
    {
        var groupEntity = await GetGroup(app, version, name, ct);
        if (groupEntity == null)
        {
            groupEntity = await GetDefault(ct);
        }
        return factory.CreateGroup(groupEntity ?? throw new ArgumentNullException(nameof(groupEntity)));
    }

    private Task<ResourceGroupEntity?> GetDefault(CancellationToken ct) =>
        factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.Name == ResourceGroupName.Unknown.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<ResourceGroup> GroupByName(App app, XtiVersion version, ResourceGroupName name, CancellationToken ct)
    {
        var record = await GetGroup(app, version, name, ct);
        return factory.CreateGroup(record ?? throw new Exception($"Group '{name.DisplayText}' not found"));
    }

    private Task<ResourceGroupEntity?> GetGroup(App app, XtiVersion version, ResourceGroupName name, CancellationToken ct)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        return factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.Name == name.Value)
            .FirstOrDefaultAsync(ct);
    }

    internal async Task<ResourceGroup> GroupOrDefault(int appVersionID, ResourceGroupName name, CancellationToken ct)
    {
        var groupEntity = await GetGroup(appVersionID, name, ct);
        if (groupEntity == null)
        {
            groupEntity = await GetDefault(ct);
        }
        return factory.CreateGroup(groupEntity ?? throw new ArgumentNullException(nameof(groupEntity)));
    }

    private Task<ResourceGroupEntity?> GetGroup(int appVersionID, ResourceGroupName name, CancellationToken ct) =>
        factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.AppVersionID == appVersionID && g.Name == name.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<ResourceGroup> GroupForVersion(App app, XtiVersion version, int id, CancellationToken ct)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        var record = await factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.ID == id)
            .FirstOrDefaultAsync(ct);
        return factory.CreateGroup(record ?? throw new Exception($"Group {id} not found for version '{version.Key().DisplayText}"));
    }

    internal Task<ResourceGroup[]> Groups(AppVersion appVersion, ModifierCategory modCategory, CancellationToken ct)
    {
        var appVersionIDs = appVersion.QueryAppVersionID();
        return factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.ModCategoryID == modCategory.ID)
            .OrderBy(g => g.Name)
            .Select(g => factory.CreateGroup(g))
            .ToArrayAsync(ct);
    }
}