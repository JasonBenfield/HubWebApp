﻿using Microsoft.AspNetCore.Http;

namespace HubWebApp.Tests;

internal static class HubActionTester
{
    public static HubActionTester<TRequest, TResult> Create<TRequest, TResult>(IServiceProvider services, Func<HubAppApi, AppApiAction<TRequest, TResult>> getAction)
    {
        return new HubActionTester<TRequest, TResult>(services, getAction);
    }
}

internal interface IHubActionTester
{
    IServiceProvider Services { get; }
    HubActionTester<TOtherModel, TOtherResult> Create<TOtherModel, TOtherResult>(Func<HubAppApi, AppApiAction<TOtherModel, TOtherResult>> getAction);
    Task<App> HubApp();
    Task<ModifierModel> HubAppModifier();
    Task<ModifierModel> GeneralUserGroupModifier();
}

internal sealed class HubActionTester<TRequest, TResult> : IHubActionTester
{
    private readonly Func<HubAppApi, AppApiAction<TRequest, TResult>> getAction;

    public HubActionTester
    (
        IServiceProvider services,
        Func<HubAppApi, AppApiAction<TRequest, TResult>> getAction
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

    public Task<AppUser> LoginAsAdmin() => LoginAs(new AppUserName("hubadmin"));

    public async Task<AppUser> LoginAs(AppUserName userName)
    {
        var factory = Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(userName);
        var currentUserName = Services.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(user.ToModel().UserName);
        return user;
    }

    public async Task<AppUser> Login(params AppRoleName[]? roleNames)
    {
        var modifier = await DefaultModifier();
        var user = await Login(modifier, roleNames);
        return user;
    }

    public Task<AppUser> Login(ModifierModel modifier, params AppRoleName[]? roleNames) =>
        Login([], modifier, roleNames);

    public async Task<AppUser> Login
    (
        AppRoleName[] defaultRoleNames, 
        ModifierModel modifier, 
        params AppRoleName[]? roleNames
    )
    {
        var factory = Services.GetRequiredService<HubFactory>();
        var userGroup = await factory.UserGroups.GetGeneral();
        var user = await userGroup.AddOrUpdate
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
        foreach (var roleName in defaultRoleNames)
        {
            var role = await hubApp.Role(roleName);
            await user.AssignRole(role);
        }
        var efModifier = await factory.Modifiers.Modifier(modifier.ID);
        foreach (var roleName in roleNames ?? [])
        {
            var role = await hubApp.Role(roleName);
            await user.Modifier(efModifier).AssignRole(role);
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

    public Task<ModifierModel> GeneralUserGroupModifier() =>
        UserGroupModifier(AppUserGroupName.General);

    public async Task<ModifierModel> UserGroupModifier(AppUserGroupName name)
    {
        var factory = Services.GetRequiredService<HubFactory>();
        var userGroup = await factory.UserGroups.UserGroup(name);
        var userGroupModel = userGroup.ToModel();
        var hubApp = await HubApp();
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups);
        var userGroupModifier = await userGroupsModCategory.AddOrUpdateModifier
        (
            userGroupModel.PublicKey,
            userGroupModel.ID.ToString(),
            userGroupModel.GroupName.DisplayText
        );
        return userGroupModifier.ToModel();
    }

    public async Task<ModifierModel> DefaultModifier()
    {
        var hubApp = await HubApp();
        var defaultModifier = await hubApp.DefaultModifier();
        return defaultModifier.ToModel();
    }

    public Task<ModifierModel> HubAppModifier() => AppModifier(HubInfo.AppKey);

    public async Task<ModifierModel> AppModifier(AppKey appKey)
    {
        var factory = Services.GetRequiredService<HubFactory>();
        var app = await factory.Apps.App(appKey);
        var appModel = app.ToModel();
        App hubApp;
        if (appKey.Equals(HubInfo.AppKey))
        {
            hubApp = app;
        }
        else
        {
            hubApp = await HubApp();
        }
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var hubAppModifier = await appsModCategory.AddOrUpdateModifier
        (
            appModel.PublicKey,
            appModel.ID.ToString(),
            appModel.AppKey.Format()
        );
        return hubAppModifier.ToModel();
    }

    public Task<TResult> Execute(TRequest requestData) =>
        Execute(requestData, ModifierKey.Default);

    public Task<TResult> Execute(TRequest requestData, ModifierModel modifier) =>
        Execute(requestData, modifier.ModKey);

    public async Task<TResult> Execute(TRequest requestData, ModifierKey modKey)
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
        var pathAccessor = Services.GetRequiredService<FakeModifierKeyAccessor>();
        pathAccessor.SetValue(modKey);
        var path = actionForSuperUser.Path.WithModifier(modKey);
        httpContextAccessor.HttpContext.Request.PathBase = $"/{path.App}/{path.Version.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{path.Group.Value}/{path.Action.Value}/";
        if (!path.Modifier.Equals(ModifierKey.Default))
        {
            httpContextAccessor.HttpContext.Request.Path += path.Modifier.Value;
        }
        var currentUserName = Services.GetRequiredService<ICurrentUserName>();
        var currentUserAccess = new CurrentUserAccess(userContext, appContext, currentUserName);
        var apiUser = new AppApiUser(currentUserAccess, pathAccessor);
        var hubApi = (HubAppApi)appApiFactory.Create(apiUser);
        var action = getAction(hubApi);
        var result = await action.Invoke(requestData);
        return result;
    }
}