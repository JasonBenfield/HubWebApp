using HubWebApp.Api;
using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_WebApp.Api;

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
            var authGroupFactory = sp.GetService<AuthGroupFactory>();
            var userAdminGroupFactory = sp.GetService<UserAdminGroupFactory>();
            var api = new HubAppApi
            (
                new SuperUser(),
                authGroupFactory,
                userAdminGroupFactory
            );
            var appFactory = sp.GetService<AppFactory>();
            var setup = new AppSetup(appFactory);
            await setup.Run();
            return api;
        }
    }
}
