﻿using XTI_HubWebAppApi.UserGroups;

namespace XTI_HubWebAppApi;

internal static class UserGroupsGroupExtensions
{
    public static void AddUserGroupsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<AddUserGroupIfNotExistsAction>();
        services.AddScoped<GetUserGroupsAction>();
    }
}