// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthGroup : AppClientGroup
{
    public AuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Auth")
    {
    }

    public Task<EmptyActionResult> VerifyLogin(VerifyLoginForm model) => Post<EmptyActionResult, VerifyLoginForm>("VerifyLogin", "", model);
}