namespace XTI_HubAppClient;

internal sealed class HcApp : IApp
{
    private readonly HubAppClient hubClient;
    private readonly HcAppContext appContext;
    public HcApp(HubAppClient hubClient, HcAppContext appContext, AppModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        ID = model.ID;
        Title = model.Title;
    }

    public int ID { get; }
    public string Title { get; }

    public async Task<IModifierCategory> ModCategory(ModifierCategoryName name)
    {
        var modifier = await appContext.GetModifierKey();
        var modCategory = await hubClient.App.GetModifierCategory(modifier, name.Value);
        return new HcModifierCategory(hubClient, appContext, modCategory);
    }

    public Task<ModifierKey> ModKeyInHubApps() => appContext.ModKeyInHubApps(this);

    public async Task<IAppRole> Role(AppRoleName roleName)
    {
        var modifier = await appContext.GetModifierKey();
        var role = await hubClient.App.GetRole(modifier, roleName.Value);
        return new HcRole(role);
    }

    public async Task<IAppRole[]> Roles()
    {
        var modifier = await appContext.GetModifierKey();
        var roles = await hubClient.App.GetRoles(modifier);
        return roles.Select(r => new HcRole(r)).ToArray();
    }

    public async Task<IAppVersion> Version(AppVersionKey versionKey)
    {
        var modifier = await appContext.GetModifierKey();
        var version = await hubClient.Version.GetVersion(modifier, versionKey.Value);
        return new HcVersion(hubClient, appContext, version);
    }
}