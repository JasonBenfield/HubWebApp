using XTI_AuthenticatorWebAppApi.Home;

// Generated Code
namespace XTI_AuthenticatorWebAppApi;
internal static partial class HomeGroupExtensions
{
    internal static void AddHomeServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}