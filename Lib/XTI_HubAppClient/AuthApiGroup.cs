// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthApiGroup : AppClientGroup
{
    public AuthApiGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "AuthApi")
    {
    }

    public Task<LoginResult> Authenticate(LoginCredentials model) => Post<LoginResult, LoginCredentials>("Authenticate", "", model);
}