using HubWebApp.AuthApi;
using Microsoft.Extensions.DependencyInjection;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services)
        {
            services.AddScoped<AccessForAuthenticate, FakeAccessForAuthenticate>();
            services.AddScoped<AccessForLogin, FakeAccessForLogin>();
            services.AddScoped(sp => new FakeHubAppApi(sp));
        }
    }
}
