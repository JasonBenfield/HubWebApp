namespace XTI_HubWebAppApiActions;

public sealed class UserGroupFromPath
{
    private readonly EfHubDB factory;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public UserGroupFromPath(EfHubDB factory, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.factory = factory;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EfAppUserGroup> Value(CancellationToken ct)
    {
        var modKey = modifierKeyAccessor.Value();
        if (modKey.Equals(ModifierKey.Default))
        {
            throw new Exception(AppErrors.ModifierIsRequired);
        }
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct);
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var modifier = await modCategory.ModifierByModKey(modKey, ct);
        var userGroup = await factory.UserGroups.UserGroup(modifier.TargetID(), ct);
        return userGroup;
    }
}
