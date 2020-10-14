using HubWebApp.client;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using XTI_App;
using XTI_Configuration.Extensions;
using XTI_Credentials;
using XTI_Secrets;
using XTI_Secrets.Extensions;
using XTI_WebApp.Fakes;
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
            await input.HubClient.UserAdmin.AddUser(addUserModel);
            input.HubClient.ResetToken();
            input.TestCredentials.Source = new SimpleCredentials(new CredentialValue(addUserModel.UserName, addUserModel.Password));
            var ex = Assert.ThrowsAsync<AppClientException>(async () =>
            {
                await input.HubClient.UserAdmin.AddUser(new AddUserModel
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
            var hostEnv = new FakeHostEnvironment { EnvironmentName = "Test" };
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.UseXtiConfiguration(hostEnv.EnvironmentName, new string[] { });
            var configuration = configurationBuilder.Build();
            services.Configure<AppOptions>(configuration.GetSection(AppOptions.App));
            services.AddHttpClient();
            services.AddDataProtection();
            services.AddFileSecretCredentials();
            services.AddScoped<IHostEnvironment>(sp => hostEnv);
            services.Configure<SecretOptions>(configuration.GetSection(SecretOptions.Secret));
            var secretOptions = configuration.GetSection(SecretOptions.Secret).Get<SecretOptions>();
            services
                .AddDataProtection
                (
                    options => options.ApplicationDiscriminator = secretOptions.ApplicationName
                )
                .PersistKeysToFileSystem(new DirectoryInfo(secretOptions.KeyDirectoryPath))
                .SetApplicationName(secretOptions.ApplicationName);
            services.AddScoped<ICredentials, TestCredentials>(sp =>
            {
                var secretCredentialsFactory = sp.GetService<SecretCredentialsFactory>();
                var secretCredentials = secretCredentialsFactory.Create("HubAdmin");
                return new TestCredentials(secretCredentials);
            });
            services.AddScoped(sp =>
            {
                var httpClientFactory = sp.GetService<IHttpClientFactory>();
                var credentials = sp.GetService<ICredentials>();
                var appOptions = sp.GetService<IOptions<AppOptions>>().Value;
                return new HubAppClient
                (
                    httpClientFactory,
                    credentials,
                    appOptions.BaseUrl,
                    "Current"
                );
            });
            var sp = services.BuildServiceProvider();
            return new TestInput(sp);
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