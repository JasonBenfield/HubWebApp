﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppClient.Extensions;
using XTI_App.Extensions;
using XTI_Core.Extensions;
using XTI_Core;
using XTI_Secrets.Extensions;
using XTI_WebApp.Api;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.WebApp.Extensions;

public static class XtiWebAppHost
{
    public static WebApplicationBuilder CreateDefault(AppKey appKey, string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.UseXtiConfiguration(builder.Environment, appKey.Name.DisplayText, appKey.Type.DisplayText, args);
        builder.Services.AddSingleton(_ => appKey);
        builder.Services.AddResponseCaching();
        builder.Services.AddAppServices();
        builder.Services.AddFileSecretCredentials(XtiEnvironment.Parse(builder.Environment.EnvironmentName));
        builder.Services.AddHubClientServices();
        builder.Services.AddScoped<HubAppClientContext>();
        builder.Services.AddScoped<SystemUserXtiToken>();
        builder.Services.AddScoped<AuthCookieXtiToken>();
        builder.Services.AddXtiTokenAccessor((sp, tokenAccessor) =>
        {
            tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
            tokenAccessor.UseToken<SystemUserXtiToken>();
            tokenAccessor.AddToken(() => sp.GetRequiredService<AuthCookieXtiToken>());
        });
        XTI_WebApp.Extensions.WebAppExtensions.AddWebAppServices(builder.Services);
        builder.Services.AddScoped<ILoginReturnKey, LoginReturnKey>();
        builder.Services.AddScoped<LoginUrl>();
        builder.Services.AddAppClients((sp, domains) => { });
        builder.Services.AddAppClientDomainSelector((sp, domains) => { });
        builder.Services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HcAppContext>());
        builder.Services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HcUserContext>());
        return builder;
    }
}
