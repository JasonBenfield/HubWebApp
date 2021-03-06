﻿using HubWebApp.Api;
using HubWebApp.ApiControllers;
using HubWebApp.Apps;
using HubWebApp.Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Extensions;

namespace HubWebApp.Extensions
{
    public static class HubWebAppExtensions
    {
        public static void AddServicesForHub(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebAppServices(configuration);
            services.AddScoped<AppFromPath>();
            services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
            services.AddSingleton(_ => HubInfo.AppKey);
            services.AddScoped<AppApiFactory, HubAppApiFactory>();
            services.AddScoped(sp => (HubAppApi)sp.GetService<IAppApi>());
            services.AddScoped<HubSetup>();
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SetDefaultJsonOptions();
                })
                .AddMvcOptions(options =>
                {
                    options.SetDefaultMvcOptions();
                });
            services.AddControllersWithViews()
                .PartManager.ApplicationParts.Add
                (
                    new AssemblyPart(typeof(UserAdminController).Assembly)
                );
        }
    }
}
