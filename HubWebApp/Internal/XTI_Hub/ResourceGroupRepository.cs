using Microsoft.EntityFrameworkCore;
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

    internal async Task<ResourceGroup> AddOrUpdateResourceGroup(App app, XtiVersion version, ResourceGroupName name, ModifierCategory modCategory)
    {
        var record = await GetGroup(app, version, name);
        if (record == null)
        {
            record = await Add(app, version, name, modCategory);
        }
        else
        {
            await factory.DB
                .ResourceGroups
                .Update
                (
                    record, r =>
                    {
                        r.ModCategoryID = modCategory.ID;
                    }
                );
        }
        return factory.CreateGroup(record);
    }

    private async Task<ResourceGroupEntity> Add(App app, XtiVersion version, ResourceGroupName name, ModifierCategory modCategory)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        var appVersionID = await appVersionIDs.FirstAsync();
        var record = new ResourceGroupEntity
        {
            AppVersionID = appVersionID,
            Name = name.Value,
            ModCategoryID = modCategory.ID
        };
        await factory.DB.ResourceGroups.Create(record);
        return record;
    }

    internal Task<ResourceGroup[]> Groups(App app, XtiVersion version)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        return factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID))
            .OrderBy(g => g.Name)
            .Select(g => factory.CreateGroup(g))
            .ToArrayAsync();
    }

    internal async Task<ResourceGroup> GroupOrDefault(App app, XtiVersion version, ResourceGroupName name)
    {
        var record = await GetGroup(app, version, name);
        if (record == null)
        {
            record = await GetGroup(app, version, name);
            if (record == null)
            {
                record = await factory.DB
                    .ResourceGroups
                    .Retrieve()
                    .Where(g => g.Name == ResourceGroupName.Unknown.Value)
                    .FirstOrDefaultAsync();
            }
        }
        return factory.CreateGroup(record ?? throw new ArgumentNullException(nameof(record)));
    }

    internal async Task<ResourceGroup> GroupByName(App app, XtiVersion version, ResourceGroupName name)
    {
        var record = await GetGroup(app, version, name);
        return factory.CreateGroup(record ?? throw new Exception($"Group '{name.DisplayText}' not found"));
    }

    private Task<ResourceGroupEntity?> GetGroup(App app, XtiVersion version, ResourceGroupName name)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        return factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.Name == name.Value)
            .FirstOrDefaultAsync();
    }

    internal async Task<ResourceGroup> GroupForVersion(App app, XtiVersion version, int id)
    {
        var appVersionIDs = factory.Versions.QueryAppVersionID(app, version);
        var record = await factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => appVersionIDs.Contains(g.AppVersionID) && g.ID == id)
            .FirstOrDefaultAsync();
        return factory.CreateGroup(record ?? throw new Exception($"Group {id} not found for version '{version.Key().DisplayText}"));
    }

    public async Task<ResourceGroup> Group(int id)
    {
        var record = await factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.ID == id)
            .FirstOrDefaultAsync();
        return factory.CreateGroup(record ?? throw new Exception($"Group {id} not found"));
    }

    internal Task<ResourceGroup[]> Groups(ModifierCategory modCategory) =>
        factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.ModCategoryID == modCategory.ID)
            .OrderBy(g => g.Name)
            .Select(g => factory.CreateGroup(g))
            .ToArrayAsync();
}