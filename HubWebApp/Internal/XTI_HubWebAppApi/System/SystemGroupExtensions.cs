using XTI_HubWebAppApi.System;

namespace XTI_HubWebAppApi;

internal static class SystemGroupExtensions
{
    public static void AddSystemGroupServices(this IServiceCollection services)
    {
        services.AddScoped<AppFromSystemUser>();
        services.AddScoped<GetAppContextAction>();
        services.AddScoped<GetUserContextAction>();
        services.AddScoped<AddOrUpdateModifierByTargetKeyAction>();
    }
}