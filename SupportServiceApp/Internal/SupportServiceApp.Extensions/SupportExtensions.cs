using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_SupportServiceAppApi;

namespace SupportServiceApp.Extensions;

public static class SupportExtensions
{
    public static void AddSupportServiceAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppApiUser, AppApiSuperUser>();
        services.AddSupportAppApiServices();
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddScoped(sp => (SupportAppApi)sp.GetRequiredService<IAppApi>());
    }
}