using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using XTI_App;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services)
        {
            services.AddSingleton<IHashedPasswordFactory, FakeHashedPasswordFactory>();
            services.AddSingleton<AuthGroupFactory>(sp => new FakeAuthGroupFactory(sp));
            services.AddSingleton<UserAdminGroupFactory>(sp => new FakeUserAdminGroupFactory(sp));
            services.AddSingleton(sp => new FakeHubAppApi(sp));
        }
    }
}
