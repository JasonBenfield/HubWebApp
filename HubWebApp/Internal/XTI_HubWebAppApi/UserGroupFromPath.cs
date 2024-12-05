namespace XTI_HubWebAppApi;

public sealed class UserGroupFromPath
{
    private readonly HubFactory factory;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public UserGroupFromPath(HubFactory factory, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.factory = factory;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<AppUserGroup> Value()
    {
        var modKey = modifierKeyAccessor.Value();
        if (modKey.Equals(ModifierKey.Default))
        {
            throw new Exception(AppErrors.ModifierIsRequired);
        }
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups);
        var modifier = await modCategory.ModifierByModKey(modKey);
        var userGroup = await factory.UserGroups.UserGroup(modifier.TargetID());
        return userGroup;
    }
}
