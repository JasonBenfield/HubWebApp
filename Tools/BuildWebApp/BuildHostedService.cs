using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;
using XTI_PublishTool;
using XTI_VersionToolApi;

namespace BuildWebApp;

internal sealed class BuildHostedService : IHostedService
{
    private readonly IServiceProvider services;

    public BuildHostedService(IServiceProvider services)
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
            var hostEnv = sp.GetRequiredService<IHostEnvironment>();
            var options = sp.GetRequiredService<IOptions<BuildOptions>>().Value;
            var versionKey = options.VersionKey;
            if (string.IsNullOrWhiteSpace(versionKey))
            {
                if (hostEnv.IsProduction())
                {
                    var version = await retrieveVersion(appKey);
                    versionKey = version.VersionKey;
                }
                else
                {
                    var currentVersion = await retrieveCurrentVersion(appKey);
                    versionKey = currentVersion.VersionKey;
                }
            }
            var builder = new BuildWebProcess
            (
                appKey, 
                AppVersionKey.Parse(versionKey)
            );
            await builder.Build();
            await runDotnetBuild();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.ExitCode = 999;
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    private static AppKey ensureAppKeyIsValid(IServiceProvider sp)
    {
        var options = sp.GetRequiredService<IOptions<BuildOptions>>().Value;
        if (string.IsNullOrWhiteSpace(options.AppName))
        {
            throw new ArgumentException("App Name is Required");
        }
        var appName = new AppName(options.AppName);
        return new AppKey(appName, AppType.Values.WebApp);
    }

    private async Task<VersionToolOutput> retrieveVersion(AppKey appKey)
    {
        var versionOptions = new VersionToolOptions();
        versionOptions.CommandGetVersion();
        versionOptions.AppName = appKey.Name.DisplayText;
        versionOptions.AppType = appKey.Type.DisplayText;
        var versionResult = await runVersionTool(versionOptions);
        var currentVersion = versionResult.Data<VersionToolOutput>();
        return currentVersion;
    }

    private async Task<VersionToolOutput> retrieveCurrentVersion(AppKey appKey)
    {
        var versionOptions = new VersionToolOptions();
        versionOptions.CommandGetCurrentVersion(appKey.Name.Value, appKey.Type.DisplayText);
        var versionResult = await runVersionTool(versionOptions);
        var currentVersion = versionResult.Data<VersionToolOutput>();
        return currentVersion;
    }

    private async Task<WinProcessResult> runVersionTool(VersionToolOptions versionOptions)
    {
        var xtiFolder = services.GetRequiredService<XtiFolder>();
        var versionToolProcess = new XtiProcess
        (
            Path.Combine
            (
                xtiFolder.ToolsPath(),
                "XTI_VersionTool",
                "XTI_VersionTool.exe"
            )
        );
        versionToolProcess.UseProductionEnvironment();
        versionToolProcess.AddConfigOptions(versionOptions);
        var result = await versionToolProcess.Run();
        result.EnsureExitCodeIsZero();
        return result;
    }

    private static async Task runDotnetBuild()
    {
        var result = await new WinProcess("dotnet")
              .WriteOutputToConsole()
              .UseArgumentNameDelimiter("")
              .AddArgument("build")
              .Run();
        result.EnsureExitCodeIsZero();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}