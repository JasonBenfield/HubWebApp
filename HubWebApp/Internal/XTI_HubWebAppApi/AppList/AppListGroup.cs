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
        GetAppDomains = source.AddAction
        (
            nameof(GetAppDomains),
            () => sp.GetRequiredService<GetAppDomainsAction>()
        );
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppModel[]> GetApps { get; }
    public AppApiAction<EmptyRequest, AppDomainModel[]> GetAppDomains { get; }
}