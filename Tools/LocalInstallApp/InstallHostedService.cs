using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Web.Administration;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_Processes;
using XTI_SecretsToolApi;

namespace LocalInstallApp
{
    public sealed class InstallHostedService : IHostedService
    {
        private readonly IServiceProvider services;

        private static readonly string appOfflineFileName = "app_offline.htm";

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
                initLog();
                var options = sp.GetService<IOptions<InstallOptions>>().Value;
                await writeLog($"Starting install {options.AppName} {options.AppType} {options.VersionKey} {options.Release}");
                var appKey = ensureAppKeyIsValid(sp);
                var hostEnv = sp.GetService<IHostEnvironment>();
                var tempDir = Path.Combine
                (
                    Path.GetTempPath(),
                    $"xti_{appKey.Name.Value}_{appKey.Type.DisplayText.Replace(" ", "")}"
                );
                try
                {
                    var versionKey = AppVersionKey.Current;
                    if (hostEnv.IsProduction() && !string.IsNullOrWhiteSpace(options.VersionKey))
                    {
                        versionKey = AppVersionKey.Parse(options.VersionKey);
                    }
                    if (hostEnv.IsDevelopment() || hostEnv.IsEnvironment("Test"))
                    {
                        await runSetup(sp, appKey, versionKey);
                    }
                    else
                    {
                        await writeLog("Downloading Assets");
                        await downloadAssets(sp, tempDir);
                    }
                    await storeSystemUserCredentials(sp, appKey);
                    if (appKey.Type.Equals(AppType.Values.WebApp))
                    {
                        if (hostEnv.IsProduction())
                        {
                            await installWebApp(sp, appKey, versionKey, versionKey, tempDir);
                        }
                        await installWebApp(sp, appKey, versionKey, AppVersionKey.Current, tempDir);
                    }
                    else if (appKey.Type.Equals(AppType.Values.Service))
                    {
                        if (hostEnv.IsProduction())
                        {
                            await installServiceApp(sp, appKey, versionKey, versionKey, tempDir);
                        }
                        await installServiceApp(sp, appKey, versionKey, AppVersionKey.Current, tempDir);
                    }
                }
                finally
                {
                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }
                }
                await writeLog("Installation Complete");
            }
            catch (Exception ex)
            {
                await writeLog($"Error: {ex}");
                Environment.ExitCode = 999;
            }
            var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
            lifetime.StopApplication();
        }

        private static void initLog()
        {
            var path = getLogPath();
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static async Task writeLog(string message)
        {
            Console.WriteLine(message);
            var path = getLogPath();
            using var writer = new StreamWriter(path, true);
            await writer.WriteLineAsync($"{DateTime.Now:M/dd/yy HH:mm:ss}\r\n{message}");
        }

        private static string getLogPath()
            => Path.Combine(Path.GetTempPath(), "xti_install_log.txt");

        private static async Task runSetup(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
        {
            var xtiFolder = sp.GetService<XtiFolder>();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var sourceDir = xtiFolder.PublishPath(appKey, versionKey);
            var setupAppDir = Path.Combine(sourceDir, "Setup");
            await writeLog($"Running Setup '{setupAppDir}'");
            await new DotnetRunProcess(setupAppDir)
                .UseEnvironment(hostEnv.EnvironmentName)
                .AddConfigOptions
                (
                    new
                    {
                        VersionKey = versionKey,
                        VersionsPath = Path.Combine(sourceDir, "versions.json")
                    },
                    "Setup"
                )
                .Run();
        }

        private static async Task downloadAssets(IServiceProvider sp, string tempDir)
        {
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            var gitFactory = sp.GetService<GitFactory>();
            var gitHubRepo = await gitFactory.CreateGitHubRepo(options.RepoOwner, options.RepoName);
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
            Directory.CreateDirectory(tempDir);
            var release = await gitHubRepo.Release(options.Release);
            var setupAsset = release?.Assets.FirstOrDefault(a => a.Name.Equals("setup.zip", StringComparison.OrdinalIgnoreCase));
            if (setupAsset != null)
            {
                var versionsAsset = release?.Assets.FirstOrDefault(a => a.Name.Equals("versions.json", StringComparison.OrdinalIgnoreCase));
                if (versionsAsset != null)
                {
                    await writeLog($"Downloading versions.json {release.TagName} {setupAsset.Name}");
                    var versionsContent = await gitHubRepo.DownloadReleaseAsset(versionsAsset);
                    var versionsPath = Path.Combine(tempDir, "versions.json");
                    await File.WriteAllBytesAsync(versionsPath, versionsContent);
                }
                await writeLog($"Downloading Setup {release.TagName} {setupAsset.Name}");
                var setupContent = await gitHubRepo.DownloadReleaseAsset(setupAsset);
                var setupZipPath = Path.Combine(tempDir, "setup.zip");
                await File.WriteAllBytesAsync(setupZipPath, setupContent);
                var setupAppDir = Path.Combine(tempDir, "Setup");
                ZipFile.ExtractToDirectory(setupZipPath, setupAppDir);
                var hostEnv = sp.GetService<IHostEnvironment>();
                await writeLog($"Running Setup '{setupAppDir}'");
                var setupResult = await new XtiProcess(Path.Combine(setupAppDir, $"{options.AppName}SetupApp.exe"))
                    .UseEnvironment(hostEnv.EnvironmentName)
                    .WriteOutputToConsole()
                    .AddConfigOptions
                    (
                        new
                        {
                            VersionKey = options.VersionKey,
                            VersionsPath = Path.Combine(tempDir, "versions.json")
                        },
                        "Setup"
                    )
                    .Run();
                setupResult.EnsureExitCodeIsZero();
            }
            var appAsset = release?.Assets.FirstOrDefault(a => a.Name.Equals("app.zip", StringComparison.OrdinalIgnoreCase));
            if (appAsset != null)
            {
                await writeLog($"Downloading App {release.TagName} {setupAsset.Name}");
                var appContent = await gitHubRepo.DownloadReleaseAsset(appAsset);
                var appZipPath = Path.Combine(tempDir, "setup.zip");
                await File.WriteAllBytesAsync(appZipPath, appContent);
                var setupAppDir = Path.Combine(tempDir, "App");
                ZipFile.ExtractToDirectory(appZipPath, setupAppDir);
            }
        }

        private async Task storeSystemUserCredentials(IServiceProvider sp, AppKey appKey)
        {
            var credentialKey = getCredentialKey(appKey);
            var options = sp.GetService<IOptions<InstallOptions>>().Value;
            var xtiFolder = sp.GetService<XtiFolder>();
            var path = getSecretsToolPath(xtiFolder);
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
            await writeLog(process.CommandText());
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

        private async Task installWebApp(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey, AppVersionKey installVersionKey, string tempDir)
        {
            var installationService = createInstallationService(sp, appKey);
            int installationID;
            if (installVersionKey.Equals(AppVersionKey.Current))
            {
                installationID = await installationService.BeginCurrentInstall(installVersionKey);
            }
            else
            {
                installationID = await installationService.BeginVersionInstall();
            }
            await writeLog($"Preparing IIS for {versionKey.DisplayText}");
            await prepareIis(sp, appKey, installVersionKey);
            await deleteExistingWebFiles(sp, appKey, installVersionKey);
            await copyToInstallDir(sp, appKey, versionKey, installVersionKey, tempDir, false);
            var appOfflinePath = getAppOfflinePath(sp, appKey, installVersionKey);
            File.Delete(appOfflinePath);
            await installationService.Installed(installationID);
        }

        private InstallationService createInstallationService(IServiceProvider sp, AppKey appKey)
        {
            InstallationService installationService;
            var installationServiceFactory = sp.GetService<InstallationServiceFactory>();
            if (appKey.Equals(HubInfo.AppKey))
            {
                installationService = installationServiceFactory.CreateHubApiInstallationService();
            }
            else
            {
                installationService = installationServiceFactory.CreateHubClientInstallationService();
            }
            return installationService;
        }

        private static string getAppOfflinePath(IServiceProvider sp, AppKey appKey, AppVersionKey installVersionKey)
        {
            var xtiFolder = sp.GetService<XtiFolder>();
            var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
            var appOfflinePath = Path.Combine(installDir, appOfflineFileName);
            return appOfflinePath;
        }

        private static async Task deleteExistingWebFiles(IServiceProvider sp, AppKey appKey, AppVersionKey installVersionKey)
        {
            var xtiFolder = sp.GetService<XtiFolder>();
            var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
            await writeLog($"Deleting files in '{installDir}'");
            foreach (var file in Directory.GetFiles(installDir).Where(f => !Path.GetFileName(f).Equals(appOfflineFileName, StringComparison.OrdinalIgnoreCase)))
            {
                File.Delete(file);
            }
            foreach (var directory in Directory.GetDirectories(installDir))
            {
                Directory.Delete(directory, true);
            }
        }

        private static async Task prepareIis(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
        {
            var secretCredentialsValue = await retrieveCredentials(sp, "WebApp");
            using var server = new ServerManager();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var siteName = hostEnv.IsProduction() ? "WebApps" : hostEnv.EnvironmentName;
            var site = server.Sites[siteName];
            var appName = getAppName(appKey);
            var appPoolName = $"Xti_{hostEnv.EnvironmentName}_{appName}_{versionKey.DisplayText}";
            var appPool = server.ApplicationPools.FirstOrDefault(ap => ap.Name.Equals(appPoolName, StringComparison.OrdinalIgnoreCase));
            if (appPool == null)
            {
                await writeLog($"Adding application pool '{appPoolName}'");
                var newAppPool = server.ApplicationPools.Add(appPoolName);
                newAppPool.ProcessModel.UserName = secretCredentialsValue.UserName;
                newAppPool.ProcessModel.Password = secretCredentialsValue.Password;
                newAppPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
                newAppPool.ManagedRuntimeVersion = "v4.0";
            }
            var baseApp = site.Applications.FirstOrDefault(a => a.Path == "/");
            var virtDirPath = $"/{appName}";
            var virtDir = baseApp.VirtualDirectories.FirstOrDefault(vd => vd.Path.Equals(virtDirPath, StringComparison.OrdinalIgnoreCase));
            var xtiFolder = sp.GetService<XtiFolder>();
            var virtDirPhysPath = xtiFolder.InstallPath(appKey);
            if (virtDir == null)
            {
                await writeLog($"Adding virtual directory '{virtDirPath}'");
                baseApp.VirtualDirectories.Add(virtDirPath, virtDirPhysPath);
            }
            var appPhysPath = Path.Combine(virtDirPhysPath, versionKey.DisplayText);
            var appPath = $"/{appName}/{versionKey.DisplayText}";
            var iisApp = site.Applications.FirstOrDefault(a => a.Path.Equals(appPath, StringComparison.OrdinalIgnoreCase));
            if (iisApp == null)
            {
                await writeLog($"Adding application '{appPath}'");
                iisApp = site.Applications.Add(appPath, appPhysPath);
                iisApp.ApplicationPoolName = appPoolName;
            }
            server.CommitChanges();
            if (!Directory.Exists(appPhysPath))
            {
                Directory.CreateDirectory(appPhysPath);
            }
            await writeAppOffline(sp, appKey, versionKey);
            await Task.Delay(5000);
        }

        private static async Task writeAppOffline(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey)
        {
            var offlinePath = getAppOfflinePath(sp, appKey, versionKey);
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
        }

        private static async Task<CredentialValue> retrieveCredentials(IServiceProvider sp, string credentialKey)
        {
            var xtiFolder = sp.GetService<XtiFolder>();
            var path = getSecretsToolPath(xtiFolder);
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

        private static string getSecretsToolPath(XtiFolder xtiFolder)
            => Path.Combine
            (
                xtiFolder.ToolsPath(),
                "Xti_SecretsTool",
                "Xti_SecretsTool.exe"
            );

        private static async Task installServiceApp(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey, AppVersionKey installVersionKey, string tempDir)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var hostEnv = sp.GetService<IHostEnvironment>();
            ServiceController sc = null;
            if (installVersionKey.Equals(AppVersionKey.Current))
            {
                var xtiFolder = sp.GetService<XtiFolder>();
                var appName = getAppName(appKey);
                var serviceName = $"Xti_{hostEnv.EnvironmentName}_{appName}";
                sc = getService(serviceName);
                if (sc == null)
                {
                    var binPath = Path.Combine
                    (
                        xtiFolder.InstallPath(appKey, AppVersionKey.Current),
                        $"{appName}ServiceApp.exe"
                    );
                    binPath = $"{binPath} --Environment {hostEnv.EnvironmentName}";
                    await writeLog($"Creating service '{binPath}'");
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
                    await writeLog($"Stopping services '{sc.DisplayName}'");
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
            await copyToInstallDir(sp, appKey, versionKey, installVersionKey, tempDir, true);
            if (sc != null)
            {
                await writeLog($"Starting services '{sc.DisplayName}'");
                sc.Start();
            }
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

        private static async Task copyToInstallDir(IServiceProvider sp, AppKey appKey, AppVersionKey versionKey, AppVersionKey installVersionKey, string tempDir, bool purge)
        {
            var xtiFolder = sp.GetService<XtiFolder>();
            var hostEnv = sp.GetService<IHostEnvironment>();
            var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
            string sourceDir;
            if (hostEnv.IsDevelopment() || hostEnv.IsEnvironment("Test"))
            {
                sourceDir = getSourceAppDir(xtiFolder, appKey, versionKey);
            }
            else
            {
                sourceDir = Path.Combine(tempDir, "App");
            }
            await writeLog($"Copying from '{tempDir}' to '{installDir}'");
            var process = new RobocopyProcess(sourceDir, installDir)
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
        }

        private static string getSourceAppDir(XtiFolder xtiFolder, AppKey appKey, AppVersionKey versionKey)
        {
            return Path.Combine
            (
                xtiFolder.PublishPath(appKey, versionKey),
                "App"
            );
        }

        private static string getAppName(AppKey appKey)
            => appKey.Name.DisplayText.Replace(" ", "");

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
