using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub;
using XTI_Processes;

namespace InstallApp;

internal sealed class InstallHostedService : IHostedService
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
            var credentials = await addInstallationUser(sp, appKey);
            var appVersion = await retrieveVersion(sp, appKey);
            var options = sp.GetRequiredService<IOptions<InstallOptions>>().Value;
            var installMachineName =
                string.IsNullOrWhiteSpace(options.DestinationMachine)
                    ? Environment.MachineName
                    : options.DestinationMachine;
            await newInstallation
            (
                sp,
                appKey,
                installMachineName
            );
            var hostEnv = sp.GetRequiredService<IHostEnvironment>();
            if (string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                await runLocalInstall(sp, appVersion.VersionKey, credentials, installMachineName);
            }
            else
            {
                var release = $"v{appVersion.Major}.{appVersion.Minor}.{appVersion.Patch}";
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
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
                        KeyValuePair.Create("installationUserName", credentials.UserName),
                        KeyValuePair.Create("installationPassword", credentials.Password),
                        KeyValuePair.Create("release", release),
                        KeyValuePair.Create("machineName", installMachineName)
                    }
                );
                var installServiceUrl = $"http://{options.DestinationMachine}:61862";
                Console.WriteLine($"Posting to '{installServiceUrl}' {appKey.Name.Value} {appKey.Type.DisplayText} {appVersion.VersionKey} {credentials.UserName} {credentials.Password} {release}");
                using var response = await client.PostAsync(installServiceUrl, content);
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
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    private static async Task<AppVersionModel> retrieveVersion(IServiceProvider sp, AppKey appKey)
    {
        var appFactory = sp.GetRequiredService<AppFactory>();
        var app = await appFactory.Apps.App(appKey);
        var version = await app.CurrentVersion();
        return version.ToModel();
    }

    private static async Task<CredentialValue> addInstallationUser(IServiceProvider sp, AppKey appKey)
    {
        Console.WriteLine("Adding installation user");
        var machineName = getMachineName(sp);
        var dashIndex = machineName.IndexOf(".");
        if (dashIndex > -1)
        {
            machineName = machineName.Substring(0, dashIndex);
        }
        var appFactory = sp.GetRequiredService<AppFactory>();
        var clock = sp.GetRequiredService<IClock>();
        var hashedPasswordFactory = sp.GetRequiredService<IHashedPasswordFactory>();
        var password = $"{Guid.NewGuid():N}?!";
        var hashedPassword = hashedPasswordFactory.Create(password);
        var systemUser = await appFactory.InstallationUsers.AddOrUpdateInstallationUser(machineName, hashedPassword, clock.Now());
        var credentials = new CredentialValue(systemUser.UserName().Value, password);
        Console.WriteLine($"Added installation user '{systemUser.UserName()}'");
        return credentials;
    }

    private static Task newInstallation(IServiceProvider sp, AppKey appKey, string machineName)
    {
        Console.WriteLine("New installation");
        var installationProcess = sp.GetRequiredService<InstallationProcess>();
        var clock = sp.GetRequiredService<IClock>();
        var hostEnv = sp.GetRequiredService<IHostEnvironment>();
        return installationProcess.NewInstallation(appKey, machineName, hostEnv.IsProduction(), clock.Now());
    }

    private static string getMachineName(IServiceProvider sp)
    {
        var options = sp.GetRequiredService<IOptions<InstallOptions>>().Value;
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

    private static async Task runLocalInstall(IServiceProvider sp, string versionKey, CredentialValue credential, string machineName)
    {
        var hostEnv = sp.GetRequiredService<IHostEnvironment>();
        var options = sp.GetRequiredService<IOptions<InstallOptions>>().Value;
        var installProcessPath = Path.Combine
        (
            new XtiFolder(hostEnv).ToolsPath(),
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
                    SystemPassword = credential.Password,
                    MachineName = machineName,
                    Domain = options.Domain
                }
            );
        Console.WriteLine($"Running Local Install\r\n{installProcess.CommandText()}");
        var result = await installProcess
            .WriteOutputToConsole()
            .Run();
        result.EnsureExitCodeIsZero();
    }

    private static AppKey ensureAppKeyIsValid(IServiceProvider sp)
    {
        var options = sp.GetRequiredService<IOptions<InstallOptions>>().Value;
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

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}