using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_Core.Extensions;
using XTI_DB;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace XTI_HubDB.Extensions
{
    public static class Extensions
    {
        public static void AddHubDbContextForSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptions>(configuration.GetSection(DbOptions.DB));
            services.AddDbContext<HubDbContext>((sp, options) =>
            {
                var appDbOptions = sp.GetService<IOptions<DbOptions>>().Value;
                var hostEnvironment = sp.GetService<IHostEnvironment>();
                var connectionString = new HubConnectionString(appDbOptions, hostEnvironment.EnvironmentName).Value();
                options
                    .UseSqlServer(connectionString, b => b.MigrationsAssembly("XTI_HubDB.EF.SqlServer"));
                if (hostEnvironment?.IsDevOrTest() == true)
                {
                    options.EnableSensitiveDataLogging();
                }
                else
                {
                    options.EnableSensitiveDataLogging(false);
                }
            });
            services.AddScoped<IHubDbContext>(sp => sp.GetService<HubDbContext>());
        }
    }
}
