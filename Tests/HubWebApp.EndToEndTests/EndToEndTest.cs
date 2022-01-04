using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Configuration.Extensions;
using XTI_Credentials;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_Secrets;
using XTI_Secrets.Extensions;
using XTI_WebAppClient;

namespace HubWebApp.EndToEndTests;

public class EndToEndTest
{
    [Test]
    public async Task ShouldLogin()
    {
        var input = setup();
        var addUserModel = new AddUserModel
        {
            UserName = "TestUser1",
            Password = "Password12345"
        };
        await input.HubClient.Users.AddOrUpdateUser(addUserModel);
        input.HubClient.ResetToken();
        input.TestCredentials.Source = new SimpleCredentials(new CredentialValue(addUserModel.UserName, addUserModel.Password));
        var ex = Assert.ThrowsAsync<AppClientException>(async () =>
        {
            await input.HubClient.Users.AddOrUpdateUser(new AddUserModel
            {
                UserName = "TestUser2",
                Password = "Password12345"
            });
        });
        Assert.That(ex?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Forbidden));
        Console.WriteLine(ex);

    }

    private TestInput setup()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Test");
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration
            (
                (hostContext, config) =>
                {
                    config.UseXtiConfiguration(hostContext.HostingEnvironment, new string[] { });
                    config.AddInMemoryCollection
                    (
                        new[]
                        {
                                KeyValuePair.Create("Authenticator:CredentialKey", "HubAdmin")
                        }
                    );
                }
            )
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.Configure<AppOptions>(hostContext.Configuration.GetSection(AppOptions.App));
                    services.AddHttpClient();
                    services.AddFileSecretCredentials(hostContext.HostingEnvironment);
                    services.AddSingleton(sp =>
                    {
                        var credentialsFactory = sp.GetRequiredService<SecretCredentialsFactory>();
                        var credentials = credentialsFactory.Create("TEST");
                        return new XtiTokenFactory(credentials);
                    });
                    services.AddSingleton<ICredentials, TestCredentials>();
                    services.AddHubClientServices(hostContext.Configuration);
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        return new TestInput(scope.ServiceProvider);
    }

    public sealed class TestCredentials : ICredentials
    {
        public TestCredentials(ICredentials source)
        {
            Source = source;
        }

        public ICredentials Source { get; set; }

        public Task<CredentialValue> Value() => Source.Value();
    }

    public sealed class TestInput
    {
        public TestInput(IServiceProvider sp)
        {
            HubClient = sp.GetRequiredService<HubAppClient>();
            TestCredentials = (TestCredentials)sp.GetRequiredService<ICredentials>();
        }
        public HubAppClient HubClient { get; }
        public TestCredentials TestCredentials { get; }
    }
}