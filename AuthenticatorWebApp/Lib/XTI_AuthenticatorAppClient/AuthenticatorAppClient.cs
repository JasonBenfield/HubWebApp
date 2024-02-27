// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class AuthenticatorAppClient : AppClient
{
    public AuthenticatorAppClient(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AuthenticatorAppClientVersion version) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Authenticator", version.Value)
    {
        Home = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new HomeGroup(_clientFactory, _tokenAccessor, _url, _options));
    }

    public AuthenticatorRoleNames RoleNames { get; } = AuthenticatorRoleNames.Instance;
    public string AppName { get; } = "Authenticator";
    public HomeGroup Home { get; }
}