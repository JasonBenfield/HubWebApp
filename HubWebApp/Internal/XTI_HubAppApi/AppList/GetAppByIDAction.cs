﻿namespace XTI_HubAppApi.AppList;

public sealed class GetAppByIDRequest
{
    public int AppID { get; set; }
}

public sealed class GetAppByIDAction : AppAction<GetAppByIDRequest, AppWithModKeyModel>
{
    private readonly HubFactory factory;

    public GetAppByIDAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppWithModKeyModel> Execute(GetAppByIDRequest request)
    {
        var app = await factory.Apps.App(request.AppID);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await appsModCategory.ModifierByTargetID(app.ID);
        return new AppWithModKeyModel(hubApp.ToAppModel(), modifier.ModKey().Value);
    }
}