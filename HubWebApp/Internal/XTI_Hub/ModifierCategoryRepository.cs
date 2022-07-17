using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ModifierCategoryRepository
{
    private readonly HubFactory factory;

    internal ModifierCategoryRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal async Task<ModifierCategory> AddIfNotFound(App app, ModifierCategoryName name)
    {
        var record = await factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.Name == name.Value);
        if (record == null)
        {
            record = await AddModCategory(app, name);
        }
        return factory.ModCategory(record);
    }

    private async Task<ModifierCategoryEntity> AddModCategory(App app, ModifierCategoryName name)
    {
        var record = new ModifierCategoryEntity
        {
            AppID = app.ID,
            Name = name.Value
        };
        await factory.DB.ModifierCategories.Create(record);
        return record;
    }

    public async Task<ModifierCategory> Category(int id)
    {
        var record = await factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.ID == id);
        return factory.ModCategory(record ?? throw new Exception($"Category {id} not found"));
    }

    internal Task<ModifierCategory[]> Categories(App app) =>
        factory.DB
            .ModifierCategories
            .Retrieve()
            .Where(c => c.AppID == app.ID)
            .OrderBy(c => c.Name)
            .Select(c => factory.ModCategory(c))
            .ToArrayAsync();

    internal async Task<ModifierCategory> Category(App app, int id)
    {
        var record = await factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.ID == id);
        return factory.ModCategory(record ?? throw new Exception($"Category {id} not found for app '{app.ToModel().AppKey.Format()}"));
    }

    internal async Task<ModifierCategory> CategoryOrDefault(App app, ModifierCategoryName name)
    {
        var record = await GetCategory(app, name);
        if (record == null)
        {
            record = await GetCategory(app, ModifierCategoryName.Default);
        }
        return factory.ModCategory
        (
            record ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    internal async Task<ModifierCategory> Category(App app, ModifierCategoryName name)
    {
        var record = await GetCategory(app, name);
        return factory.ModCategory
        (
            record ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    private Task<ModifierCategoryEntity?> GetCategory(App app, ModifierCategoryName name) =>
        factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.Name == name.Value);
}