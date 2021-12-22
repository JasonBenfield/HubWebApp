// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthGroup : AppClientGroup
{
    public AuthGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl) : base(httpClientFactory, xtiToken, baseUrl, "Auth")
    {
    }

    public Task<EmptyActionResult> VerifyLogin(VerifyLoginForm model) => Post<EmptyActionResult, VerifyLoginForm>("VerifyLogin", "", model);
}