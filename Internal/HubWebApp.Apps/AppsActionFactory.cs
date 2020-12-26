using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace HubWebApp.Apps
{
    public sealed class AppsActionFactory
    {
        private readonly IServiceProvider sp;

        public AppsActionFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public TitledViewAppAction<EmptyRequest> CreateAppsIndex()
        {
            var pageContext = sp.GetService<IPageContext>();
            return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Apps");
        }

        public TitledViewAppAction<EmptyRequest> CreateAppIndex()
        {
            var pageContext = sp.GetService<IPageContext>();
            return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "App Dashboard");
        }

        public AppAction<EmptyRequest, AppModel[]> CreateAll()
        {
            var appFactory = sp.GetService<AppFactory>();
            var userContext = sp.GetService<IUserContext>();
            return new AllAction(appFactory, userContext);
        }

        internal AppAction<int, AppActionRedirectResult> CreateRedirectToApp()
        {
            var factory = sp.GetService<AppFactory>();
            var path = sp.GetService<XtiPath>();
            return new RedirectToAppAction(factory, path);
        }

        internal AppAction<EmptyRequest, AppModel> CreateGetApp()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetAppAction(appFromPath);
        }

        internal AppAction<EmptyRequest, AppVersionModel> CreateGetCurrentVersion()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetCurrentVersionAction(appFromPath);
        }

    }
}
