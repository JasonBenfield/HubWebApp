using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.EfApi;
using XTI_Core;

namespace HubWebAppApi
{
    public sealed class HubSetup : IAppSetup
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;
        private readonly AppApiFactory apiFactory;

        public HubSetup(AppFactory appFactory, Clock clock, AppApiFactory apiFactory)
        {
            this.appFactory = appFactory;
            this.clock = clock;
            this.apiFactory = apiFactory;
        }

        public async Task Run(AppVersionKey versionKey)
        {
            await new AllAppSetup(appFactory, clock).Run();
            var defaultAppSetup = new DefaultAppSetup
            (
                appFactory,
                clock,
                apiFactory.CreateTemplate(),
                "Hub"
            );
            await defaultAppSetup.Run(versionKey);
            var hubApp = await appFactory.Apps().App(HubInfo.AppKey);
            var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var apps = await appFactory.Apps().All();
            foreach (var app in apps)
            {
                await appModCategory.AddOrUpdateModifier(app.ID.Value, app.Title);
            }
        }
    }
}
