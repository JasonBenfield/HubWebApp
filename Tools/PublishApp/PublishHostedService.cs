using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_PublishTool;
using XTI_Secrets;

namespace PublishApp;

internal sealed class PublishHostedService : IHostedService
{
    private readonly IServiceProvider services;

    public PublishHostedService(IServiceProvider services)
    {
        this.services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = services.CreateScope();
        var sp = scope.ServiceProvider;
        try
        {
            var hostEnv = sp.GetRequiredService<IHostEnvironment>();
            var appFactory = sp.GetRequiredService<AppFactory>();
            var gitFactory = sp.GetRequiredService<GitFactory>();
            var credentialsFactory = sp.GetRequiredService<ISecretCredentialsFactory>();
            var options = sp.GetRequiredService<IOptions<PublishOptions>>().Value;
            var publishProcess = new PublishProcess
            (
                hostEnv, 
                appFactory, 
                gitFactory, 
                credentialsFactory,
                options.RepoOwner
            );
            var appKey = new AppKey(new AppName(options.AppName), AppType.Values.Value(options.AppType));
            await publishProcess.Run(appKey, options.AppsToImport, options.RepoOwner, options.RepoName);
            if (!appKey.Type.Equals(AppType.Values.Package) && !options.NoInstall)
            {
                await publishProcess.RunInstall(appKey, options.DestinationMachine);
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

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}