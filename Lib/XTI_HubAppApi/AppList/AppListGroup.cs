using XTI_Hub;
using XTI_App.Api;
using XTI_WebApp.Api;
using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppList
{
    public sealed class AppListGroup : AppApiGroupWrapper
    {
        public AppListGroup(AppApiGroup source, AppListActionFactory factory) : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), factory.CreateIndex));
            All = source.AddAction
            (
                actions.Action
                (
                    nameof(All),
                    factory.CreateAll
                )
            );
            GetAppModifierKey = source.AddAction
            (
                actions.Action(nameof(GetAppModifierKey), factory.CreateGetAppModifierKey)
            );
            RedirectToApp = source.AddAction
            (
                actions.Action
                (
                    nameof(RedirectToApp),
                    factory.CreateRedirectToApp
                )
            );
        }

        public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
        public AppApiAction<EmptyRequest, AppModel[]> All { get; }
        public AppApiAction<AppKey, string> GetAppModifierKey { get; }
        public AppApiAction<int, WebRedirectResult> RedirectToApp { get; }
    }
}
