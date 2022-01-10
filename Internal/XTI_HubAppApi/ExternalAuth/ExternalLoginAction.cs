using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.ExternalAuth;

public sealed class ExternalLoginRequest
{
    public string ExternalUserKey { get; set; } = "";
    public string StartUrl { get; set; } = "";
    public string ReturnUrl { get; set; } = "";
}

internal sealed class ExternalLoginAction : AppAction<ExternalLoginRequest, WebRedirectResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory appFactory;
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;

    public ExternalLoginAction(AppFromPath appFromPath, AppFactory appFactory, Authentication auth, IAnonClient anonClient)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
        this.auth = auth;
        this.anonClient = anonClient;
    }

    public async Task<WebRedirectResult> Execute(ExternalLoginRequest model)
    {
        var app = await appFromPath.Value();
        var user = await appFactory.Users.UserByExternalKey(app, model.ExternalUserKey);
        await auth.Authenticate(user);
        anonClient.Load();
        anonClient.Persist("", DateTimeOffset.MinValue, anonClient.RequesterKey);
        var startUrl = new StartUrl(model.StartUrl, model.ReturnUrl).Value;
        return new WebRedirectResult(startUrl);
    }
}