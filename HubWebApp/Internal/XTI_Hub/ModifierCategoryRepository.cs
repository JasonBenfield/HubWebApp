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

    internal async Task<ModifierCategory> AddOrUpdate(App app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = await factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.Name == name.Value, ct);
        if (record == null)
        {
            record = await AddModCategory(app, name, ct);
        }
        else
        {
            await factory.DB.ModifierCategories.Update
            (
                record, 
                c => c.DisplayText = name.DisplayText,
                ct
            );
        }
        return factory.ModCategory(record);
    }

    private async Task<ModifierCategoryEntity> AddModCategory(App app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = new ModifierCategoryEntity
        {
            AppID = app.ID,
            Name = name.Value
        };
        await factory.DB.ModifierCategories.Create(record, ct);
        return record;
    }

    public async Task<ModifierCategory> Category(int id, CancellationToken ct)
    {
        var record = await factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.ID == id, ct);
        return factory.ModCategory(record ?? throw new Exception($"Category {id} not found"));
    }

    internal Task<ModifierCategory[]> Categories(App app, CancellationToken ct) =>
        factory.DB
            .ModifierCategories
            .Retrieve()
            .Where(c => c.AppID == app.ID)
            .OrderBy(c => c.Name)
            .Select(c => factory.ModCategory(c))
            .ToArrayAsync(ct);

    internal async Task<ModifierCategory> Category(App app, int id, CancellationToken ct)
    {
        var record = await factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.ID == id, ct);
        return factory.ModCategory(record ?? throw new Exception($"Category {id} not found for app '{app.ToModel().AppKey.Format()}"));
    }

    internal async Task<ModifierCategory> CategoryOrDefault(App app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = await GetCategory(app, name, ct);
        if (record == null)
        {
            record = await GetCategory(app, ModifierCategoryName.Default, ct);
        }
        return factory.ModCategory
        (
            record ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    internal async Task<ModifierCategory> Category(App app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = await GetCategory(app, name, ct);
        return factory.ModCategory
        (
            record ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    private Task<ModifierCategoryEntity?> GetCategory(App app, ModifierCategoryName name, CancellationToken ct) =>
        factory.DB
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.Name == name.Value, ct);
}