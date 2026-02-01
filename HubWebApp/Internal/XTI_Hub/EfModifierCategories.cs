using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfModifierCategories
{
    private readonly EfHubDB factory;

    internal EfModifierCategories(EfHubDB factory)
    {
        this.factory = factory;
    }

    internal async Task<EfModifierCategory> AddOrUpdate(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = await factory.Context
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.Name == name.Value, ct);
        if (record == null)
        {
            record = await AddModCategory(app, name, ct);
        }
        else
        {
            await factory.Context.ModifierCategories.Update
            (
                record, 
                c => c.DisplayText = name.DisplayText,
                ct
            );
        }
        return factory.ModCategory(record);
    }

    private async Task<ModifierCategoryEntity> AddModCategory(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = new ModifierCategoryEntity
        {
            AppID = app.ID,
            Name = name.Value
        };
        await factory.Context.ModifierCategories.Create(record, ct);
        return record;
    }

    public async Task<EfModifierCategory> Category(int id, CancellationToken ct)
    {
        var record = await factory.Context
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.ID == id, ct);
        return factory.ModCategory(record ?? throw new Exception($"Category {id} not found"));
    }

    internal Task<EfModifierCategory[]> Categories(EfApp app, CancellationToken ct) =>
        factory.Context
            .ModifierCategories
            .Retrieve()
            .Where(c => c.AppID == app.ID)
            .OrderBy(c => c.Name)
            .Select(c => factory.ModCategory(c))
            .ToArrayAsync(ct);

    internal async Task<EfModifierCategory> Category(EfApp app, int id, CancellationToken ct)
    {
        var record = await factory.Context
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.ID == id, ct);
        return factory.ModCategory(record ?? throw new Exception($"Category {id} not found for app '{app.ToModel().AppKey.Format()}"));
    }

    internal async Task<EfModifierCategory> CategoryOrDefault(EfApp app, ModifierCategoryName name, CancellationToken ct)
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

    internal async Task<EfModifierCategory> Category(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var record = await GetCategory(app, name, ct);
        return factory.ModCategory
        (
            record ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    private Task<ModifierCategoryEntity?> GetCategory(EfApp app, ModifierCategoryName name, CancellationToken ct) =>
        factory.Context
            .ModifierCategories
            .Retrieve()
            .FirstOrDefaultAsync(c => c.AppID == app.ID && c.Name == name.Value, ct);
}