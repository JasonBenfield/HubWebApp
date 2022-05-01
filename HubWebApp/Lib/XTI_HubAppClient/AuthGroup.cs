// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthGroup : AppClientGroup
{
    public AuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Auth")
    {
        Actions = new AuthActions(clientUrl);
    }

    public AuthActions Actions { get; }

    public Task<string> VerifyLogin(VerifyLoginForm model) => Post<string, VerifyLoginForm>("VerifyLogin", "", model);
    public Task<string> LoginReturnKey(LoginReturnModel model) => Post<string, LoginReturnModel>("LoginReturnKey", "", model);
}