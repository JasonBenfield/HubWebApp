using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfModifier
{
    private readonly EfHubDB db;
    private readonly ModifierEntity modifier;

    internal EfModifier(EfHubDB db, ModifierEntity modifier)
    {
        this.db = db;
        this.modifier = modifier ?? new ModifierEntity();
        ID = this.modifier.ID;
    }

    public int ID { get; }
    public int TargetID() => int.Parse(modifier.TargetKey);

    public bool IsDefault() => ModKey().Equals(ModifierKey.Default);

    public bool IsForCategory(EfModifierCategory modCategory) => modCategory.ID == modifier.CategoryID;

    public async Task<EfApp> App(CancellationToken ct)
    {
        var efCategory = await db.ModCategories.Category(modifier.CategoryID, ct);
        var efApp = await efCategory.App(ct);
        return efApp;
    }

    public Task SetDisplayText(string displayText, CancellationToken ct) =>
        db.Context.Modifiers.Update
        (
            modifier,
            r =>
            {
                r.DisplayText = displayText;
            },
            ct
        );

    public async Task<EfModifier> DefaultModifier(CancellationToken ct)
    {
        EfModifier? efDefaultModifier;
        if (IsDefault())
        {
            efDefaultModifier = this;
        }
        else
        {
            var appID = await db.Context.ModifierCategories.Retrieve()
                .Where(modCat => modCat.ID == modifier.CategoryID)
                .Select(modCat => modCat.AppID)
                .FirstAsync(ct);
            var efApp = await db.Apps.App(appID, ct);
            efDefaultModifier = await efApp.DefaultModifier(ct);
        }
        return efDefaultModifier;
    }

    public Task<EfModifierCategory> Category(CancellationToken ct) =>
        db.ModCategories.Category(modifier.CategoryID, ct);

    public ModifierModel ToModel() =>
        new ModifierModel
        (
            ID: ID,
            CategoryID: modifier.CategoryID,
            ModKey: ModKey(),
            TargetKey: modifier.TargetKey,
            DisplayText: modifier.DisplayText
        );

    private ModifierKey ModKey() => new ModifierKey(modifier.ModKeyDisplayText);

    public override string ToString() => $"{nameof(EfModifier)} {ID}";
}