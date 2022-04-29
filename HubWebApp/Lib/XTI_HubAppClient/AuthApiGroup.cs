// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthApiGroup : AppClientGroup
{
    public AuthApiGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "AuthApi")
    {
    }

    public Task<LoginResult> Authenticate(LoginCredentials model) => Post<LoginResult, LoginCredentials>("Authenticate", "", model);
    public Task<EmptyActionResult> Logout() => Post<EmptyActionResult, EmptyRequest>("Logout", "", new EmptyRequest());
}