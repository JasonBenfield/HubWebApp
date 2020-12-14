using Microsoft.Extensions.DependencyInjection;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services)
        {
            services.AddScoped(sp => new FakeHubAppApi(sp));
        }
    }
}
