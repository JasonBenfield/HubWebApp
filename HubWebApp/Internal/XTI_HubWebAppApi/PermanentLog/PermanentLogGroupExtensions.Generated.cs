using XTI_HubWebAppApiActions.PermanentLog;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class PermanentLogGroupExtensions
{
    internal static void AddPermanentLogServices(this IServiceCollection services)
    {
        services.AddScoped<LogBatchAction>();
        services.AddScoped<LogSessionDetailsAction>();
    }
}