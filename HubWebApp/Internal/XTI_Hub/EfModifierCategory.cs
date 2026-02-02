using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfModifierCategory
{
    private readonly EfHubDB db;
    private readonly ModifierCategoryEntity category;

    internal EfModifierCategory(EfHubDB db, ModifierCategoryEntity category)
    {
        this.db = db;
        this.category = category ?? new ModifierCategoryEntity();
        ID = this.category.ID;
    }

    public int ID { get; }

    public bool IsDefault() => ModifierCategoryName.Default.Equals(category.Name);

    public Task<EfModifier> AddDefaultModifierIfNotFound(CancellationToken ct) =>
        db.Modifiers.AddDefaultModifierIfNotFound(this, ct);

    public Task<EfModifier> AddOrUpdateModifier(IGeneratedKey generatedModKey, string targetKey, string displayText, CancellationToken ct) => db.Modifiers.AddOrUpdateByTargetKey(this, generatedModKey, targetKey, displayText, ct);

    public Task<EfModifier> AddOrUpdateModifier(ModifierKey modKey, int targetID, string displayText, CancellationToken ct) =>
        AddOrUpdateModifier(modKey, targetID.ToString(), displayText, ct);

    public Task<EfModifier> AddOrUpdateModifier(ModifierKey modKey, string targetKey, string displayText, CancellationToken ct) =>
        db.Modifiers.AddOrUpdateByModKey(this, modKey, targetKey, displayText, ct);

    public Task<EfModifier> ModifierByModKey(ModifierKey modKey, CancellationToken ct) => db.Modifiers.ModifierByModKey(this, modKey, ct);

    public Task<EfModifier> ModifierByModKeyOrDefault(ModifierKey modKey, CancellationToken ct) => db.Modifiers.ModifierOrDefault(this, modKey, ct);

    public Task<EfModifier> ModifierByTargetID(int targetID, CancellationToken ct) => ModifierByTargetKey(targetID.ToString(), ct);

    public Task<EfModifier> ModifierByTargetKey(string targetKey, CancellationToken ct) => db.Modifiers.ModifierByTargetKey(this, targetKey, ct);

    public Task<EfApp> App(CancellationToken ct) => db.Apps.App(category.AppID, ct);

    public Task<EfModifier[]> Modifiers(CancellationToken ct) => db.Modifiers.Modifiers(this, ct);

    public Task<EfResourceGroup[]> ResourceGroups(EfAppVersion appVersion, CancellationToken ct) => db.Groups.Groups(appVersion, this, ct);

    public ModifierCategoryModel ToModel() =>
        new ModifierCategoryModel
        (
            ID: ID,
            Name: new ModifierCategoryName(category.Name)
        );

    public override string ToString() => $"{nameof(EfModifierCategory)} {ID}";
}