using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Web.Administration;
using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Credentials;
using XTI_Processes;
using XTI_SecretsToolApi;

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
                var hostEnv = sp.GetService<IHostEnvironment>();
                await storeSystemUserCredentials(sp, appKey);
                var versionKey = AppVersionKey.Current.DisplayText;
                if (appKey.Type.Equals(AppType.Values.WebApp))
                {
                    if (hostEnv.IsProduction())
                    {
                        await installWebApp(sp, appKey, versionKey, versionKey);
                    }
                    await installWebApp(sp, appKey, versionKey, AppVersionKey.Current.DisplayText);
                }
                else if (appKey.Type.Equals(AppType.Values.Service))
                {
                    await installServiceApp(sp, appKey, versionKey);
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

        private async Task storeSystemUserCredentials(IServiceProvider sp, AppKey appKey)
        {
            var credentialKey = getCredentialKey(appKey);
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            var path = getSecretsToolPath();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var secretsOptions = new SecretsToolOptions
            {
                Command = "Store",
                CredentialKey = credentialKey,
                UserName = options.SystemUserName,
                Password = options.SystemPassword
            };
            var process = new XtiProcess(path)
                .WriteOutputToConsole()
                .UseEnvironment(hostEnv.EnvironmentName)
                .AddConfigOptions(secretsOptions);
            Console.WriteLine(process.CommandText());
            var result = await process.Run();
            result.EnsureExitCodeIsZero();
        }

        private string getCredentialKey(AppKey appKey)
            => $"System_User_{appKey.Type.DisplayText}_{appKey.Name.DisplayText}"
                .Replace(" ", "");

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

        private async Task installWebApp(IServiceProvider sp, AppKey appKey, string versionKey, string installVersionKey)
        {
            await prepareIis(sp, appKey, versionKey);
            var installDir = await copyToInstallDir(sp, appKey, versionKey, installVersionKey, false);
            var appOfflinePath = Path.Combine(installDir, "app_offline.htm");
            File.Delete(appOfflinePath);
        }

        private static async Task prepareIis(IServiceProvider sp, AppKey appKey, string versionKey)
        {
            var secretCredentialsValue = await retrieveCredentials(sp, "WebApp");
            using var server = new ServerManager();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var siteName = hostEnv.IsProduction() ? "WebApps" : hostEnv.EnvironmentName;
            var site = server.Sites[siteName];
            var appName = getAppName(appKey);
            var appPoolName = $"Xti_{hostEnv.EnvironmentName}_{appName}_{versionKey}";
            var appPool = server.ApplicationPools.FirstOrDefault(ap => ap.Name.Equals(appPoolName, StringComparison.OrdinalIgnoreCase));
            if (appPool == null)
            {
                Console.WriteLine($"Adding application pool '{appPoolName}'");
                var newAppPool = server.ApplicationPools.Add(appPoolName);
                newAppPool.ProcessModel.UserName = secretCredentialsValue.UserName;
                newAppPool.ProcessModel.Password = secretCredentialsValue.Password;
                newAppPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
                newAppPool.ManagedRuntimeVersion = "v4.0";
            }
            var baseApp = site.Applications.FirstOrDefault(a => a.Path == "/");
            var virtDirPath = $"/{appName}";
            var virtDir = baseApp.VirtualDirectories.FirstOrDefault(vd => vd.Path.Equals(virtDirPath, StringComparison.OrdinalIgnoreCase));
            var virtDirPhysPath = getAppInstallDir(hostEnv, appKey);
            if (virtDir == null)
            {
                Console.WriteLine($"Adding virtual directory '{virtDirPath}'");
                baseApp.VirtualDirectories.Add(virtDirPath, virtDirPhysPath);
            }
            var appPhysPath = Path.Combine(virtDirPhysPath, versionKey);
            var appPath = $"/{appName}/{versionKey}";
            var iisApp = site.Applications.FirstOrDefault(a => a.Path.Equals(appPath, StringComparison.OrdinalIgnoreCase));
            if (iisApp == null)
            {
                Console.WriteLine($"Adding application '{appPath}'");
                iisApp = site.Applications.Add(appPath, appPhysPath);
                iisApp.ApplicationPoolName = appPoolName;
            }
            server.CommitChanges();
            if (!Directory.Exists(appPhysPath))
            {
                Directory.CreateDirectory(appPhysPath);
            }
            var offlinePath = Path.Combine(appPhysPath, "app_offline.htm");
            using (var writer = new StreamWriter(offlinePath, false))
            {
                await writer.WriteAsync
                (
@"
<html>
    <head>
        <title>App Offline</title>
    </head>
    <body>
        <h1>Web App is Temporarily Offline</h1>
    </body>
</html>
"
                );
            }
            await Task.Delay(5000);
            Console.WriteLine($"Deleting files in '{appPhysPath}'");
            foreach (var file in Directory.GetFiles(appPhysPath).Where(f => !f.Equals(offlinePath, StringComparison.OrdinalIgnoreCase)))
            {
                File.Delete(file);
            }
            foreach (var directory in Directory.GetDirectories(appPhysPath))
            {
                Directory.Delete(directory, true);
            }
        }

        private static async Task<CredentialValue> retrieveCredentials(IServiceProvider sp, string credentialKey)
        {
            var path = getSecretsToolPath();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var options = new SecretsToolOptions
            {
                Command = "Get",
                CredentialKey = credentialKey
            };
            var process = new XtiProcess(path)
                .UseEnvironment(hostEnv.EnvironmentName)
                .AddConfigOptions(options);
            var result = await process.Run();
            result.EnsureExitCodeIsZero();
            var output = result.Data<SecretsToolOutput>();
            var secretCredentialsValue = new CredentialValue(output.UserName, output.Password);
            return secretCredentialsValue;
        }

        private static string getSecretsToolPath()
            => Path.Combine
            (
                getXtiDir(),
                "Tools",
                "Xti_SecretsTool",
                "Xti_SecretsTool.exe"
            );

        private static async Task installServiceApp(IServiceProvider sp, AppKey appKey, string versionKey)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var hostEnv = sp.GetService<IHostEnvironment>();
            var appName = getAppName(appKey);
            var serviceName = $"Xti_{hostEnv.EnvironmentName}_{appName}";
            var sc = getService(serviceName);
            if (sc == null)
            {
                var binPath = Path.Combine
                (
                    getAppInstallDir(hostEnv, appKey),
                    AppVersionKey.Current.DisplayText,
                    $"{appName}ServiceApp.exe"
                );
                binPath = $"{binPath} --Environment {hostEnv.EnvironmentName}";
                Console.WriteLine($"Creating service '{binPath}'");
                var secretCredentialsValue = await retrieveCredentials(sp, "ServiceApp");
                await new WinProcess("sc")
                    .UseArgumentNameDelimiter("")
                    .AddArgument("create")
                    .UseArgumentValueDelimiter("= ")
                    .AddArgument("start", "auto")
                    .AddArgument("binpath", new Quoted(binPath))
                    .AddArgument("obj", new Quoted(secretCredentialsValue.UserName))
                    .AddArgument("password", new Quoted(secretCredentialsValue.Password))
                    .Run();
                sc = getService(serviceName);
            }
            else if (sc.Status == ServiceControllerStatus.Running)
            {
                Console.WriteLine($"Stopping services '{sc.DisplayName}'");
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            if (hostEnv.IsProduction())
            {
                await copyToInstallDir(sp, appKey, versionKey, versionKey, true);
            }
            await copyToInstallDir(sp, appKey, versionKey, AppVersionKey.Current.DisplayText, true);
            Console.WriteLine($"Starting services '{sc.DisplayName}'");
            sc.Start();
#pragma warning restore CA1416 // Validate platform compatibility
        }

#pragma warning disable CA1416 // Validate platform compatibility
        private static ServiceController getService(string serviceName)
            => ServiceController
                .GetServices(".")
                .FirstOrDefault
                (
                    c => c.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase)
                );
#pragma warning restore CA1416 // Validate platform compatibility

        private static string getAppInstallDir(IHostEnvironment hostEnv, AppKey appKey)
        {
            var xtiDir = getXtiDir();
            var appName = getAppName(appKey);
            var appType = getAppType(appKey);
            return Path.Combine(xtiDir, "Apps", hostEnv.EnvironmentName, $"{appType}s", appName);
        }

        private static async Task<string> copyToInstallDir(IServiceProvider sp, AppKey appKey, string versionKey, string installVersionKey, bool purge)
        {
            var xtiDir = getXtiDir();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var appType = getAppType(appKey);
            var appName = getAppName(appKey);
            var publishDir = Path.Combine(xtiDir, "Published", hostEnv.EnvironmentName, $"{appType}s", appName, versionKey, "App");
            var installDir = Path.Combine(getAppInstallDir(hostEnv, appKey), installVersionKey);
            Console.WriteLine($"Copying from '{publishDir}' to '{installDir}'");
            var process = new RobocopyProcess(publishDir, installDir)
                .CopySubdirectoriesIncludingEmpty()
                .NoDirectoryLogging()
                .NoFileClassLogging()
                .NoFileLogging()
                .NoFileSizeLogging()
                .NoJobHeader()
                .NoJobSummary()
                .NoProgressDisplayed();
            if (purge)
            {
                process.Purge();
            }
            await process.Run();
            return installDir;
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
