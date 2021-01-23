using HubWebApp.Core;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_Core;

namespace HubWebApp.Api
{
    public sealed class HubSetup
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

        public async Task Run()
        {
            await new AllAppSetup(appFactory, clock).Run();
            await new DefaultAppSetup
            (
                appFactory,
                clock,
                apiFactory.CreateTemplate(),
                "Hub"
            ).Run();
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
