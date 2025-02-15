﻿using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using HubWebApp.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Core.Fakes;
using XTI_HubDB.Extensions;
using XTI_HubWebAppApiActions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;

namespace HubWebApp.IntegrationTests;

internal sealed class HubTestHost
{
    public Task<IServiceProvider> Setup(string envName, Action<IServiceCollection>? configure = null)
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", envName);
        var builder = new XtiHostBuilder(XtiEnvironment.Parse(envName));
        builder.Services.AddSingleton<IHostEnvironment>
        (
            _ => new FakeHostEnvironment { EnvironmentName = envName }
        );
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<FakeClock>();
        builder.Services.AddSingleton<IClock>(sp => sp.GetRequiredService<FakeClock>());
        builder.Services.AddScoped<IAppApiUser, AppApiSuperUser>();
        builder.Services.AddSingleton(_ => AppVersionKey.Current);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<FakeCurrentUserName>();
        builder.Services.AddScoped<ICurrentUserName>(sp => sp.GetRequiredService<FakeCurrentUserName>());
        builder.Services.AddScoped<EfAppContext>();
        builder.Services.AddScoped<IAppContext, CachedAppContext>();
        builder.Services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<EfAppContext>());
        builder.Services.AddScoped<EfUserContext>();
        builder.Services.AddScoped<IUserContext, CachedUserContext>();
        builder.Services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<EfUserContext>());
        builder.Services.AddConfigurationOptions<DefaultWebAppOptions>();
        builder.Services.AddSingleton(sp => sp.GetRequiredService<DefaultWebAppOptions>().DB);
        builder.Services.AddHubDbContextForSqlServer();
        builder.Services.AddScoped<HubFactory>();
        builder.Services.AddScoped<EfPermanentLog>();
        builder.Services.AddScoped<AppFromPath>();
        builder.Services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        builder.Services.AddScoped<JwtAccess>();
        builder.Services.AddKeyedScoped<IAccess>
        (
            "Authenticate",
            (sp, key) => sp.GetRequiredService<JwtAccess>()
        );
        builder.Services.AddKeyedScoped<IAccess>
        (
            "Login",
            (sp, key) => new CookieAccess
            (
                sp.GetRequiredService<IHttpContextAccessor>(),
                sp.GetRequiredService<JwtAccess>()
            )
        );
        builder.Services.AddSingleton(_ => HubInfo.AppKey);
        builder.Services.AddScoped<AppApiFactory, HubAppApiFactory>();
        builder.Services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        builder.Services.AddScoped<IHubAdministration, EfHubAdministration>();
        builder.Services.AddScoped<ILoginReturnKey, LoginReturnKey>();
        builder.Services.AddHubAppApiServices();
        if (configure != null)
        {
            configure(builder.Services);
        }
        var sp = builder.Build().Scope();
        var currentUserName = sp.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(new AppUserName("JB"));
        return Task.FromResult(sp);
    }
}