using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services)
        {
            services.AddSingleton<AuthGroupFactory>(sp => new FakeAuthGroupFactory(sp));
            services.AddSingleton<UserAdminGroupFactory>(sp => new FakeUserAdminGroupFactory(sp));
            services.AddSingleton(sp => new FakeHubAppApi(sp));
        }
    }
}
