using HubWebApp.Api;
using HubWebApp.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Fakes;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests
{
    public static class Extensions
    {
        public static Task Setup(this IServiceProvider services)
        {
            var hubSetup = services.GetService<HubSetup>();
            return hubSetup.Run();
        }

        public static Task<App> HubApp(this IServiceProvider services)
        {
            var factory = services.GetService<AppFactory>();
            return factory.Apps().App(HubAppKey.Key);
        }

        public static async Task<AppUser> AddAdminUser(this IServiceProvider services)
        {
            var factory = services.GetService<AppFactory>();
            var hubApp = await services.HubApp();
            var adminUser = await factory.Users().Add(new AppUserName("hubadmin"), new FakeHashedPassword("Password12345"), DateTime.UtcNow);
            var adminRole = await hubApp.Role(HubRoles.Instance.Admin);
            await adminUser.AddRole(adminRole);
            return adminUser;
        }

        public static void LoginAs(this IServiceProvider services, AppUser user)
        {
            var httpContextAccessor = services.GetService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext == null)
            {
                httpContextAccessor.HttpContext = new DefaultHttpContext()
                {
                    RequestServices = services
                };
            }
            httpContextAccessor.HttpContext.User = new FakeHttpUser().Create(new GeneratedKey().Value(), user);
        }

        public static void RequestPage(this IServiceProvider services, XtiPath path)
        {
            var httpContextAccessor = services.GetService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext == null)
            {
                httpContextAccessor.HttpContext = new DefaultHttpContext()
                {
                    RequestServices = services
                };
            }
            httpContextAccessor.HttpContext.Request.PathBase = $"/{path.App.DisplayText}/{path.Version.DisplayText}";
            httpContextAccessor.HttpContext.Request.Path = $"/{path.Group.DisplayText}/{path.Action.DisplayText}/{path.Modifier.DisplayText}";
        }
    }
}
