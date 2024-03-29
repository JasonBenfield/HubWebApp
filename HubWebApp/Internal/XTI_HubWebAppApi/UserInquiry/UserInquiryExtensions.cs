﻿using XTI_HubWebAppApi.UserInquiry;

namespace XTI_HubWebAppApi;

internal static class UserInquiryExtensions
{
    public static void AddUserInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetUserAction>();
        services.AddScoped<GetUserAuthenticatorsAction>();
        services.AddScoped<GetUserOrAnonAction>();
        services.AddScoped<GetUsersAction>();
    }
}