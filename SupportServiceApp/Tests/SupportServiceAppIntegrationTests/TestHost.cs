using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Hosting;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_PermanentLog;
using XTI_PermanentLog.Implementations;
using XTI_Secrets.Extensions;
using XTI_SupportServiceAppApi;
using XTI_TempLog;
using XTI_TempLog.Abstractions;
using XTI_TempLog.Extensions;

namespace SupportServiceAppIntegrationTests;

internal sealed class TestHost
{
    public static XtiHost CreateDefault(XtiEnvironment xtiEnv)
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", xtiEnv.EnvironmentName);
        var host = new XtiHostBuilder(xtiEnv);
        host.Services.AddSingleton(_ => SupportAppKey.Value);
        host.Services.AddAppServices();
        host.Services.AddFileSecretCredentials(xtiEnv);
        host.Services.AddScoped(sp =>
        {
            var xtiFolder = sp.GetRequiredService<XtiFolder>();
            var appKey = sp.GetRequiredService<AppKey>();
            return xtiFolder.AppDataFolder(appKey);
        });
        host.Services.AddSingleton<CurrentSession>();
        host.Services.AddScoped<IActionRunnerFactory, ActionRunnerFactory>();
        host.Services.AddSingleton<ISystemUserCredentials, SystemUserCredentials>();
        host.Services.AddSingleton<ICurrentUserName, SystemCurrentUserName>();
        host.Services.AddSingleton<IAppEnvironmentContext, AppEnvironmentContext>();
        host.Services.AddHostedService<AppAgendaHostedService>();
        host.Services.AddHubClientServices();
        host.Services.AddScoped<SystemUserXtiToken>();
        host.Services.AddXtiTokenAccessorFactory((sp, tokenAccessorFactory) =>
        {
            tokenAccessorFactory.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
            tokenAccessorFactory.UseDefaultToken<SystemUserXtiToken>();
        });
        host.Services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HcAppContext>());
        host.Services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HcUserContext>());
        host.Services.AddScoped<IAppApiUser, AppApiSuperUser>();

        host.Services.AddMemoryCache();
        host.Services.AddSingleton<IHostEnvironment>(new HostingEnvironment
        {
            EnvironmentName = xtiEnv.EnvironmentName
        });
        host.Services.AddSupportAppApiServices();
        host.Services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        host.Services.AddScoped(sp => (SupportAppApi)sp.GetRequiredService<IAppApi>());
        host.Services.AddScoped<ITempLogsV1>(sp =>
        {
            var dataProtector = sp.GetDataProtector("XTI_TempLog");
            var hostEnv = sp.GetRequiredService<IHostEnvironment>();
            var appKey = sp.GetRequiredService<AppKey>();
            var appDataFolder = sp.GetRequiredService<XtiFolder>().AppDataFolder();
            return new DiskTempLogsV1(dataProtector, appDataFolder.Path(), "TempLogs");
        });
        host.Services.AddScoped<EfPermanentLog>();
        host.Services.AddScoped<HcPermanentLog>();
        host.Services.AddScoped
        (
            sp =>
            {
                IPermanentLog permanentLog;
                var options = sp.GetRequiredService<SupportServiceAppOptions>();
                if (options.PermanentLogType == "DB")
                {
                    permanentLog = sp.GetRequiredService<EfPermanentLog>();
                }
                else
                {
                    permanentLog = sp.GetRequiredService<HcPermanentLog>();
                }
                return permanentLog;
            }
        );
        host.Services.AddScoped<TempToPermanentLog>();
        host.Services.AddScoped<TempToPermanentLogV1>();
        host.Services.AddFileSecretCredentials(xtiEnv);
        host.Services.AddScoped<InstallationUserCredentials>();
        host.Services.AddScoped<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
        host.Services.AddScoped<InstallationUserXtiToken>();
        host.Services.AddXtiTokenAccessorFactory((sp, accessorFactory) =>
        {
            accessorFactory.AddToken(() => sp.GetRequiredService<InstallationUserXtiToken>());
            accessorFactory.UseDefaultToken<InstallationUserXtiToken>();
        });
        return host.Build();
    }
}
