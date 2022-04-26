﻿using XTI_App.Abstractions;

namespace XTI_HubAppClient;

internal sealed class HcResourceGroup : IResourceGroup
{
    private readonly HubAppClient hubClient;
    private readonly HcAppContext appContext;
    private readonly XTI_App.Abstractions.AppVersionKey versionKey;
    private readonly ResourceGroupName name;

    public HcResourceGroup(HubAppClient hubClient, HcAppContext appContext, XTI_App.Abstractions.AppVersionKey versionKey, ResourceGroupModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        this.versionKey = versionKey;
        ID = new EntityID(model.ID);
        name = new ResourceGroupName(model.Name);
    }

    public EntityID ID { get; }
    public ResourceGroupName Name() => name;

    public async Task<IModifierCategory> ModCategory()
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new GetResourceGroupModCategoryRequest
        {
            VersionKey = versionKey.Value,
            GroupID = ID.Value
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
            GroupID = ID.Value,
            ResourceName = name.Value
        };
        var resource = await hubClient.ResourceGroup.GetResource(appModifier, request);
        return new HcResource(resource);
    }
}