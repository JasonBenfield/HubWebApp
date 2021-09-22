using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Configuration.Extensions;
using XTI_Credentials;
using XTI_HubAppClient;
using XTI_Secrets;
using XTI_Secrets.Extensions;
using XTI_WebAppClient;

namespace HubWebApp.EndToEndTests
{
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
            await input.HubClient.Users.AddUser(addUserModel);
            input.HubClient.ResetToken();
            input.TestCredentials.Source = new SimpleCredentials(new CredentialValue(addUserModel.UserName, addUserModel.Password));
            var ex = Assert.ThrowsAsync<AppClientException>(async () =>
            {
                await input.HubClient.Users.AddUser(new AddUserModel
                {
                    UserName = "TestUser2",
                    Password = "Password12345"
                });
            });
            Assert.That(ex.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Forbidden));
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
                        services.AddDataProtection();
                        services.AddFileSecretCredentials();
                        services.AddSingleton(sp =>
                        {
                            var credentialsFactory = sp.GetService<SecretCredentialsFactory>();
                            var credentials = credentialsFactory.Create("TEST");
                            return new XtiTokenFactory(credentials);
                        });
                        services.AddSingleton<ICredentials, TestCredentials>();
                        services.AddScoped(sp =>
                        {
                            var httpClientFactory = sp.GetService<IHttpClientFactory>();
                            var tokenFactory = sp.GetService<IXtiTokenFactory>();
                            var appOptions = sp.GetService<IOptions<AppOptions>>().Value;
                            return new HubAppClient
                            (
                                httpClientFactory,
                                tokenFactory,
                                appOptions.BaseUrl,
                                "Current"
                            );
                        });
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
                HubClient = sp.GetService<HubAppClient>();
                TestCredentials = (TestCredentials)sp.GetService<ICredentials>();
            }
            public HubAppClient HubClient { get; }
            public TestCredentials TestCredentials { get; }
        }
    }
}