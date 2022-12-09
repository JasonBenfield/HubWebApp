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

    public async Task<App> App()
    {
        var category = await factory.ModCategories.Category(record.CategoryID);
        var app = await category.App();
        return app;
    }

    public Task SetDisplayText(string displayText)
    {
        return factory.DB.Modifiers.Update(record, r =>
        {
            r.DisplayText = displayText;
        });
    }

    public async Task<Modifier> DefaultModifier()
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
                .FirstAsync();
            var app = await factory.Apps.App(appID);
            defaultModifier = await app.DefaultModifier();
        }
        return defaultModifier;
    }

    public ModifierModel ToModel() => new ModifierModel
    {
        ID = ID,
        CategoryID = record.CategoryID,
        ModKey = ModKey(),
        TargetKey = record.TargetKey,
        DisplayText = record.DisplayText
    };

    private ModifierKey ModKey() => new ModifierKey(record.ModKey);

    public override string ToString() => $"{nameof(Modifier)} {ID}";
}