using HubWebAppApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;

namespace HubWebApp.Fakes
{
    public sealed class FakeAuthenticatorAppApi
    {
        private readonly IServiceProvider sp;

        public FakeAuthenticatorAppApi(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public async Task<HubAppApi> Create()
        {
            var api = new HubAppApi
            (
                new AppApiSuperUser(),
                sp
            );
            var appFactory = sp.GetService<AppFactory>();
            var clock = sp.GetService<Clock>();
            var apiFactory = sp.GetService<AppApiFactory>();
            await new HubSetup(appFactory, clock, apiFactory).Run(AppVersionKey.Current);
            return api;
        }
    }
}
