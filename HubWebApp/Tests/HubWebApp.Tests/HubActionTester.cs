﻿using Microsoft.AspNetCore.Http;

namespace HubWebApp.Tests;

internal static class HubActionTester
{
    public static HubActionTester<TModel, TResult> Create<TModel, TResult>(IServiceProvider services, Func<HubAppApi, AppApiAction<TModel, TResult>> getAction)
    {
        return new HubActionTester<TModel, TResult>(services, getAction);
    }
}

internal interface IHubActionTester
{
    IServiceProvider Services { get; }
    HubActionTester<TOtherModel, TOtherResult> Create<TOtherModel, TOtherResult>(Func<HubAppApi, AppApiAction<TOtherModel, TOtherResult>> getAction);
    Task<App> HubApp();
    Task<Modifier> HubAppModifier();
}

internal sealed class HubActionTester<TModel, TResult> : IHubActionTester
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

    public void Logout()
    {
        var currentUserName = Services.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(AppUserName.Anon);
    }

    public Task<AppUser> LoginAsAdmin()
    {
        var factory = Services.GetRequiredService<HubFactory>();
        return factory.Users.UserByUserName(new AppUserName("hubadmin"));
    }

    public Task<AppUser> Login(params AppRoleName[]? roleNames) =>
        Login(ModifierKey.Default, roleNames);

    public async Task<AppUser> Login(ModifierKey modKey, params AppRoleName[]? roleNames)
    {
        var factory = Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.AddOrUpdate
        (
            new AppUserName("loggedinUser"),
            new FakeHashedPassword(""),
            new PersonName("loggedin User"),
            new EmailAddress(""),
            DateTimeOffset.Now
        );
        var currentUserName = Services.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(user.ToModel().UserName);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        Modifier modifier;
        if (modKey.Equals(ModifierKey.Default))
        {
            modifier = await hubApp.DefaultModifier();
        }
        else
        {
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            modifier = await appsModCategory.ModifierByModKey(modKey);
        }
        foreach (var roleName in roleNames ?? new AppRoleName[0])
        {
            var role = await hubApp.Role(roleName);
            await user.Modifier(modifier).AssignRole(role);
        }
        return user;
    }

    public Task<App> HubApp()
    {
        var factory = Services.GetRequiredService<HubFactory>();
        return factory.Apps.App(HubInfo.AppKey);
    }

    public async Task<AppRole> AdminRole()
    {
        var app = await HubApp();
        var role = await app.Role(AppRoleName.Admin);
        return role;
    }

    public async Task<Modifier> DefaultModifier()
    {
        var hubApp = await HubApp();
        var defaultModifier = await hubApp.DefaultModifier();
        return defaultModifier;
    }

    public async Task<Modifier> HubAppModifier()
    {
        var hubApp = await HubApp();
        var hubAppModel = hubApp.ToAppModel();
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var hubAppModifier = await appsModCategory.AddOrUpdateModifier
        (
            hubAppModel.PublicKey,
            hubAppModel.ID.ToString(),
            hubAppModel.AppKey.Format()
        );
        return hubAppModifier;
    }

    public Task<TResult> Execute(TModel model) =>
        Execute(model, ModifierKey.Default);

    public Task<TResult> Execute(TModel model, Modifier modifier) =>
        Execute(model, modifier.ToModel().ModKey);

    public async Task<TResult> Execute(TModel model, ModifierKey modKey)
    {
        var appContext = Services.GetRequiredService<IAppContext>();
        var httpContextAccessor = Services.GetRequiredService<IHttpContextAccessor>();
        if (httpContextAccessor.HttpContext == null)
        {
            httpContextAccessor.HttpContext = new DefaultHttpContext
            {
                RequestServices = Services
            };
        }
        var appApiFactory = Services.GetRequiredService<AppApiFactory>();
        var hubApiForSuperUser = (HubAppApi)appApiFactory.CreateForSuperUser();
        var actionForSuperUser = getAction(hubApiForSuperUser);
        httpContextAccessor.HttpContext.Request.PathBase = $"/{actionForSuperUser.Path.App}/{actionForSuperUser.Path.Version.DisplayText}".Replace(" ", "");
        var modKeyPath = modKey.Equals(ModifierKey.Default) ? "" : $"/{modKey.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{actionForSuperUser.Path.Group.DisplayText}/{actionForSuperUser.Path.Action.DisplayText}{modKeyPath}".Replace(" ", "");
        var appKey = Services.GetRequiredService<AppKey>();
        var userContext = Services.GetRequiredService<ISourceUserContext>();
        var pathAccessor = Services.GetRequiredService<FakeXtiPathAccessor>();
        var path = actionForSuperUser.Path.WithModifier(modKey ?? ModifierKey.Default);
        pathAccessor.SetPath(path);
        httpContextAccessor.HttpContext.Request.PathBase = $"/{path.App}/{path.Version.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{path.Group.Value}/{path.Action.Value}/";
        if (!path.Modifier.Equals(ModifierKey.Default))
        {
            httpContextAccessor.HttpContext.Request.Path += path.Modifier.Value;
        }
        var currentUserName = Services.GetRequiredService<ICurrentUserName>();
        var apiUser = new AppApiUser(userContext, appContext, currentUserName, pathAccessor);
        var hubApi = (HubAppApi)appApiFactory.Create(apiUser);
        var action = getAction(hubApi);
        var result = await action.Invoke(model);
        return result;
    }
}