using XTI_HubAppApi.UserInquiry;

namespace XTI_HubAppApi;

internal static class UserInquiryExtensions
{
    public static void AddUserInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetCurrentUserAction>();
        services.AddScoped<GetUserAction>();
        services.AddScoped<GetUserByUserNameAction>();
        services.AddScoped<RedirectToAppUserAction>();
    }
}