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

    public bool IsDefault() => ModifierCategoryName.Default.Equals(record.Name);

    public Task<Modifier> AddDefaultModifierIfNotFound(CancellationToken ct)
        => factory.Modifiers.AddDefaultModifierIfNotFound(this, ct);

    public Task<Modifier> AddOrUpdateModifier(IGeneratedKey generatedModKey, string targetKey, string displayText, CancellationToken ct)
        => factory.Modifiers.AddOrUpdateByTargetKey(this, generatedModKey, targetKey, displayText, ct);

    public Task<Modifier> AddOrUpdateModifier(ModifierKey modKey, int targetID, string displayText, CancellationToken ct)
        => AddOrUpdateModifier(modKey, targetID.ToString(), displayText, ct);

    public Task<Modifier> AddOrUpdateModifier(ModifierKey modKey, string targetKey, string displayText, CancellationToken ct)
        => factory.Modifiers.AddOrUpdateByModKey(this, modKey, targetKey, displayText, ct);

    public Task<Modifier> ModifierByModKey(ModifierKey modKey, CancellationToken ct) => factory.Modifiers.ModifierByModKey(this, modKey, ct);

    public Task<Modifier> ModifierByModKeyOrDefault(ModifierKey modKey, CancellationToken ct) => factory.Modifiers.ModifierOrDefault(this, modKey, ct);

    public Task<Modifier> ModifierByTargetID(int targetID, CancellationToken ct) => ModifierByTargetKey(targetID.ToString(), ct);

    public Task<Modifier> ModifierByTargetKey(string targetKey, CancellationToken ct) => factory.Modifiers.ModifierByTargetKey(this, targetKey, ct);

    public Task<App> App(CancellationToken ct) => factory.Apps.App(record.AppID, ct);

    public Task<Modifier[]> Modifiers(CancellationToken ct) => factory.Modifiers.Modifiers(this, ct);

    public Task<ResourceGroup[]> ResourceGroups(AppVersion appVersion) => factory.Groups.Groups(appVersion, this);

    public ModifierCategoryModel ToModel() => new ModifierCategoryModel
    {
        ID = ID,
        Name = new ModifierCategoryName(record.Name)
    };

    public override string ToString() => $"{nameof(ModifierCategory)} {ID}";
}