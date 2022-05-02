namespace XTI_HubAppApi.AppList;

public sealed class GetAppByAppKeyRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
}

public sealed class GetAppByAppKeyAction : AppAction<GetAppByAppKeyRequest, AppWithModKeyModel>
{
    private readonly HubFactory factory;

    public GetAppByAppKeyAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppWithModKeyModel> Execute(GetAppByAppKeyRequest request)
    {
        var app = await factory.Apps.App(request.AppKey);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await appsModCategory.ModifierByTargetID(app.ID);
        return new AppWithModKeyModel(hubApp.ToAppModel(), modifier.ModKey().Value);
    }
}