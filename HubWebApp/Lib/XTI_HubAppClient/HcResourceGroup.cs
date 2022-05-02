namespace XTI_HubAppClient;

internal sealed class HcResourceGroup : IResourceGroup
{
    private readonly HubAppClient hubClient;
    private readonly HcAppContext appContext;
    private readonly AppVersionKey versionKey;
    private readonly ResourceGroupName name;

    public HcResourceGroup(HubAppClient hubClient, HcAppContext appContext, AppVersionKey versionKey, ResourceGroupModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        this.versionKey = versionKey;
        ID = model.ID;
        name = new ResourceGroupName(model.Name);
    }

    public int ID { get; }
    public ResourceGroupName Name() => name;

    public async Task<IModifierCategory> ModCategory()
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new GetResourceGroupModCategoryRequest
        {
            VersionKey = versionKey.Value,
            GroupID = ID
        };
        var modCategory = await hubClient.ResourceGroup.GetModCategory(appModifier, request);
        return new HcModifierCategory(hubClient, appContext, modCategory);
    }

    public async Task<IResource> Resource(ResourceName name)
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new GetResourceGroupResourceRequest
        {
            VersionKey = versionKey.Value,
            GroupID = ID,
            ResourceName = name.Value
        };
        var resource = await hubClient.ResourceGroup.GetResource(appModifier, request);
        return new HcResource(resource);
    }
}