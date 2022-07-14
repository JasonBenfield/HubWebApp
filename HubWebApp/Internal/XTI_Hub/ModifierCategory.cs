using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ModifierCategory
{
    private readonly HubFactory factory;
    private readonly ModifierCategoryEntity record;

    internal ModifierCategory(HubFactory factory, ModifierCategoryEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ModifierCategoryEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public ModifierCategoryName Name() => new ModifierCategoryName(record.Name);

    public Task<Modifier> AddDefaultModifierIfNotFound()
        => factory.Modifiers.AddDefaultModifierIfNotFound(this);

    public Task<Modifier> AddOrUpdateModifier(int targetID, string displayText)
        => AddOrUpdateModifier(targetID.ToString(), displayText);

    public Task<Modifier> AddOrUpdateModifier(string targetKey, string displayText)
        => factory.Modifiers.AddOrUpdateByTargetKey(this, targetKey, displayText);

    public Task<Modifier> ModifierByModKey(ModifierKey modKey) => factory.Modifiers.ModifierByModKey(this, modKey);

    public Task<Modifier> ModifierByModKeyOrDefault(ModifierKey modKey) => factory.Modifiers.ModifierOrDefault(this, modKey);

    public Task<Modifier> ModifierByTargetID(int targetID) => ModifierByTargetKey(targetID.ToString());

    public Task<Modifier> ModifierByTargetKey(string targetKey) => factory.Modifiers.ModifierByTargetKey(this, targetKey);

    public Task<App> App() => factory.Apps.App(record.AppID);

    public Task<Modifier[]> Modifiers() => factory.Modifiers.Modifiers(this);

    public Task<ResourceGroup[]> ResourceGroups(AppVersion appVersion) => factory.Groups.Groups(appVersion, this);

    public ModifierCategoryModel ToModel() => new ModifierCategoryModel
    {
        ID = ID,
        Name = Name()
    };

    public override string ToString() => $"{nameof(ModifierCategory)} {ID}";
}