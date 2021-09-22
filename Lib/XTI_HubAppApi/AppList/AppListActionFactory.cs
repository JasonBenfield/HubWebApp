using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppList
{
    public sealed class AppListActionFactory
    {
        private readonly IServiceProvider sp;

        public AppListActionFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public TitledViewAppAction<EmptyRequest> CreateIndex()
        {
            var pageContext = sp.GetService<IPageContext>();
            return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Apps");
        }

        public GetAllAppsAction CreateAll()
        {
            var appFactory = sp.GetService<AppFactory>();
            var userContext = sp.GetService<IUserContext>();
            return new GetAllAppsAction(appFactory, userContext);
        }

        internal GetAppModifierKeyAction CreateGetAppModifierKey()
        {
            var factory = sp.GetService<AppFactory>();
            return new GetAppModifierKeyAction(factory);
        }

        internal RedirectToAppAction CreateRedirectToApp()
        {
            var factory = sp.GetService<AppFactory>();
            var path = sp.GetService<XtiPath>();
            var hubApi = sp.GetService<HubAppApi>();
            return new RedirectToAppAction(factory, path, hubApi);
        }
    }
}
