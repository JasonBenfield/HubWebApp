using HubWebApp.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_Core;

namespace HubWebApp.Fakes
{
    public sealed class FakeHubAppApi
    {
        private readonly IServiceProvider sp;

        public FakeHubAppApi(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public async Task<HubAppApi> Create()
        {
            var api = new HubAppApi
            (
                new AppApiSuperUser(),
                AppVersionKey.Current,
                sp
            );
            var appFactory = sp.GetService<AppFactory>();
            var clock = sp.GetService<Clock>();
            await new AllAppSetup(appFactory, clock).Run();
            await new HubSetup(appFactory, clock).Run();
            return api;
        }
    }
}
