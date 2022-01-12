using XTI_App.Api;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Auth;

internal sealed class LoginAction : AppAction<LoginModel, WebRedirectResult>
{
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;

    public LoginAction(Authentication auth, IAnonClient anonClient)
    {
        this.auth = auth;
        this.anonClient = anonClient;
    }

    public async Task<WebRedirectResult> Execute(LoginModel model)
    {
        await auth.Authenticate(model.Credentials.UserName, model.Credentials.Password);
        anonClient.Load();
        anonClient.Persist("", DateTimeOffset.MinValue, anonClient.RequesterKey);
        var startUrl = new StartUrl(model.StartUrl, model.ReturnUrl).Value;
        return new WebRedirectResult(startUrl);
    }
}