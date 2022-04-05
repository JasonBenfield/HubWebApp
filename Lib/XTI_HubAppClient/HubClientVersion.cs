﻿using XTI_App.Abstractions;

namespace XTI_HubAppClient;

internal sealed class HubClientVersion : IAppVersion
{
    private readonly HubAppClient hubClient;
    private readonly HubClientAppContext appContext;
    private readonly XTI_App.Abstractions.AppVersionKey versionKey;

    public HubClientVersion(HubAppClient hubClient, HubClientAppContext appContext, XtiVersionModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        ID = new EntityID(model.ID);
        versionKey = XTI_App.Abstractions.AppVersionKey.Parse(model.VersionKey);
    }

    public EntityID ID { get; }
    public XTI_App.Abstractions.AppVersionKey Key() => versionKey;

    public async Task<IResourceGroup> ResourceGroup(ResourceGroupName name)
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new GetVersionResourceGroupRequest
        {
            VersionKey = versionKey.Value,
            GroupName = name.Value
        };
        var group = await hubClient.Version.GetResourceGroup(appModifier, request);
        return new HubClientResourceGroup(hubClient, appContext, versionKey, group);
    }
}