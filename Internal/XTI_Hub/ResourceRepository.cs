using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ResourceRepository
{
    private readonly AppFactory factory;

    internal ResourceRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    public async Task<Resource> AddOrUpdate(ResourceGroup group, ResourceName name, ResourceResultType resultType)
    {
        var record = await factory.DB
            .Resources.Retrieve()
            .FirstOrDefaultAsync(r => r.GroupID == group.ID.Value && r.Name == name.Value);
        if (record == null)
        {
            record = new ResourceEntity
            {
                GroupID = group.ID.Value,
                Name = name.Value,
                ResultType = resultType.Value
            };
            await factory.DB.Resources.Create(record);
        }
        else
        {
            await factory.DB
                .Resources
                .Update
                (
                    record,
                    r =>
                    {
                        r.ResultType = resultType.Value;
                    }
                );
        }
        return factory.CreateResource(record);
    }

    public Task<Resource[]> Resources(ResourceGroup group) => 
        factory.DB
            .Resources
            .Retrieve()
            .Where(r => r.GroupID == group.ID.Value)
            .OrderBy(r => r.ResultType)
            .ThenBy(r => r.Name)
            .Select(r => factory.CreateResource(r))
            .ToArrayAsync();

    internal async Task<Resource> ResourceOrDefault(ResourceGroup group, ResourceName name)
    {
        var record = await GetResource(group, name);
        if (record == null)
        {
            record = await GetResource(group, ResourceName.Unknown);
            if (record == null)
            {
                record = await factory.DB
                    .Resources
                    .Retrieve()
                   .FirstOrDefaultAsync(r => r.Name == ResourceName.Unknown.Value);
            }
        }
        return factory.CreateResource(record ?? throw new ArgumentNullException(nameof(record)));
    }

    internal async Task<Resource> ResourceByName(ResourceGroup group, ResourceName name)
    {
        var record = await GetResource(group, name);
        return factory.CreateResource
        (
            record ?? 
            throw new Exception($"Resource '{name.DisplayText}' not found for group '{group.Name().DisplayText}'")
        );
    }

    private Task<ResourceEntity?> GetResource(ResourceGroup group, ResourceName name) =>
        factory.DB
            .Resources
            .Retrieve()
            .FirstOrDefaultAsync(r => r.GroupID == group.ID.Value && r.Name == name.Value);

    internal async Task<Resource> Resource(AppVersion version, int id)
    {
        var groupIDs = factory.DB
            .ResourceGroups
            .Retrieve()
            .Where(rg => rg.VersionID == version.ID.Value)
            .Select(rg => rg.ID);
        var record = await factory.DB
            .Resources
            .Retrieve()
            .FirstOrDefaultAsync(r => r.ID == id && groupIDs.Any(gID => gID == r.GroupID));
        return factory.CreateResource(record ?? throw new Exception($"Resource {id} not found for version '{version.Key().DisplayText}"));
    }
}