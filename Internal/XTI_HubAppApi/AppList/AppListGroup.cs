using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppList;

public sealed class AppListGroup : AppApiGroupWrapper
{
    public AppListGroup(AppApiGroup source, IServiceProvider sp) : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        Index = source.AddAction(actions.View(nameof(Index), () => sp.GetRequiredService<IndexAction>()));
        GetAppDomain = source.AddAction
        (
            actions.Action
            (
                nameof(GetAppDomain),
                ResourceAccess.AllowAuthenticated(),
                () => sp.GetRequiredService<GetAppDomainAction>()
            )
        );
        GetApps = source.AddAction
        (
            actions.Action
            (
                nameof(GetApps),
                () => sp.GetRequiredService<GetAppsAction>()
            )
        );
        GetAppByID = source.AddAction
        (
            actions.Action(nameof(GetAppByID), () => sp.GetRequiredService<GetAppByIDAction>())
        );
        GetAppByAppKey = source.AddAction
        (
            actions.Action(nameof(GetAppByAppKey), () => sp.GetRequiredService<GetAppByAppKeyAction>())
        );
        RedirectToApp = source.AddAction
        (
            actions.Action
            (
                nameof(RedirectToApp),
                () => sp.GetRequiredService<RedirectToAppAction>()
            )
        );
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<GetAppDomainRequest, string> GetAppDomain { get; }
    public AppApiAction<EmptyRequest, AppWithModKeyModel[]> GetApps { get; }
    public AppApiAction<GetAppByIDRequest, AppWithModKeyModel> GetAppByID { get; }
    public AppApiAction<GetAppByAppKeyRequest, AppWithModKeyModel> GetAppByAppKey { get; }
    public AppApiAction<int, WebRedirectResult> RedirectToApp { get; }
}