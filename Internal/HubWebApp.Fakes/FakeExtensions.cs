using HubWebApp.Api;
using HubWebApp.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddFakesForXtiWebApp(config);
            services.AddScoped(sp =>
            {
                var user = sp.GetService<IAppApiUser>();
                return new HubAppApi
                (
                    user,
                    AppVersionKey.Current,
                    sp
                );
            });
            services.AddScoped<AppApi>(sp => sp.GetService<HubAppApi>());
            services.AddScoped<HubSetup>();
            services.AddScoped(_ => HubAppKey.Key);
        }
    }
}
