﻿using XTI_HubWebAppApi.AppUserInquiry;

namespace XTI_HubWebAppApi;

internal static class AppUserExtensions
{
    public static void AddAppUserGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<GetAssignedRolesAction>();
        services.AddScoped<GetExplicitlyUnassignedRolesAction>();
        services.AddScoped<GetExplicitUserAccessAction>();
    }
}