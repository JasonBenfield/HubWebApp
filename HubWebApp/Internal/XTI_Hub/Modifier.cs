using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class Modifier
{
    private readonly HubFactory factory;
    private readonly ModifierEntity record;

    internal Modifier(HubFactory factory, ModifierEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ModifierEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public int TargetID() => int.Parse(record.TargetKey);

    public bool IsDefault() => ModKey().Equals(ModifierKey.Default);

    public bool IsForCategory(ModifierCategory modCategory) => modCategory.ID == record.CategoryID;

    public async Task<App> App(CancellationToken ct)
    {
        var category = await factory.ModCategories.Category(record.CategoryID, ct);
        var app = await category.App(ct);
        return app;
    }

    public Task SetDisplayText(string displayText, CancellationToken ct) =>
        factory.DB.Modifiers.Update
        (
            record, 
            r =>
            {
                r.DisplayText = displayText;
            },
            ct
        );

    public async Task<Modifier> DefaultModifier(CancellationToken ct)
    {
        Modifier? defaultModifier;
        if (IsDefault())
        {
            defaultModifier = this;
        }
        else
        {
            var appID = await factory.DB
                .ModifierCategories
                .Retrieve()
                .Where(modCat => modCat.ID == record.CategoryID)
                .Select(modCat => modCat.AppID)
                .FirstAsync(ct);
            var app = await factory.Apps.App(appID, ct);
            defaultModifier = await app.DefaultModifier(ct);
        }
        return defaultModifier;
    }

    public Task<ModifierCategory> Category(CancellationToken ct) =>
        factory.ModCategories.Category(record.CategoryID, ct);

    public ModifierModel ToModel() => new ModifierModel
    {
        ID = ID,
        CategoryID = record.CategoryID,
        ModKey = ModKey(),
        TargetKey = record.TargetKey,
        DisplayText = record.DisplayText
    };

    private ModifierKey ModKey() => new ModifierKey(record.ModKeyDisplayText);

    public override string ToString() => $"{nameof(Modifier)} {ID}";
}