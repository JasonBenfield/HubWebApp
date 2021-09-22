using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.UserInquiry
{
    public sealed class RedirectToAppUserAction : AppAction<RedirectToAppUserRequest, WebRedirectResult>
    {
        private readonly AppFactory factory;
        private readonly XtiPath path;
        private readonly HubAppApi hubApi;

        public RedirectToAppUserAction(AppFactory factory, XtiPath path, HubAppApi hubApi)
        {
            this.factory = factory;
            this.path = path;
            this.hubApi = hubApi;
        }

        public async Task<WebRedirectResult> Execute(RedirectToAppUserRequest model)
        {
            var app = await factory.Apps().App(model.AppID);
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var modifier = await appsModCategory.Modifier(app.ID.Value);
            var redirectPath = path
                .WithNewGroup(hubApi.AppUser.Index.Path)
                .WithModifier(modifier.ModKey());
            var url = $"{redirectPath.Format()}?userID={model.UserID}";
            return new WebRedirectResult(url);
        }
    }
}
