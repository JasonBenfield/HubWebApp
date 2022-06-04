namespace XTI_HubAppApi.UserInquiry;

public sealed class RedirectToAppUserAction : AppAction<RedirectToAppUserRequest, WebRedirectResult>
{
    private readonly HubFactory factory;
    private readonly XtiPath path;
    private readonly HubAppApi hubApi;

    public RedirectToAppUserAction(HubFactory factory, XtiPath path, HubAppApi hubApi)
    {
        this.factory = factory;
        this.path = path;
        this.hubApi = hubApi;
    }

    public async Task<WebRedirectResult> Execute(RedirectToAppUserRequest model, CancellationToken stoppingToken)
    {
        var app = await factory.Apps.App(model.AppID);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await appsModCategory.ModifierByTargetID(app.ID);
        var redirectPath = path
            .WithNewGroup(hubApi.AppUser.Index.Path)
            .WithModifier(modifier.ModKey());
        var url = $"{redirectPath.Format()}?userID={model.UserID}";
        return new WebRedirectResult(url);
    }
}