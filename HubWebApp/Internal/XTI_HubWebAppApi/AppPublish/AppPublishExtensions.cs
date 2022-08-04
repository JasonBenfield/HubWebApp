using XTI_HubWebAppApi.AppPublish;

namespace XTI_HubWebAppApi;

internal static class AppPublishExtensions
{
    public static void AddAppPublishGroupServices(this IServiceCollection services)
    {
        services.AddScoped<BeginPublishAction>();
        services.AddScoped<EndPublishAction>();
        services.AddScoped<GetVersionsAction>();
        services.AddScoped<NewVersionValidation>();
        services.AddScoped<NewVersionAction>();
    }
}