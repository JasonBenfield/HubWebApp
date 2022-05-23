using Microsoft.AspNetCore.Http;

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
        var userContext = Services.GetRequiredService<FakeUserContext>();
        userContext.SetCurrentUser(AppUserName.Anon);
    }

    public FakeAppUser LoginAsAdmin()
    {
        var userContext = Services.GetRequiredService<FakeUserContext>();
        var user = userContext.User(new AppUserName("hubadmin"));
        userContext.SetCurrentUser(user.UserName());
        return user;
    }

    public FakeAppUser Login(params AppRoleName[]? roleNames)
    {
        var appContext = Services.GetRequiredService<FakeAppContext>();
        var defaultModifier = appContext.App().DefaultModifier();
        return Login(defaultModifier, roleNames);
    }

    public FakeAppUser Login(FakeModifier modifier, params AppRoleName[]? roleNames)
    {
        var userContext = Services.GetRequiredService<FakeUserContext>();
        var user = userContext.AddUser(new AppUserName("loggedinUser"));
        if(roleNames != null)
        {
            user.AddRoles(modifier, roleNames);
        }
        userContext.SetCurrentUser(user.UserName());
        return user;
    }

    public Task<App> HubApp()
    {
        var factory = Services.GetRequiredService<HubFactory>();
        return factory.Apps.App(HubInfo.AppKey);
    }

    public FakeApp FakeHubApp()
    {
        var appContext = Services.GetRequiredService<FakeAppContext>();
        return appContext.App();
    }

    public async Task<AppRole> AdminRole()
    {
        var app = await HubApp();
        var role = await app.Role(AppRoleName.Admin);
        return role;
    }

    public async Task<Modifier> HubAppModifier()
    {
        var hubApp = await HubApp();
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var hubAppModifier = await appsModCategory.AddOrUpdateModifier
        (
            hubApp.ID.ToString(),
            hubApp.Key().Name.DisplayText
        );
        return hubAppModifier;
    }

    public FakeModifier FakeHubAppModifier()
    {
        var fakeHubApp = FakeHubApp();
        var modCategory = fakeHubApp.ModCategory(HubInfo.ModCategories.Apps);
        var appContext = Services.GetRequiredService<FakeAppContext>();
        var app = appContext.App();
        return modCategory.ModifierByTargetID(app.ID.ToString());
    }

    public Task<TResult> Execute(TModel model) =>
        Execute(model, ModifierKey.Default);

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
        httpContextAccessor.HttpContext.Request.PathBase = $"/{actionForSuperUser.Path.App.DisplayText}/{actionForSuperUser.Path.Version.DisplayText}".Replace(" ", "");
        var modKeyPath = modKey.Equals(ModifierKey.Default) ? "" : $"/{modKey.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{actionForSuperUser.Path.Group.DisplayText}/{actionForSuperUser.Path.Action.DisplayText}{modKeyPath}".Replace(" ", "");
        var appKey = Services.GetRequiredService<AppKey>();
        var userContext = Services.GetRequiredService<ISourceUserContext>();
        var pathAccessor = Services.GetRequiredService<FakeXtiPathAccessor>();
        var path = actionForSuperUser.Path.WithModifier(modKey ?? ModifierKey.Default);
        pathAccessor.SetPath(path);
        httpContextAccessor.HttpContext.Request.PathBase = $"/{path.App.Value}/{path.Version.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{path.Group.Value}/{path.Action.Value}/";
        if (!path.Modifier.Equals(ModifierKey.Default))
        {
            httpContextAccessor.HttpContext.Request.Path += path.Modifier.Value;
        }
        var currentUserName = Services.GetRequiredService<ICurrentUserName>();
        var apiUser = new AppApiUser(appContext, userContext, currentUserName, pathAccessor);
        var hubApi = (HubAppApi)appApiFactory.Create(apiUser);
        var action = getAction(hubApi);
        var result = await action.Invoke(model);
        return result;
    }
}