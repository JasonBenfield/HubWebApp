using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Credentials;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;
using XTI_Processes;

namespace InstallApp
{
    public sealed class InstallHostedService : IHostedService
    {
        private readonly IServiceProvider services;

        public InstallHostedService(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;
            try
            {
                var appKey = ensureAppKeyIsValid(sp);
                var credentials = await addSystemUser(sp, appKey);
                var appVersion = await retrieveVersion(sp, appKey);
                var hostEnv = sp.GetService<IHostEnvironment>();
                var options = sp.GetService<IOptions<InstallOptions>>().Value;
                if (string.IsNullOrWhiteSpace(options.DestinationMachine))
                {
                    await runLocalInstall(sp, appVersion.VersionKey, credentials);
                }
                else
                {
                    var release = $"v{appVersion.Major}.{appVersion.Minor}.{appVersion.Patch}";
                    var httpClientFactory = sp.GetService<IHttpClientFactory>();
                    using var client = httpClientFactory.CreateClient();
                    using var content = new FormUrlEncodedContent
                    (
                        new[]
                        {
                            KeyValuePair.Create("command", "install"),
                            KeyValuePair.Create("envName", hostEnv.EnvironmentName),
                            KeyValuePair.Create("appName", appKey.Name.Value),
                            KeyValuePair.Create("appType", appKey.Type.DisplayText.Replace(" ", "")),
                            KeyValuePair.Create("versionKey", appVersion.VersionKey),
                            KeyValuePair.Create("repoOwner", options.RepoOwner),
                            KeyValuePair.Create("repoName", options.RepoName),
                            KeyValuePair.Create("systemUserName", credentials.UserName),
                            KeyValuePair.Create("systemPassword", credentials.Password),
                            KeyValuePair.Create("release", release)
                        }
                    );
                    var response = await client.PostAsync($"http://{options.DestinationMachine}:61862", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.ExitCode = 999;
            }
            var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
            lifetime.StopApplication();
        }

        private static async Task<AppVersionModel> retrieveVersion(IServiceProvider sp, AppKey appKey)
        {
            AppVersionModel appVersion;
            var hostEnv = sp.GetService<IHostEnvironment>();
            var versionKey = AppVersionKey.Current.DisplayText;
            if (hostEnv.IsProduction())
            {
                var hubApi = sp.GetService<HubAppApi>();
                appVersion = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
                {
                    AppKey = appKey,
                    VersionKey = AppVersionKey.Current
                });
                versionKey = appVersion.VersionKey;
            }
            else
            {
                appVersion = new AppVersionModel
                {
                    VersionKey = AppVersionKey.Current.Value,
                    Major = 1,
                    Minor = 0,
                    Patch = 0
                };
            }
            return appVersion;
        }

        private static async Task<CredentialValue> addSystemUser(IServiceProvider sp, AppKey appKey)
        {
            Console.WriteLine("Adding system user");
            var machineName = getMachineName(sp);
            var dashIndex = machineName.IndexOf(".");
            if (dashIndex > -1)
            {
                machineName = machineName.Substring(0, dashIndex);
            }
            var hubApi = sp.GetService<HubAppApi>();
            var password = $"{Guid.NewGuid():N}?!";
            var systemUser = await hubApi.AppRegistration.AddSystemUser.Invoke(new AddSystemUserRequest
            {
                AppKey = appKey,
                MachineName = machineName,
                Password = password
            });
            var credentials = new CredentialValue(systemUser.UserName, password);
            Console.WriteLine($"Added system user '{systemUser.UserName}'");
            return credentials;
        }

        private static string getMachineName(IServiceProvider sp)
        {
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            string machineName;
            if (string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                machineName = Environment.MachineName;
            }
            else
            {
                machineName = options.DestinationMachine;
            }
            return machineName;
        }

        private static async Task copyToDestinationMachine(IServiceProvider sp, AppKey appKey, string versionKey)
        {
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            if (!string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                var hostEnv = sp.GetService<IHostEnvironment>();
                var sourceDir = getSourceDir(appKey, versionKey, hostEnv);
                var sourceAppDir = Path.Combine(sourceDir, "App");
                var targetDir = Path.Combine
                (
                    $"\\{options.DestinationMachine}",
                    "XTI",
                    "Published",
                    hostEnv.EnvironmentName,
                    $"{getAppType(appKey)}s",
                    getAppName(appKey),
                    versionKey
                );
                if (Directory.Exists(targetDir))
                {
                    Directory.Delete(targetDir, true);
                }
                var targetAppDir = Path.Combine(targetDir, "App");
                Console.WriteLine($"Copying from '{sourceAppDir}' to '{targetAppDir}'");
                await new RobocopyProcess(sourceAppDir, targetAppDir)
                    .Purge()
                    .CopySubdirectoriesIncludingEmpty()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
                var sourceSetupDir = Path.Combine(sourceDir, "Setup");
                var targetSetupDir = Path.Combine(targetDir, "Setup");
                Console.WriteLine($"Copying from '{sourceSetupDir}' to '{targetSetupDir}'");
                await new RobocopyProcess(sourceSetupDir, targetSetupDir)
                    .Purge()
                    .CopySubdirectoriesIncludingEmpty()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoDirectoryLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .Run();
            }
        }

        private static string getSourceDir(AppKey appKey, string versionKey, IHostEnvironment hostEnv)
        {
            return Path.Combine
            (
                getXtiDir(),
                "Published",
                hostEnv.EnvironmentName,
                $"{getAppType(appKey)}s",
                getAppName(appKey),
                versionKey
            );
        }

        private static async Task runLocalInstall(IServiceProvider sp, string versionKey, CredentialValue credential)
        {
            var hostEnv = sp.GetService<IHostEnvironment>();
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            var installProcessPath = Path.Combine
            (
                Environment.GetEnvironmentVariable("XTI_Dir"),
                "Tools",
                "LocalInstallApp",
                "LocalInstallApp.exe"
            );
            var installProcess = new XtiProcess(installProcessPath)
                .UseEnvironment(hostEnv.EnvironmentName)
                .AddConfigOptions
                (
                    new
                    {
                        AppName = options.AppName,
                        AppType = options.AppType,
                        VersionKey = versionKey,
                        SystemUserName = credential.UserName,
                        SystemPassword = credential.Password
                    }
                );
            if (string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                Console.WriteLine("Running Local Install");
                var result = await installProcess
                    .WriteOutputToConsole()
                    .Run();
                result.EnsureExitCodeIsZero();
            }
            else
            {
                Console.WriteLine($"Running Install on '{options.DestinationMachine}'");
                var result = await new PsExecProcess(options.DestinationMachine, installProcess)
                    .WriteOutputToConsole()
                    .Run();
                result.EnsureExitCodeIsZero();
            }
        }

        private static AppKey ensureAppKeyIsValid(IServiceProvider sp)
        {
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            if (string.IsNullOrWhiteSpace(options.AppName))
            {
                throw new ArgumentException("App Name is Required");
            }
            if (string.IsNullOrWhiteSpace(options.AppType))
            {
                throw new ArgumentException("App Type is Required");
            }
            var appName = new AppName(options.AppName);
            var appType = AppType.Values.Value(options.AppType);
            if (appType.Equals(AppType.Values.NotFound))
            {
                throw new ArgumentException($"App Type '{options.AppType}' is not valid");
            }
            return new AppKey(appName, appType);
        }

        private static string getXtiDir()
        {
            var xtiDir = Environment.GetEnvironmentVariable("XTI_Dir");
            if (string.IsNullOrWhiteSpace(xtiDir))
            {
                xtiDir = "c:\\xti";
            }
            return xtiDir;
        }

        private static string getAppName(AppKey appKey)
            => appKey.Name.DisplayText.Replace(" ", "");

        private static string getAppType(AppKey appKey)
        {
            var appType = appKey.Type.DisplayText;
            if (appKey.Type.Equals(AppType.Values.Service))
            {
                appType = "ServiceApp";
            }
            return appType.Replace(" ", "");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
