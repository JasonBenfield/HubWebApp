using XTI_HubWebAppApiActions.UserInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class UserInquiryGroupExtensions
{
    internal static void AddUserInquiryServices(this IServiceCollection services)
    {
        services.AddScoped<GetUserAction>();
        services.AddScoped<GetUserAuthenticatorsAction>();
        services.AddScoped<GetUserOrAnonAction>();
        services.AddScoped<GetUsersAction>();
    }
}