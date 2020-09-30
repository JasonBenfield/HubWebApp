using HubWebApp.Api;
using Microsoft.Extensions.DependencyInjection;
using XTI_App;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions
{
    public static class HubWebAppExtensions
    {
        public static void AddServicesForHub(this IServiceCollection services)
        {
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
