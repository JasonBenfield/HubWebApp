using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Configuration.Extensions;
using XTI_HubDB.EF;
using XTI_HubDB.Extensions;

namespace HubDbTool
{
    public sealed class MainDbContextFactory : IDesignTimeDbContextFactory<HubDbContext>
    {
        public HubDbContext CreateDbContext(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<MainDbToolOptions>(hostContext.Configuration);
                    services.AddHubDbContextForSqlServer(hostContext.Configuration);
                })
                .Build();
            var scope = host.Services.CreateScope();
            return scope.ServiceProvider.GetService<HubDbContext>();
        }
    }
}
