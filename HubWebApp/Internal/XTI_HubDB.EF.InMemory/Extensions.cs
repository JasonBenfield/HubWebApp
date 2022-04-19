using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace XTI_HubDB.Extensions;

public static class Extensions
{
    public static void AddHubDbContextForInMemory(this IServiceCollection services)
    {
        services.AddDbContext<HubDbContext>(options =>
        {
            options
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging();
        });
        services.AddScoped<IHubDbContext>(sp => sp.GetRequiredService<HubDbContext>());
    }
}