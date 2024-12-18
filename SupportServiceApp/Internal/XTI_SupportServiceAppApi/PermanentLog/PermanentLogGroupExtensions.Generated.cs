using XTI_SupportServiceAppApi.PermanentLog;

// Generated Code
namespace XTI_SupportServiceAppApi;
internal static partial class PermanentLogGroupExtensions
{
    internal static void AddPermanentLogServices(this IServiceCollection services)
    {
        services.AddScoped<MoveToPermanentAction>();
        services.AddScoped<MoveToPermanentV1Action>();
        services.AddScoped<RetryAction>();
        services.AddScoped<RetryV1Action>();
    }
}