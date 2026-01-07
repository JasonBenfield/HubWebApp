using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ModifierRepository
{
    private readonly HubFactory factory;

    internal ModifierRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal Task<Modifier> AddDefaultModifierIfNotFound(ModifierCategory category, CancellationToken ct) =>
        AddOrUpdateByModKey(category, ModifierKey.Default, "", "", ct);

    internal async Task<Modifier> AddOrUpdateByModKey(ModifierCategory category, ModifierKey modKey, string targetKey, string displayText, CancellationToken ct)
    {
        var record = await factory.DB
            .Modifiers
            .Retrieve()
            .Where(m => m.CategoryID == category.ID && (m.ModKey == modKey.Value || m.TargetKey == targetKey))
            .FirstOrDefaultAsync(ct);
        if (record == null)
        {
            record = await Add(category, modKey, targetKey, displayText, ct);
        }
        else
        {
            await factory.DB.Modifiers.Update
            (
                record,
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
        return factory.CreateModifier(record);
    }

    internal async Task<Modifier> AddOrUpdateByTargetKey(ModifierCategory category, IGeneratedKey generatedModKey, string targetKey, string displayText, CancellationToken ct)
    {
        var record = await GetModifierByTargetKey(category, targetKey, ct);
        if (record == null)
        {
            var modKey = await GenerateModKey(category, generatedModKey, ct);
            record = await Add(category, modKey, targetKey, displayText, ct);
        }
        else
        {
            await factory.DB.Modifiers.Update
            (
                record,
                m =>
                {
                    m.DisplayText = displayText;
                },
                ct
            );
        }
        return factory.CreateModifier(record);
    }

    private async Task<ModifierKey> GenerateModKey(ModifierCategory category, IGeneratedKey generatedModKey, CancellationToken ct)
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

    private async Task<ModifierEntity> Add(ModifierCategory category, ModifierKey modKey, string targetID, string displayText, CancellationToken ct)
    {
        var record = new ModifierEntity
        {
            CategoryID = category.ID,
            ModKey = modKey.Value,
            ModKeyDisplayText = modKey.DisplayText,
            TargetKey = targetID,
            DisplayText = displayText
        };
        await factory.DB.Modifiers.Create(record, ct);
        return record;
    }

    internal Task<Modifier[]> Modifiers(ModifierCategory category, CancellationToken ct) =>
        modifiersForCategory(category)
            .Select(m => factory.CreateModifier(m))
            .ToArrayAsync(ct);

    public async Task<Modifier> Modifier(int id, CancellationToken ct)
    {
        var entity = await factory.DB.Modifiers.Retrieve()
            .Where(m => m.ID == id)
            .FirstOrDefaultAsync(ct);
        return factory.CreateModifier(entity ?? throw new Exception($"Modifier {id} not found"));
    }

    internal async Task<Modifier> ModifierByModKey(ModifierCategory modCategory, ModifierKey modKey, CancellationToken ct)
    {
        if (!modCategory.IsDefault() && modKey.Equals(ModifierKey.Default))
        {
            var app = await modCategory.App(ct);
            modCategory = await app.ModCategory(ModifierCategoryName.Default, ct);
        }
        var record = await GetModifierByModKey(modCategory, modKey, ct);
        return factory.CreateModifier
        (
            record ?? throw new ModifierNotFoundException(modKey, modCategory)
        );
    }

    private Task<ModifierEntity?> GetModifierByModKey(ModifierCategory modCategory, ModifierKey modKey, CancellationToken ct) =>
        factory.DB.Modifiers.Retrieve()
            .Where(m => m.CategoryID == modCategory.ID && m.ModKey == modKey.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<Modifier> ModifierOrDefault(ModifierCategory modCategory, ModifierKey modKey, CancellationToken ct)
    {
        var app = await modCategory.App(ct);
        if (!modCategory.IsDefault() && modKey.Equals(ModifierKey.Default))
        {
            modCategory = await app.ModCategory(ModifierCategoryName.Default, ct);
        }
        var record = await GetModifierByModKey(modCategory, modKey, ct);
        Modifier mod;
        if (record == null)
        {
            mod = await app.DefaultModifier(ct);
        }
        else
        {
            mod = factory.CreateModifier(record);
        }
        return mod;
    }

    internal async Task<Modifier> ModifierForApp(App app, int modifierID, CancellationToken ct)
    {
        var categoryIDs = factory.DB
            .ModifierCategories
            .Retrieve()
            .Where(modCat => modCat.AppID == app.ID)
            .Select(modCat => modCat.ID);
        var record = await factory.DB
            .Modifiers
            .Retrieve()
            .Where(m => categoryIDs.Contains(m.CategoryID) && m.ID == modifierID)
            .FirstOrDefaultAsync(ct);
        return factory.CreateModifier
        (
            record ?? throw new ModifierNotFoundException(modifierID, app)
        );
    }

    internal async Task<Modifier[]> ModifiersForApp(App app, CancellationToken ct)
    {
        var categoryIDs = factory.DB.ModifierCategories.Retrieve()
            .Where(modCat => modCat.AppID == app.ID)
            .Select(modCat => modCat.ID);
        var modifierIDs = factory.DB.Modifiers.Retrieve()
            .Where(m => categoryIDs.Contains(m.CategoryID))
            .Select(m => m.ID);
        var records = await factory.DB.Modifiers.Retrieve()
            .Where(m => modifierIDs.Contains(m.ID))
            .ToArrayAsync(ct);
        return records.Select(m => factory.CreateModifier(m)).ToArray();
    }

    internal async Task<Modifier> ModifierByTargetKey(ModifierCategory category, string targetKey, CancellationToken ct)
    {
        var record = await GetModifierByTargetKey(category, targetKey, ct);
        return factory.CreateModifier
        (
            record ?? throw new ModifierNotFoundException(targetKey, category)
        );
    }

    private Task<ModifierEntity?> GetModifierByTargetKey(ModifierCategory category, string targetKey, CancellationToken ct) =>
        modifiersForCategory(category)
            .Where(m => m.TargetKey == targetKey)
            .FirstOrDefaultAsync(ct);

    private IQueryable<ModifierEntity> modifiersForCategory(ModifierCategory modCategory) =>
        factory.DB.Modifiers.Retrieve()
            .Where(m => m.CategoryID == modCategory.ID);
}