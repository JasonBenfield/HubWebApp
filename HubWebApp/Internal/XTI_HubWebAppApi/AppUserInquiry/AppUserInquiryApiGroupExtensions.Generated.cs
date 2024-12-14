using XTI_HubWebAppApiActions.AppUserInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AppUserInquiryApiGroupExtensions
{
    internal static void AddAppUserInquiryServices(this IServiceCollection services)
    {
        services.AddScoped<GetAssignedRolesAction>();
        services.AddScoped<GetExplicitlyUnassignedRolesAction>();
        services.AddScoped<GetExplicitUserAccessAction>();
        services.AddScoped<IndexAction>();
    }
}