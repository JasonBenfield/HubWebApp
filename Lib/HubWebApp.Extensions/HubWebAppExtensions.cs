using HubWebApp.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using XTI_App;
using XTI_App.EF;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions
{
    public static class HubWebAppExtensions
    {
        public static void AddServicesForHub(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(optionsAction: (IServiceProvider sp, DbContextOptionsBuilder dbOptions) =>
            {
                var webAppOptions = sp.GetService<IOptions<WebAppOptions>>().Value;
                dbOptions.UseSqlServer(webAppOptions.ConnectionString)
                    .EnableSensitiveDataLogging();
            });
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddScoped<AppApi>(sp =>
            {
                var authGroupFactory = new DiAuthGroupFactory(sp);
                var userAdminFactory = new DiUserAdminFactory(sp);
                return new HubAppApi
                (
                    new SuperUser(),
                    authGroupFactory,
                    userAdminFactory
                );
            });
            services.AddScoped(sp => (HubAppApi)sp.GetService<AppApi>());
            services
                .AddMvc()
                .AddMvcOptions(options =>
                {
                });
            services.AddControllersWithViews();
        }
    }
}
