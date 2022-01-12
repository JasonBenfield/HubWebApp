using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ResourceGroupRepository
{
    private readonly AppFactory factory;

    internal ResourceGroupRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    internal async Task<ResourceGroup> AddOrUpdateResourceGroup(AppVersion version, ResourceGroupName name, ModifierCategory modCategory)
    {
        var record = await GetGroup(version, name);
        if (record == null)
        {
            record = await Add(version, name, modCategory);
        }
        else
        {
            await factory.DB
                .ResourceGroups
                .Update
                (
                    record, r =>
                    {
                        r.ModCategoryID = modCategory.ID.Value;
                    }
                );
        }
        return factory.CreateGroup(record);
    }

    private async Task<ResourceGroupEntity> Add(AppVersion version, ResourceGroupName name, ModifierCategory modCategory)
    {
        var record = new ResourceGroupEntity
        {
            VersionID = version.ID.Value,
            Name = name.Value,
            ModCategoryID = modCategory.ID.Value
        };
        await factory.DB.ResourceGroups.Create(record);
        return record;
    }

    internal Task<ResourceGroup[]> Groups(AppVersion version) =>
        factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.VersionID == version.ID.Value)
            .OrderBy(g => g.Name)
            .Select(g => factory.CreateGroup(g))
            .ToArrayAsync();

    internal async Task<ResourceGroup> GroupOrDefault(AppVersion version, ResourceGroupName name)
    {
        var record = await GetGroup(version, name);
        if (record == null)
        {
            record = await GetGroup(version, name);
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

    internal async Task<ResourceGroup> GroupByName(AppVersion version, ResourceGroupName name)
    {
        var record = await GetGroup(version, name);
        return factory.CreateGroup(record ?? throw new Exception($"Group '{name.DisplayText}' not found"));
    }

    private Task<ResourceGroupEntity?> GetGroup(AppVersion version, ResourceGroupName name) =>
        factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.VersionID == version.ID.Value && g.Name == name.Value)
            .FirstOrDefaultAsync();

    internal async Task<ResourceGroup> GroupForVersion(AppVersion version, int id)
    {
        var record = await factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(g => g.VersionID == version.ID.Value && g.ID == id)
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
            .Where(g => g.ModCategoryID == modCategory.ID.Value)
            .OrderBy(g => g.Name)
            .Select(g => factory.CreateGroup(g))
            .ToArrayAsync();
}