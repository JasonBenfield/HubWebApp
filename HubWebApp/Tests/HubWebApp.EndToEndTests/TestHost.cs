using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Credentials;
using XTI_HubAppClient.Extensions;
using XTI_Secrets;
using XTI_Secrets.Extensions;
using XTI_WebAppClient;

namespace HubWebApp.EndToEndTests;

internal sealed class TestHost
{
    public IServiceProvider Setup(AppKey appKey, string envName = "Test")
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", envName);
        var xtiEnv = XtiEnvironment.Parse(envName);
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration
            (
                (hostContext, config) =>
                {
                    config.UseXtiConfiguration(xtiEnv, appKey, new string[0]);
                }
            )
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddMemoryCache();
                    services.AddSingleton(_ => appKey);
                    services.AddFileSecretCredentials(xtiEnv);
                    services.AddScoped<ISystemUserCredentials, SystemUserCredentials>();
                    services.AddScoped<SystemUserXtiToken>();
                    services.AddScoped
                    (
                        sp =>
                        {
                            var authClient = sp.GetRequiredService<IAuthClient>();
                            var credentialsFactory = sp.GetRequiredService<SecretCredentialsFactory>();
                            var credentials = credentialsFactory.Create("TEST");
                            return new TesterXtiToken(authClient, credentials);
                        }
                    );
                    services.AddScoped
                    (
                        sp =>
                        {
                            var authClient = sp.GetRequiredService<IAuthClient>();
                            return new NewUserXtiToken(authClient, new SimpleCredentials(NewUserXtiToken.NewUserCredentials));
                        }
                    );
                    services.AddHubClientServices();
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
                        tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                        tokenAccessor.AddToken(() => sp.GetRequiredService<TesterXtiToken>());
                        tokenAccessor.UseToken<TesterXtiToken>();
                        tokenAccessor.AddToken(() => sp.GetRequiredService<NewUserXtiToken>());
                    });
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        return scope.ServiceProvider;
    }
}
