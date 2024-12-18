using XTI_HubWebAppApiActions.System;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class SystemGroupExtensions
{
    internal static void AddSystemServices(this IServiceCollection services)
    {
        services.AddScoped<AddOrUpdateModifierByModKeyAction>();
        services.AddScoped<AddOrUpdateModifierByTargetKeyAction>();
        services.AddScoped<GetAppContextAction>();
        services.AddScoped<GetModifierAction>();
        services.AddScoped<GetStoredObjectAction>();
        services.AddScoped<GetStoredObjectValidation>();
        services.AddScoped<GetUserAuthenticatorsAction>();
        services.AddScoped<GetUserByUserNameAction>();
        services.AddScoped<GetUserOrAnonAction>();
        services.AddScoped<GetUserRolesAction>();
        services.AddScoped<GetUsersWithAnyRoleAction>();
        services.AddScoped<SetUserAccessAction>();
        services.AddScoped<StoreObjectAction>();
        services.AddScoped<StoreObjectValidation>();
    }
}