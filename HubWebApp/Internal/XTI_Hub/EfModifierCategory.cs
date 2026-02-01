using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfModifierCategory
{
    private readonly EfHubDB factory;
    private readonly ModifierCategoryEntity record;

    internal EfModifierCategory(EfHubDB factory, ModifierCategoryEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ModifierCategoryEntity();
        ID = this.record.ID;
    }

    public int ID { get; }

    public bool IsDefault() => ModifierCategoryName.Default.Equals(record.Name);

    public Task<EfModifier> AddDefaultModifierIfNotFound(CancellationToken ct)
        => factory.Modifiers.AddDefaultModifierIfNotFound(this, ct);

    public Task<EfModifier> AddOrUpdateModifier(IGeneratedKey generatedModKey, string targetKey, string displayText, CancellationToken ct)
        => factory.Modifiers.AddOrUpdateByTargetKey(this, generatedModKey, targetKey, displayText, ct);

    public Task<EfModifier> AddOrUpdateModifier(ModifierKey modKey, int targetID, string displayText, CancellationToken ct)
        => AddOrUpdateModifier(modKey, targetID.ToString(), displayText, ct);

    public Task<EfModifier> AddOrUpdateModifier(ModifierKey modKey, string targetKey, string displayText, CancellationToken ct)
        => factory.Modifiers.AddOrUpdateByModKey(this, modKey, targetKey, displayText, ct);

    public Task<EfModifier> ModifierByModKey(ModifierKey modKey, CancellationToken ct) => factory.Modifiers.ModifierByModKey(this, modKey, ct);

    public Task<EfModifier> ModifierByModKeyOrDefault(ModifierKey modKey, CancellationToken ct) => factory.Modifiers.ModifierOrDefault(this, modKey, ct);

    public Task<EfModifier> ModifierByTargetID(int targetID, CancellationToken ct) => ModifierByTargetKey(targetID.ToString(), ct);

    public Task<EfModifier> ModifierByTargetKey(string targetKey, CancellationToken ct) => factory.Modifiers.ModifierByTargetKey(this, targetKey, ct);

    public Task<EfApp> App(CancellationToken ct) => factory.Apps.App(record.AppID, ct);

    public Task<EfModifier[]> Modifiers(CancellationToken ct) => factory.Modifiers.Modifiers(this, ct);

    public Task<EfResourceGroup[]> ResourceGroups(EfAppVersion appVersion, CancellationToken ct) => factory.Groups.Groups(appVersion, this, ct);

    public ModifierCategoryModel ToModel() => new ModifierCategoryModel
    {
        ID = ID,
        Name = new ModifierCategoryName(record.Name)
    };

    public override string ToString() => $"{nameof(EfModifierCategory)} {ID}";
}