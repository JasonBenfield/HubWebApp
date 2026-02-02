using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfModifierCategories
{
    private readonly EfHubDB db;

    internal EfModifierCategories(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfModifierCategory> AddOrUpdate(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var category = await db.Context.ModifierCategories.Retrieve()
            .Where(c => c.AppID == app.ID && c.Name == name.Value)
            .FirstOrDefaultAsync(ct);
        if (category == null)
        {
            category = await AddModCategory(app, name, ct);
        }
        else
        {
            await db.Context.ModifierCategories.Update
            (
                category,
                c => c.DisplayText = name.DisplayText,
                ct
            );
        }
        return db.ModCategory(category);
    }

    private async Task<ModifierCategoryEntity> AddModCategory(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var category = new ModifierCategoryEntity
        {
            AppID = app.ID,
            Name = name.Value
        };
        await db.Context.ModifierCategories.Create(category, ct);
        return category;
    }

    public async Task<EfModifierCategory> Category(int id, CancellationToken ct)
    {
        var category = await db.Context.ModifierCategories.Retrieve()
            .Where(c => c.ID == id)
            .FirstOrDefaultAsync(ct);
        return db.ModCategory(category ?? throw new Exception($"Category {id} not found"));
    }

    internal Task<EfModifierCategory[]> Categories(EfApp app, CancellationToken ct) =>
        db.Context.ModifierCategories.Retrieve()
            .Where(c => c.AppID == app.ID)
            .OrderBy(c => c.Name)
            .Select(c => db.ModCategory(c))
            .ToArrayAsync(ct);

    internal async Task<EfModifierCategory> Category(EfApp app, int id, CancellationToken ct)
    {
        var category = await db.Context.ModifierCategories.Retrieve()
            .Where(c => c.AppID == app.ID && c.ID == id)
            .FirstOrDefaultAsync(ct);
        return db.ModCategory(category ?? throw new Exception($"Category {id} not found for app '{app.ToModel().AppKey.Format()}"));
    }

    internal async Task<EfModifierCategory> CategoryOrDefault(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var category = await GetCategory(app, name, ct);
        if (category == null)
        {
            category = await GetCategory(app, ModifierCategoryName.Default, ct);
        }
        return db.ModCategory
        (
            category ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    internal async Task<EfModifierCategory> Category(EfApp app, ModifierCategoryName name, CancellationToken ct)
    {
        var category = await GetCategory(app, name, ct);
        return db.ModCategory
        (
            category ?? throw new Exception($"Category '{name.DisplayText}' not found")
        );
    }

    private Task<ModifierCategoryEntity?> GetCategory(EfApp app, ModifierCategoryName name, CancellationToken ct) =>
        db.Context.ModifierCategories.Retrieve()
            .Where(c => c.AppID == app.ID && c.Name == name.Value)
            .FirstOrDefaultAsync(ct);
}