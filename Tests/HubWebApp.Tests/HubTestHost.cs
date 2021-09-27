using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Fakes;
using XTI_Hub;
using XTI_HubAppApi;

namespace HubWebApp.Tests
{
    public sealed class HubTestHost
    {
        private IServiceScope scope;

        public async Task<IServiceProvider> Setup(Action<HostBuilderContext, IServiceCollection> configure = null)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddFakesForHubWebApp(hostContext.Configuration);
                        if (configure != null)
                        {
                            configure(hostContext, services);
                        }
                    }
                )
                .Build();
            scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;
            var setup = sp.GetService<IAppSetup>();
            await setup.Run(AppVersionKey.Current);
            var adminUser = await addAdminUser(sp);
            return sp;
        }

        private async Task<AppUser> addAdminUser(IServiceProvider services)
        {
            var factory = services.GetService<AppFactory>();
            var adminUser = await factory.Users().Add(new AppUserName("hubadmin"), new FakeHashedPassword("Password12345"), DateTime.UtcNow);
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var adminRole = await hubApp.Role(HubInfo.Roles.Admin);
            await adminUser.AddRole(adminRole);
            return adminUser;
        }
    }
}
