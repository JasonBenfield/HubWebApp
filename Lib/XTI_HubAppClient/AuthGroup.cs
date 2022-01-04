// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthGroup : AppClientGroup
{
    public AuthGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "Auth")
    {
    }

    public Task<EmptyActionResult> VerifyLogin(VerifyLoginForm model) => Post<EmptyActionResult, VerifyLoginForm>("VerifyLogin", "", model);
}