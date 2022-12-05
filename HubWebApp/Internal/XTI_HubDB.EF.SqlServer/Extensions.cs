using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_DB;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace XTI_HubDB.Extensions;

public static class Extensions
{
    public static void AddHubDbContextForSqlServer(this IServiceCollection services)
    {
        services.AddConfigurationOptions<DbOptions>(DbOptions.DB);
        services.AddDbContext<HubDbContext>((sp, options) =>
        {
            var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
            var dbOptions = sp.GetRequiredService<DbOptions>();
            var connectionString = new XtiConnectionString
            (
                dbOptions, 
                new XtiDbName(xtiEnv.EnvironmentName, "Hub")
            );
            options.UseSqlServer
            (
                connectionString.Value(),
                b => b.MigrationsAssembly("XTI_HubDB.EF.SqlServer")
            );
            if (xtiEnv.IsDevelopmentOrTest())
            {
                options.EnableSensitiveDataLogging();
            }
            else
            {
                options.EnableSensitiveDataLogging(false);
            }
        });
        services.AddScoped<IHubDbContext>(sp => sp.GetRequiredService<HubDbContext>());
    }
}