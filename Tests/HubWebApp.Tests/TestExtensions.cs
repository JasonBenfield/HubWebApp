using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_Core;
using XTI_Core.Fakes;
using MainDB.Extensions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_App.Abstractions;
using Microsoft.Extensions.Configuration;
using HubWebApp.Fakes;

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
