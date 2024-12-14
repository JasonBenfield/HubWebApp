using XTI_HubWebAppApiActions.AppPublish;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class PublishApiGroupExtensions
{
    internal static void AddPublishServices(this IServiceCollection services)
    {
        services.AddScoped<BeginPublishAction>();
        services.AddScoped<EndPublishAction>();
        services.AddScoped<GetVersionsAction>();
        services.AddScoped<NewVersionAction>();
        services.AddScoped<NewVersionValidation>();
    }
}