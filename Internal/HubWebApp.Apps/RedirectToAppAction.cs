using HubWebApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.Apps
{
    public sealed class RedirectToAppAction : AppAction<int, WebRedirectResult>
    {
        private readonly AppFactory factory;
        private readonly XtiPath path;

        public RedirectToAppAction(AppFactory factory, XtiPath path)
        {
            this.factory = factory;
            this.path = path;
        }

        public async Task<WebRedirectResult> Execute(int appID)
        {
            var app = await factory.Apps().App(appID);
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var modifier = await appsModCategory.Modifier(app.ID.Value);
            var redirectPath = path
                .WithNewGroup("App")
                .WithAction("Index")
                .WithModifier(modifier.ModKey());
            return new WebRedirectResult(redirectPath.Format());
        }
    }
}
