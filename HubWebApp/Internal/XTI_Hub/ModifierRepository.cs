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

    internal Task<Modifier> AddDefaultModifierIfNotFound(ModifierCategory category) =>
        AddOrUpdateByModKey(category, ModifierKey.Default, "", "");

    internal async Task<Modifier> AddOrUpdateByModKey(ModifierCategory category, ModifierKey modKey, string targetID, string displayText)
    {
        var record = await GetModifierByModKey(category, modKey);
        if (record == null)
        {
            record = await Add(category, modKey, targetID, displayText);
        }
        return factory.CreateModifier(record);
    }

    internal async Task<Modifier> AddOrUpdateByTargetKey(ModifierCategory category, string targetKey, string displayText)
    {
        var record = await GetModifierByTargetKey(category, targetKey);
        if (record == null)
        {
            record = await Add(category, ModifierKey.Generate(), targetKey, displayText);
        }
        return factory.CreateModifier(record);
    }

    private async Task<ModifierEntity> Add(ModifierCategory category, ModifierKey modKey, string targetID, string displayText)
    {
        var record = new ModifierEntity
        {
            CategoryID = category.ID,
            ModKey = modKey.Value,
            TargetKey = targetID,
            DisplayText = displayText
        };
        await factory.DB.Modifiers.Create(record);
        return record;
    }

    internal Task<Modifier[]> Modifiers(ModifierCategory category) =>
        modifiersForCategory(category)
            .Select(m => factory.CreateModifier(m))
            .ToArrayAsync();

    internal async Task<Modifier> Modifier(int id)
    {
        var entity = await factory.DB
            .Modifiers.Retrieve()
            .Where(m => m.ID == id).FirstOrDefaultAsync();
        return factory.CreateModifier(entity ?? throw new Exception($"Modifier {id} not found"));
    }

    internal async Task<Modifier> ModifierByModKey(ModifierCategory modCategory, ModifierKey modKey)
    {
        if
        (
            !modCategory.Name().Equals(ModifierCategoryName.Default)
            && modKey.Equals(ModifierKey.Default)
        )
        {
            var app = await modCategory.App();
            modCategory = await app.ModCategory(ModifierCategoryName.Default);
        }
        var record = await GetModifierByModKey(modCategory, modKey);
        return factory.CreateModifier
        (
            record ?? throw new ModifierNotFoundException(modKey, modCategory)
        );
    }

    private Task<ModifierEntity?> GetModifierByModKey(ModifierCategory modCategory, ModifierKey modKey) =>
        factory.DB
            .Modifiers
            .Retrieve()
            .Where(m => m.CategoryID == modCategory.ID && m.ModKey == modKey.Value)
            .FirstOrDefaultAsync();

    internal async Task<Modifier> ModifierOrDefault(ModifierCategory modCategory, ModifierKey modKey)
    {
        var app = await modCategory.App();
        if
        (
            !modCategory.Name().Equals(ModifierCategoryName.Default)
            && modKey.Equals(ModifierKey.Default)
        )
        {
            modCategory = await app.ModCategory(ModifierCategoryName.Default);
        }
        var record = await GetModifierByModKey(modCategory, modKey);
        Modifier mod;
        if (record == null)
        {
            mod = await app.DefaultModifier();
        }
        else
        {
            mod = factory.CreateModifier(record);
        }
        return mod;
    }

    internal async Task<Modifier> ModifierForApp(App app, int modifierID)
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
            .FirstOrDefaultAsync();
        return factory.CreateModifier
        (
            record ?? throw new ModifierNotFoundException(modifierID, app)
        );
    }

    internal async Task<Modifier[]> ModifiersForApp(App app)
    {
        var categoryIDs = factory.DB
            .ModifierCategories
            .Retrieve()
            .Where(modCat => modCat.AppID == app.ID)
            .Select(modCat => modCat.ID);
        var modifierIDs = factory.DB
            .Modifiers.Retrieve()
            .Where(m => categoryIDs.Contains(m.CategoryID))
            .Select(m => m.ID);
        var records = await factory.DB
            .Modifiers
            .Retrieve()
            .Where(m => modifierIDs.Contains(m.ID))
            .ToArrayAsync();
        return records.Select(m => factory.CreateModifier(m)).ToArray();
    }

    internal async Task<Modifier> ModifierByTargetKey(ModifierCategory category, string targetKey)
    {
        var record = await GetModifierByTargetKey(category, targetKey);
        return factory.CreateModifier
        (
            record ?? throw new ModifierNotFoundException(targetKey, category)
        );
    }

    private Task<ModifierEntity?> GetModifierByTargetKey(ModifierCategory category, string targetKey) =>
        modifiersForCategory(category)
            .Where(m => m.TargetKey == targetKey)
            .FirstOrDefaultAsync();

    private IQueryable<ModifierEntity> modifiersForCategory(ModifierCategory modCategory) =>
        factory.DB
            .Modifiers
            .Retrieve()
            .Where(m => m.CategoryID == modCategory.ID);
}