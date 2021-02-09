using HubWebAppApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests
{
    public static class HubActionTester
    {
        public static HubActionTester<TModel, TResult> Create<TModel, TResult>(IServiceProvider services, Func<HubAppApi, AppApiAction<TModel, TResult>> getAction)
        {
            return new HubActionTester<TModel, TResult>(services, getAction);
        }
    }

    public interface IHubActionTester
    {
        IServiceProvider Services { get; }
        Task<AppUser> AdminUser();
        HubActionTester<TOtherModel, TOtherResult> Create<TOtherModel, TOtherResult>(Func<HubAppApi, AppApiAction<TOtherModel, TOtherResult>> getAction);
        Task<App> HubApp();
        Task<Modifier> HubAppModifier();
    }

    public sealed class HubActionTester<TModel, TResult> : IHubActionTester
    {
        private readonly Func<HubAppApi, AppApiAction<TModel, TResult>> getAction;

        public HubActionTester
        (
            IServiceProvider services,
            Func<HubAppApi, AppApiAction<TModel, TResult>> getAction
        )
        {
            Services = services;
            this.getAction = getAction;
        }

        public HubActionTester<TOtherModel, TOtherResult> Create<TOtherModel, TOtherResult>
        (
            Func<HubAppApi, AppApiAction<TOtherModel, TOtherResult>> getAction
        )
        {
            return HubActionTester.Create(Services, getAction);
        }

        public IServiceProvider Services { get; }

        public async Task AddRole(AppUser user, AppRoleName roleName)
        {
            var app = await HubApp();
            var role = await app.Role(roleName);
            await user.AddRole(role);
        }

        public Task<App> HubApp()
        {
            var factory = Services.GetService<AppFactory>();
            return factory.Apps().App(HubInfo.AppKey);
        }

        public Task<AppUser> AdminUser()
        {
            var factory = Services.GetService<AppFactory>();
            return factory.Users().User(new AppUserName("hubadmin"));
        }

        public async Task<Modifier> HubAppModifier()
        {
            var hubApp = await HubApp();
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var hubAppModifier = await appsModCategory.Modifier(hubApp.ID.Value);
            return hubAppModifier;
        }

        public async Task<TResult> Execute(TModel model, AppUser user, ModifierKey modKey = null)
        {
            if (modKey == null)
            {
                modKey = ModifierKey.Default;
            }
            var httpContextAccessor = Services.GetService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext == null)
            {
                httpContextAccessor.HttpContext = new DefaultHttpContext
                {
                    RequestServices = Services
                };
            }
            httpContextAccessor.HttpContext.User = new FakeHttpUser().Create("Session-Key", user);
            var appApiFactory = Services.GetService<AppApiFactory>();
            var hubApiForSuperUser = (HubAppApi)appApiFactory.CreateForSuperUser();
            var actionForSuperUser = getAction(hubApiForSuperUser);
            httpContextAccessor.HttpContext.Request.PathBase = $"/{actionForSuperUser.Path.App.DisplayText}/{actionForSuperUser.Path.Version.DisplayText}".Replace(" ", "");
            var modKeyPath = modKey.Equals(ModifierKey.Default) ? "" : $"/{modKey.Value}";
            httpContextAccessor.HttpContext.Request.Path = $"/{actionForSuperUser.Path.Group.DisplayText}/{actionForSuperUser.Path.Action.DisplayText}{modKeyPath}".Replace(" ", "");
            var appContext = Services.GetService<IAppContext>();
            var userContext = new FakeUserContext();
            userContext.SetUser(user);
            var path = actionForSuperUser.Path.WithModifier(modKey ?? ModifierKey.Default);
            var apiUser = new XtiAppApiUser(appContext, userContext, path);
            var hubApi = (HubAppApi)appApiFactory.Create(apiUser);
            var action = getAction(hubApi);
            var result = await action.Execute(model);
            return result.Data;
        }

    }
}
