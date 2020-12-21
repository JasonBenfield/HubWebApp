using HubWebApp.Api;
using HubWebApp.ApiControllers;
using HubWebApp.Core;
using HubWebApp.UserAdminApi;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Extensions;

namespace HubWebApp.Extensions
{
    public static class HubWebAppExtensions
    {
        public static void AddServicesForHub(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebAppServices(configuration);
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddSingleton(_ => HubAppKey.Key);
            services.AddScoped<AppApi>(sp =>
            {
                var appApiUser = sp.GetService<IAppApiUser>();
                var xtiPath = sp.GetService<XtiPath>();
                return new HubAppApi
                (
                    appApiUser,
                    xtiPath.Version,
                    sp
                );
            });
            services.AddScoped(sp => (HubAppApi)sp.GetService<AppApi>());
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddMvcOptions(options =>
                {
                });
            services.AddControllersWithViews()
                .PartManager.ApplicationParts.Add
                (
                    new AssemblyPart(typeof(UserAdminController).Assembly)
                );
        }
    }
}
