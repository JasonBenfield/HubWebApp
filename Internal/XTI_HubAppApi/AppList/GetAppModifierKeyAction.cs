using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.AppList
{
    public sealed class GetAppModifierKeyAction : AppAction<AppKey, string>
    {
        private readonly AppFactory factory;

        public GetAppModifierKeyAction(AppFactory factory)
        {
            this.factory = factory;
        }

        public async Task<string> Execute(AppKey appKey)
        {
            var app = await factory.Apps.App(appKey);
            var hubApp = await factory.Apps.App(HubInfo.AppKey);
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var modifier = await appsModCategory.Modifier(app.ID.Value);
            return modifier.ModKey().Value;
        }
    }
}
