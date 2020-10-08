using HubWebApp.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using XTI_App;
using XTI_App.EF;
using XTI_App.Api;
using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;

namespace HubWebApp.Extensions
{
    public static class HubWebAppExtensions
    {
        public static void AddServicesForHub(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(optionsAction: (IServiceProvider sp, DbContextOptionsBuilder dbOptionsBuilder) =>
            {
                var dbOptions = sp.GetService<IOptions<DbOptions>>();
                var hostEnvironment = sp.GetService<IHostEnvironment>();
                dbOptionsBuilder.UseSqlServer(new AppConnectionString(dbOptions, hostEnvironment.EnvironmentName).Value())
                    .EnableSensitiveDataLogging();
            });
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddScoped<AccessForAuthenticate, JwtAccess>();
            services.AddScoped<AccessForLogin, CookieAccess>();
            services.AddScoped<AppApi>(sp =>
            {
                var xtiPath = sp.GetService<XtiPath>();
                return new HubAppApi
                (
                    new AppApiSuperUser(),
                    xtiPath.Version,
                    new AuthGroupFactory(sp),
                    new UserAdminGroupFactory(sp)
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
