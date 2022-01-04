// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthenticatorsGroup : AppClientGroup
{
    public AuthenticatorsGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "Authenticators")
    {
    }

    public Task<EmptyActionResult> RegisterAuthenticator(string modifier) => Post<EmptyActionResult, EmptyRequest>("RegisterAuthenticator", modifier, new EmptyRequest());
    public Task<EmptyActionResult> RegisterUserAuthenticator(string modifier, RegisterUserAuthenticatorRequest model) => Post<EmptyActionResult, RegisterUserAuthenticatorRequest>("RegisterUserAuthenticator", modifier, model);
}