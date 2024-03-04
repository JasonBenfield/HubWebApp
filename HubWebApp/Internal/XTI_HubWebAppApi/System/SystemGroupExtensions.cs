using XTI_HubWebAppApi.System;

namespace XTI_HubWebAppApi;

internal static class SystemGroupExtensions
{
    public static void AddSystemGroupServices(this IServiceCollection services)
    {
        services.AddScoped<AppFromSystemUser>();
        services.AddScoped<GetAppContextAction>();
        services.AddScoped<GetModifierAction>();
        services.AddScoped<GetUserRolesAction>();
        services.AddScoped<GetUserOrAnonAction>();
        services.AddScoped<GetUsersWithAnyRoleAction>();
        services.AddScoped<GetUserAuthenticatorsAction>();
        services.AddScoped<AddOrUpdateModifierByTargetKeyAction>();
        services.AddScoped<AddOrUpdateModifierByModKeyAction>();
        services.AddScoped<SystemSetUserAccessAction>();
        services.AddScoped<SystemGetStoredObjectAction>();
        services.AddScoped<SystemStoreObjectAction>();
    }
}