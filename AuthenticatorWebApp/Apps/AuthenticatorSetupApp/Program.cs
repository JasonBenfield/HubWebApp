using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuthenticatorSetupApp;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_AppSetupApp.Extensions;
using XTI_AuthenticatorWebAppApi;

await XtiSetupAppHost.CreateDefault(AuthenticatorInfo.AppKey, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton(_ => AppVersionKey.Current);
        services.AddScoped<AppApiFactory, AuthenticatorAppApiFactory>();
        services.AddScoped<IAppSetup, AuthenticatorAppSetup>();
    })
    .RunConsoleAsync();