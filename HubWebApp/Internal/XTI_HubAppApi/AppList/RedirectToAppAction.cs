namespace XTI_HubAppApi.AppList;

public sealed class RedirectToAppAction : AppAction<int, WebRedirectResult>
{
    private readonly HubFactory factory;
    private readonly XtiPath path;
    private readonly HubAppApi hubApi;

    public RedirectToAppAction(HubFactory factory, XtiPath path, HubAppApi hubApi)
    {
        this.factory = factory;
        this.path = path;
        this.hubApi = hubApi;
    }

    public async Task<WebRedirectResult> Execute(int appID)
    {
        var app = await factory.Apps.App(appID);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await appsModCategory.ModifierByTargetID(app.ID);
        var redirectPath = path
            .WithNewGroup(hubApi.App.Index.Path)
            .WithModifier(modifier.ModKey());
        return new WebRedirectResult(redirectPath.Format());
    }
}