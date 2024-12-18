using XTI_HubWebAppApiActions.VersionInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class VersionGroupExtensions
{
    internal static void AddVersionServices(this IServiceCollection services)
    {
        services.AddScoped<GetVersionAction>();
    }
}