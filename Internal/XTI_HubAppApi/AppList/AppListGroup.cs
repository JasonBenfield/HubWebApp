using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
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
        All = source.AddAction
        (
            actions.Action
            (
                nameof(All),
                () => sp.GetRequiredService<GetAllAppsAction>()
            )
        );
        GetAppModifierKey = source.AddAction
        (
            actions.Action(nameof(GetAppModifierKey), () => sp.GetRequiredService<GetAppModifierKeyAction>())
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
    public AppApiAction<EmptyRequest, AppModel[]> All { get; }
    public AppApiAction<AppKey, string> GetAppModifierKey { get; }
    public AppApiAction<int, WebRedirectResult> RedirectToApp { get; }
}