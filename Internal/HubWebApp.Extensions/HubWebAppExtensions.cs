using HubWebApp.ApiControllers;
using MainDB.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.Auth;
using XTI_WebApp.Extensions;

namespace HubWebApp.Extensions
{
    public static class HubWebAppExtensions
    {
        public static void AddServicesForHub(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebAppServices(configuration);
            services.AddMainDbContextForSqlServer(configuration);
            services.AddScoped<AppFactory>();
            services.AddScoped<ISourceUserContext, WebUserContext>();
            services.AddScoped<ISourceAppContext, DefaultAppContext>();
            services.AddScoped<AppFromPath>();
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddScoped<AccessForAuthenticate, JwtAccess>();
            services.AddScoped<AccessForLogin, CookieAccess>();
            services.AddSingleton(_ => HubInfo.AppKey);
            services.AddScoped<AppApiFactory, HubAppApiFactory>();
            services.AddScoped(sp => (HubAppApi)sp.GetService<IAppApi>());
            services.AddScoped<DefaultAppSetup>();
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SetDefaultJsonOptions();
                })
                .AddMvcOptions(options =>
                {
                    options.SetDefaultMvcOptions();
                });
            services.AddControllersWithViews()
                .PartManager.ApplicationParts.Add
                (
                    new AssemblyPart(typeof(UsersController).Assembly)
                );
        }
    }
}
