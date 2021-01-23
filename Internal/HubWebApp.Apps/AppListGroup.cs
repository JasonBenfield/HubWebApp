using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.Apps
{
    public sealed class AppListGroup : AppApiGroupWrapper
    {
        public AppListGroup(AppApiGroup source, AppGroupActionFactory factory) : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), factory.CreateAppsIndex));
            All = source.AddAction
            (
                actions.Action
                (
                    nameof(All),
                    factory.CreateAll
                )
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
        public AppApiAction<int, WebRedirectResult> RedirectToApp { get; }
    }
}
