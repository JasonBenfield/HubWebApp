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

        public HubSetup(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task Run()
        {
            await new DefaultAppSetup
            (
                appFactory,
                clock,
                new HubAppApiTemplateFactory().Create(),
                "Hub"
            ).Run();
            var hubApp = await appFactory.Apps().App(HubAppKey.Key);
            var appModCategory = await hubApp.ModCategory(new ModifierCategoryName("Apps"));
            var apps = await appFactory.Apps().All();
            foreach (var app in apps)
            {
                await appModCategory.AddOrUpdateModifier(app.ID.Value, app.Title);
            }
        }
    }
}
