using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebAppApi.Apps
{
    public sealed class RedirectToAppAction : AppAction<int, WebRedirectResult>
    {
        private readonly AppFactory factory;
        private readonly XtiPath path;
        private readonly HubAppApi hubApi;

        public RedirectToAppAction(AppFactory factory, XtiPath path, HubAppApi hubApi)
        {
            this.factory = factory;
            this.path = path;
            this.hubApi = hubApi;
        }

        public async Task<WebRedirectResult> Execute(int appID)
        {
            var app = await factory.Apps().App(appID);
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var modifier = await appsModCategory.Modifier(app.ID.Value);
            var redirectPath = path
                .WithNewGroup(hubApi.App.Index.Path)
                .WithModifier(modifier.ModKey());
            return new WebRedirectResult(redirectPath.Format());
        }
    }
}
