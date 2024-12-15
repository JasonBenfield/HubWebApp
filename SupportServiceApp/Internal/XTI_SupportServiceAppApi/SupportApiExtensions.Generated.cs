// Generated Code
namespace XTI_SupportServiceAppApi;
public static partial class SupportApiExtensions
{
    public static void AddSupportAppApiServices(this IServiceCollection services)
    {
        services.AddInstallationsServices();
        services.AddPermanentLogServices();
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddMoreServices();
    }

    static partial void AddMoreServices(this IServiceCollection services);
}