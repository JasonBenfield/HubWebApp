using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.Apps
{
    public sealed class AppsGroup : AppApiGroup
    {
        public AppsGroup
        (
            AppApi api,
            ResourceAccess access,
            IAppApiUser user,
            AppsActionFactory factory
        ) : base
        (
            api,
            new NameFromGroupClassName(nameof(AppsGroup)).Value,
            ModifierCategoryName.Default,
            access,
            user,
            (n, a, u) => new WebAppApiActionCollection(n, a, u)
        )
        {
            var actions = Actions<WebAppApiActionCollection>();
            Index = actions.AddDefaultView();
            All = actions.AddAction
            (
                nameof(All),
                () => factory.CreateAll()
            );
        }

        public AppApiAction<EmptyRequest, AppActionViewResult> Index { get; }
        public AppApiAction<EmptyRequest, AppModel[]> All { get; }
    }
}
