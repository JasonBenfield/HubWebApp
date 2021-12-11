using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.AppUserInquiry;

namespace XTI_HubAppApi
{
    internal static class AppUserExtensions
    {
        public static void AddAppUserGroupServices(this IServiceCollection services)
        {
            services.AddScoped<IndexAction>();
            services.AddScoped<GetUserModifierCategoriesAction>();
            services.AddScoped<GetUserRoleAccessAction>();
            services.AddScoped<GetUserRolesAction>();
        }
    }
}
