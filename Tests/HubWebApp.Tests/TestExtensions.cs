using HubWebApp.Fakes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_AppSetupApp.Extensions;
using XTI_Core;
using XTI_Core.Fakes;
using XTI_Hub;
using XTI_HubAppApi;

namespace HubWebApp.Tests
{
    public static class TestExtensions
    {
        public static void AddServicesForTests(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFakesForHubWebApp(configuration);
            services.AddSingleton<Clock, FakeClock>();
            services.AddScoped<AppFactory>();
            services.AddScoped<HubAppApiFactory>();
            services.AddScoped<AppApiFactory>(sp => sp.GetService<HubAppApiFactory>());
            services.AddScoped<FakeXtiPathAccessor>();
            services.AddScoped<IXtiPathAccessor>(sp => sp.GetService<FakeXtiPathAccessor>());
            services.AddScoped<FakeAppContext>();
            services.AddScoped<ISourceAppContext>(sp => sp.GetService<FakeAppContext>());
            services.AddScoped<IAppContext>(sp => sp.GetService<FakeAppContext>());
            services.AddScoped<FakeUserContext>();
            services.AddScoped<ISourceUserContext>(sp => sp.GetService<FakeUserContext>());
            services.AddScoped<IUserContext>(sp => sp.GetService<FakeUserContext>());
            services.AddScoped<IAppApiUser, AppApiUser>();
            services.AddScoped(sp => sp.GetService<HubAppApiFactory>().Create(sp.GetService<IAppApiUser>()));
            services.AddScoped<IAppSetup, DefaultAppSetup>();

        }
    }
}
