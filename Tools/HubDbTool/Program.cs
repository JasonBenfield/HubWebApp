using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_Configuration.Extensions;
using XTI_DB;
using XTI_HubDB.EF;
using XTI_HubDB.Extensions;

namespace HubDbTool
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<MainDbToolOptions>(hostContext.Configuration);
                    services.Configure<DbOptions>(hostContext.Configuration.GetSection(DbOptions.DB));
                    services.AddHubDbContextForSqlServer(hostContext.Configuration);
                    services.AddScoped<HubDbReset>();
                    services.AddScoped<HubDbBackup>();
                    services.AddScoped<HubDbRestore>();
                    services.AddHostedService<HostedService>();
                })
                .RunConsoleAsync();
        }
    }
}
