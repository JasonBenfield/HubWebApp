namespace XTI_HubAppClient;

internal sealed class HcVersion : IAppVersion
{
    private readonly HubAppClient hubClient;
    private readonly HcAppContext appContext;
    private readonly AppVersionKey versionKey;

    public HcVersion(HubAppClient hubClient, HcAppContext appContext, XtiVersionModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        ID = new EntityID(model.ID);
        versionKey = model.VersionKey;
    }

    public EntityID ID { get; }
    public AppVersionKey Key() => versionKey;

    public async Task<IResourceGroup> ResourceGroup(ResourceGroupName name)
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new GetVersionResourceGroupRequest
        {
            VersionKey = versionKey.Value,
            GroupName = name.Value
        };
        var group = await hubClient.Version.GetResourceGroup(appModifier, request);
        return new HcResourceGroup(hubClient, appContext, versionKey, group);
    }
}