using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XTI_HubDB.Entities;

namespace XTI_HubDB.Extensions;

public static class Extensions
{
    public static void AddHubDbContextForInMemory(this IServiceCollection services)
    {
        services.AddDbContextFactory<HubDbContext>(options =>
        {
            options
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging();
        });
    }
}