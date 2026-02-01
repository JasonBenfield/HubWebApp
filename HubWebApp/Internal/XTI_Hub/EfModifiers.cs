using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfModifiers
{
    private readonly EfHubDB db;

    internal EfModifiers(EfHubDB db)
    {
        this.db = db;
    }

    internal Task<EfModifier> AddDefaultModifierIfNotFound(EfModifierCategory category, CancellationToken ct) =>
        AddOrUpdateByModKey(category, ModifierKey.Default, "", "", ct);

    internal async Task<EfModifier> AddOrUpdateByModKey(EfModifierCategory category, ModifierKey modKey, string targetKey, string displayText, CancellationToken ct)
    {
        var modifier = await db.Context.Modifiers.Retrieve()
            .Where(m => m.CategoryID == category.ID && (m.ModKey == modKey.Value || m.TargetKey == targetKey))
            .FirstOrDefaultAsync(ct);
        if (modifier == null)
        {
            modifier = await Add(category, modKey, targetKey, displayText, ct);
        }
        else
        {
            await db.Context.Modifiers.Update
            (
                modifier,
                m =>
                {
                    m.ModKey = modKey.Value;
                    m.ModKeyDisplayText = modKey.DisplayText;
                    m.TargetKey = targetKey;
                    m.DisplayText = displayText;
                },
                ct
            );
        }
        return new EfModifier(db, modifier);
    }

    internal async Task<EfModifier> AddOrUpdateByTargetKey(EfModifierCategory category, IGeneratedKey generatedModKey, string targetKey, string displayText, CancellationToken ct)
    {
        var modifier = await GetModifierByTargetKey(category, targetKey, ct);
        if (modifier == null)
        {
            var modKey = await GenerateModKey(category, generatedModKey, ct);
            modifier = await Add(category, modKey, targetKey, displayText, ct);
        }
        else
        {
            await db.Context.Modifiers.Update
            (
                modifier,
                m =>
                {
                    m.DisplayText = displayText;
                },
                ct
            );
        }
        return new EfModifier(db, modifier);
    }

    private async Task<ModifierKey> GenerateModKey(EfModifierCategory category, IGeneratedKey generatedModKey, CancellationToken ct)
    {
        var modKey = new ModifierKey(generatedModKey.Value());
        var existingModifier = await GetModifierByModKey(category, modKey, ct);
        if (existingModifier != null && generatedModKey is FixedGeneratedKey)
        {
            throw new Exception("Unable to generate a unique key");
        }
        int keyAttempts = 0;
        while (existingModifier != null)
        {
            modKey = new ModifierKey(generatedModKey.Value());
            keyAttempts++;
            if (keyAttempts > 100)
            {
                throw new Exception("Unable to generate a unique key");
            }
            existingModifier = await GetModifierByModKey(category, modKey, ct);
        }
        return modKey;
    }

    private async Task<ModifierEntity> Add(EfModifierCategory category, ModifierKey modKey, string targetID, string displayText, CancellationToken ct)
    {
        var modifier = new ModifierEntity
        {
            CategoryID = category.ID,
            ModKey = modKey.Value,
            ModKeyDisplayText = modKey.DisplayText,
            TargetKey = targetID,
            DisplayText = displayText
        };
        await db.Context.Modifiers.Create(modifier, ct);
        return modifier;
    }

    internal Task<EfModifier[]> Modifiers(EfModifierCategory category, CancellationToken ct) =>
        ModifiersForCategoryQuery(category)
            .Select(m => new EfModifier(db, m))
            .ToArrayAsync(ct);

    public async Task<EfModifier> Modifier(int id, CancellationToken ct)
    {
        var modifier = await db.Context.Modifiers.Retrieve()
            .Where(m => m.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfModifier(db, modifier ?? throw new Exception($"Modifier {id} not found"));
    }

    internal async Task<EfModifier> ModifierByModKey(EfModifierCategory efModCategory, ModifierKey modKey, CancellationToken ct)
    {
        if (!efModCategory.IsDefault() && modKey.Equals(ModifierKey.Default))
        {
            var app = await efModCategory.App(ct);
            efModCategory = await app.ModCategory(ModifierCategoryName.Default, ct);
        }
        var modifier = await GetModifierByModKey(efModCategory, modKey, ct);
        return new EfModifier
        (
            db,
            modifier ?? throw new ModifierNotFoundException(modKey, efModCategory)
        );
    }

    private Task<ModifierEntity?> GetModifierByModKey(EfModifierCategory modCategory, ModifierKey modKey, CancellationToken ct) =>
        db.Context.Modifiers.Retrieve()
            .Where(m => m.CategoryID == modCategory.ID && m.ModKey == modKey.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<EfModifier> ModifierOrDefault(EfModifierCategory modCategory, ModifierKey modKey, CancellationToken ct)
    {
        var efApp = await modCategory.App(ct);
        if (!modCategory.IsDefault() && modKey.Equals(ModifierKey.Default))
        {
            modCategory = await efApp.ModCategory(ModifierCategoryName.Default, ct);
        }
        var modifier = await GetModifierByModKey(modCategory, modKey, ct);
        EfModifier efModifier;
        if (modifier == null)
        {
            efModifier = await efApp.DefaultModifier(ct);
        }
        else
        {
            efModifier = new EfModifier(db, modifier);
        }
        return efModifier;
    }

    internal async Task<EfModifier> ModifierForApp(EfApp app, int modifierID, CancellationToken ct)
    {
        var categoryIDs = db.Context.ModifierCategories.Retrieve()
            .Where(modCat => modCat.AppID == app.ID)
            .Select(modCat => modCat.ID);
        var modifier = await db.Context.Modifiers.Retrieve()
            .Where(m => categoryIDs.Contains(m.CategoryID) && m.ID == modifierID)
            .FirstOrDefaultAsync(ct);
        return new EfModifier
        (
            db,
            modifier ?? throw new ModifierNotFoundException(modifierID, app)
        );
    }

    internal async Task<EfModifier[]> ModifiersForApp(EfApp app, CancellationToken ct)
    {
        var categoryIDs = db.Context.ModifierCategories.Retrieve()
            .Where(modCat => modCat.AppID == app.ID)
            .Select(modCat => modCat.ID);
        var modifierIDs = db.Context.Modifiers.Retrieve()
            .Where(m => categoryIDs.Contains(m.CategoryID))
            .Select(m => m.ID);
        var modifiers = await db.Context.Modifiers.Retrieve()
            .Where(m => modifierIDs.Contains(m.ID))
            .ToArrayAsync(ct);
        return modifiers.Select(m => new EfModifier(db, m)).ToArray();
    }

    internal async Task<EfModifier> ModifierByTargetKey(EfModifierCategory category, string targetKey, CancellationToken ct)
    {
        var modifier = await GetModifierByTargetKey(category, targetKey, ct);
        return new EfModifier
        (
            db,
            modifier ?? throw new ModifierNotFoundException(targetKey, category)
        );
    }

    private Task<ModifierEntity?> GetModifierByTargetKey(EfModifierCategory category, string targetKey, CancellationToken ct) =>
        ModifiersForCategoryQuery(category)
            .Where(m => m.TargetKey == targetKey)
            .FirstOrDefaultAsync(ct);

    private IQueryable<ModifierEntity> ModifiersForCategoryQuery(EfModifierCategory modCategory) =>
        db.Context.Modifiers.Retrieve()
            .Where(m => m.CategoryID == modCategory.ID);
}