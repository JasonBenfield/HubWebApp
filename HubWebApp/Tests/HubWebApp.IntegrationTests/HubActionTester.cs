using Microsoft.AspNetCore.Http;

namespace HubWebApp.IntegrationTests;

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
        httpContextAccessor.HttpContext.Request.PathBase = $"/{actionForSuperUser.Path.App}/{actionForSuperUser.Path.Version.DisplayText}".Replace(" ", "");
        var modKeyPath = modKey.Equals(ModifierKey.Default) ? "" : $"/{modKey.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{actionForSuperUser.Path.Group.DisplayText}/{actionForSuperUser.Path.Action.DisplayText}{modKeyPath}".Replace(" ", "");
        var appKey = Services.GetRequiredService<AppKey>();
        var userContext = Services.GetRequiredService<ISourceUserContext>();
        var modifierKeyAccessor = Services.GetRequiredService<FakeModifierKeyAccessor>();
        modifierKeyAccessor.SetValue(modKey);
        var path = actionForSuperUser.Path.WithModifier(modKey);
        httpContextAccessor.HttpContext.Request.PathBase = $"/{path.App}/{path.Version.Value}";
        httpContextAccessor.HttpContext.Request.Path = $"/{path.Group.Value}/{path.Action.Value}/";
        if (!path.Modifier.Equals(ModifierKey.Default))
        {
            httpContextAccessor.HttpContext.Request.Path += path.Modifier.Value;
        }
        var currentUserName = Services.GetRequiredService<ICurrentUserName>();
        var currentUserAccess = new CurrentUserAccess(userContext, appContext, currentUserName);
        var apiUser = new AppApiUser(currentUserAccess, modifierKeyAccessor);
        var hubApi = (HubAppApi)appApiFactory.Create(apiUser);
        var action = getAction(hubApi);
        var result = await action.Invoke(model);
        return result;
    }
}