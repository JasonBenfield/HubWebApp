using HubWebApp.Core;
using System.Threading.Tasks;
using XTI_App;
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
            await new AllAppSetup(appFactory, clock).Run();
            var hubApp = await appFactory.Apps().App(HubAppKey.Key);
            const string title = "Hub";
            if (hubApp.Key().Equals(HubAppKey.Key))
            {
                await hubApp.SetTitle(title);
            }
            else
            {
                hubApp = await appFactory.Apps().Add(HubAppKey.Key, title, clock.Now());
            }
            var currentVersion = await hubApp.CurrentVersion();
            if (!currentVersion.IsCurrent())
            {
                currentVersion = await hubApp.StartNewMajorVersion(clock.Now());
                await currentVersion.Publishing();
                await currentVersion.Published();
            }
            await hubApp.SetRoles(HubRoles.Instance.Values());
            var appModCategory = await hubApp.TryAddModCategory(new ModifierCategoryName("Apps"));
            var apps = await appFactory.Apps().All();
            foreach (var app in apps)
            {
                await appModCategory.AddOrUpdateModifier(app.ID.Value, app.Title);
            }
        }
    }
}
