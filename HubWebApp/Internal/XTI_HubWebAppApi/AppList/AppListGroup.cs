namespace XTI_HubWebAppApi.AppList;

public sealed class AppListGroup : AppApiGroupWrapper
{
    public AppListGroup(AppApiGroup source, IServiceProvider sp) : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetApps = source.AddAction
        (
            nameof(GetApps),
            () => sp.GetRequiredService<GetAppsAction>()
        );
        GetAppByID = source.AddAction
        (
            nameof(GetAppByID), () => sp.GetRequiredService<GetAppByIDAction>()
        );
        GetAppByAppKey = source.AddAction
        (
            nameof(GetAppByAppKey), () => sp.GetRequiredService<GetAppByAppKeyAction>()
        );
        RedirectToApp = source.AddAction
        (
            nameof(RedirectToApp),
            () => sp.GetRequiredService<RedirectToAppAction>()
        );
        GetAppDomains = source.AddAction
        (
            nameof(GetAppDomains),
            () => sp.GetRequiredService<GetAppDomainsAction>()
        );
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppWithModKeyModel[]> GetApps { get; }
    public AppApiAction<GetAppByIDRequest, AppWithModKeyModel> GetAppByID { get; }
    public AppApiAction<GetAppByAppKeyRequest, AppWithModKeyModel> GetAppByAppKey { get; }
    public AppApiAction<int, WebRedirectResult> RedirectToApp { get; }
    public AppApiAction<EmptyRequest, AppDomainModel[]> GetAppDomains { get; }
}