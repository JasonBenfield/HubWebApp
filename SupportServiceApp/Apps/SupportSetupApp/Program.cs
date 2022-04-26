using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportSetupApp;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_AppSetupApp.Extensions;
using XTI_SupportServiceAppApi;

await XtiSetupAppHost.CreateDefault(SupportInfo.AppKey, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton(_ => AppVersionKey.Current);
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddScoped<IAppSetup, SupportAppSetup>();
    })
    .RunConsoleAsync();