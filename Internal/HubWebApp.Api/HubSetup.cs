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
            var app = await appFactory.Apps().App(HubAppKey.Key);
            const string title = "Hub";
            if (app.Key().Equals(HubAppKey.Key))
            {
                await app.SetTitle(title);
            }
            else
            {
                app = await appFactory.Apps().Add(HubAppKey.Key, title, clock.Now());
            }
            var currentVersion = await app.CurrentVersion();
            if (!currentVersion.IsCurrent())
            {
                currentVersion = await app.StartNewMajorVersion(clock.Now());
                await currentVersion.Publishing();
                await currentVersion.Published();
            }
            await app.SetRoles(HubRoles.Instance.Values());
        }
    }
}
