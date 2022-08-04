namespace XTI_HubWebAppApi;

public sealed class UserGroupFromPath
{
    private readonly HubFactory factory;
    private readonly IXtiPathAccessor pathAccessor;

    public UserGroupFromPath(HubFactory factory, IXtiPathAccessor pathAccessor)
    {
        this.factory = factory;
        this.pathAccessor = pathAccessor;
    }

    public async Task<AppUserGroup> Value()
    {
        var path = pathAccessor.Value();
        var modKey = path.Modifier;
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
