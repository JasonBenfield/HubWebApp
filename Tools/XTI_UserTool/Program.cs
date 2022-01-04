using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Configuration.Extensions;
using XTI_Hub;
using XTI_Secrets.Extensions;
using XTI_Tool.Extensions;
using XTI_UserApp;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHubToolServices(hostContext.Configuration);
        services.AddFileSecretCredentials(hostContext.HostingEnvironment);
        services.AddSingleton<InstallationUserCredentials>();
        services.AddSingleton<SystemUserCredentials>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        services.Configure<UserOptions>(hostContext.Configuration);
        services.AddHostedService<HostedService>();
    })
    .RunConsoleAsync();