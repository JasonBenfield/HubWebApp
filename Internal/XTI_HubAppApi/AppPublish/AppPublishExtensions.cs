using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.AppPublish;

namespace XTI_HubAppApi;

internal static class AppPublishExtensions
{
    public static void AddAppPublishGroupServices(this IServiceCollection services)
    {
        services.AddScoped<BeginPublishAction>();
        services.AddScoped<EndPublishAction>();
        services.AddScoped<GetVersionsAction>();
        services.AddScoped<NewVersionValidation>();
        services.AddScoped<NextVersionKeyAction>();
        services.AddScoped<NewVersionAction>();
    }
}