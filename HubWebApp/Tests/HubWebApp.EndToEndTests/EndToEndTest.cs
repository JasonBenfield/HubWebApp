using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_Secrets;
using XTI_Secrets.Extensions;
using XTI_WebAppClient;

namespace HubWebApp.EndToEndTests;

internal sealed class EndToEndTest
{
    private static readonly CredentialValue NewUserCredentials = new CredentialValue("TestUser1", "Password12345");

    [Test]
    public async Task ShouldLogin()
    {
        var sp = setup();
        var addUserModel = new AddOrUpdateUserRequest
        {
            UserName = NewUserCredentials.UserName,
            Password = NewUserCredentials.Password
        };
        var hubClient = sp.GetRequiredService<HubAppClient>();
        await hubClient.Users.AddOrUpdateUser("", addUserModel);
        hubClient.UseToken<NewUserXtiToken>();
        var ex = Assert.ThrowsAsync<AppClientException>(async () =>
        {
            await hubClient.Users.AddOrUpdateUser("", new AddOrUpdateUserRequest
            {
                UserName = "TestUser2",
                Password = "Password12345"
            });
        });
        Assert.That(ex?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Forbidden));
        Console.WriteLine(ex);

    }

    private IServiceProvider setup(string envName = "Test")
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", envName);
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration
            (
                (hostContext, config) =>
                {
                    config.UseXtiConfiguration(hostContext.HostingEnvironment, "", "", new string[0]);
                }
            )
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddFileSecretCredentials(XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName));
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
                            return new NewUserXtiToken(authClient, new SimpleCredentials(NewUserCredentials));
                        }
                    );
                    services.AddHubClientServices();
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
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

    public sealed class TesterXtiToken : AuthenticatorXtiToken
    {
        public TesterXtiToken(IAuthClient authClient, ICredentials credentials)
            : base(authClient, credentials)
        {
        }
    }

    public sealed class NewUserXtiToken : AuthenticatorXtiToken
    {
        public NewUserXtiToken(IAuthClient authClient, ICredentials credentials)
            : base(authClient, credentials)
        {
        }
    }
}