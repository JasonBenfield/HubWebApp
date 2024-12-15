using XTI_HubWebAppApiActions;
using XTI_HubWebAppApiActions.System;

namespace XTI_HubWebAppApi;

static partial class HubApiExtensions
{
    static partial void AddMoreServices(this IServiceCollection services)
    {
        services.AddScoped<AppFromPath>();
        services.AddScoped<AppFromSystemUser>();
        services.AddScoped<CurrentAppUser>();
        services.AddScoped<UnverifiedUser>();
        services.AddScoped<UserGroupFromPath>();
    }
}
