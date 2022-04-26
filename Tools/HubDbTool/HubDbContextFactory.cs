using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_DB;
using XTI_HubDB.EF;
using XTI_HubDB.Extensions;

namespace HubDbTool;

internal sealed class HubDbContextFactory : IDesignTimeDbContextFactory<HubDbContext>
{
    public HubDbContext CreateDbContext(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.UseXtiConfiguration(hostingContext.HostingEnvironment, "", "", args);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(_ => new XtiEnvironment(hostContext.HostingEnvironment.EnvironmentName));
                services.AddConfigurationOptions<DbOptions>(DbOptions.DB);
                services.Configure<HubDbToolOptions>(hostContext.Configuration);
                services.AddHubDbContextForSqlServer();
            })
            .Build();
        var scope = host.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<HubDbContext>();
    }
}