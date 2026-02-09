using XTI_SupportServiceAppApiActions.PermanentLog;

// Generated Code
namespace XTI_SupportServiceAppApi;
internal static partial class PermanentLogGroupExtensions
{
    internal static void AddPermanentLogServices(this IServiceCollection services)
    {
        services.AddScoped<MoveToPermanentAction>();
        services.AddScoped<RetryAction>();
    }
}