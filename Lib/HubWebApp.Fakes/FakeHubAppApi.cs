using HubWebApp.Api;
using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

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
            var authGroupFactory = new AuthGroupFactory(sp);
            var userAdminGroupFactory = new UserAdminGroupFactory(sp);
            var api = new HubAppApi
            (
                new AppApiSuperUser(),
                "Current",
                authGroupFactory,
                userAdminGroupFactory
            );
            var appFactory = sp.GetService<AppFactory>();
            await new AppSetup(appFactory).Run();
            var clock = sp.GetService<Clock>();
            await new HubSetup(appFactory, clock).Run();
            return api;
        }
    }
}
