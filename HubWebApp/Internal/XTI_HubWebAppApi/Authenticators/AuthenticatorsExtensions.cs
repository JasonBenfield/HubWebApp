﻿using XTI_HubWebAppApi.Authenticators;

namespace XTI_HubWebAppApi;

internal static class AuthenticatorsExtensions
{
    public static void AddAuthenticatorsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<MoveAuthenticatorAction>();
        services.AddScoped<RegisterAuthenticatorAction>();
        services.AddScoped<RegisterUserAuthenticatorAction>();
        services.AddScoped<UserOrAnonByAuthenticatorAction>();
    }
}