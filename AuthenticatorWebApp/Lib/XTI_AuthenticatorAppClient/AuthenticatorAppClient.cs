// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class AuthenticatorAppClient : AppClient
{
    public AuthenticatorAppClient(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, IAppClientSessionKey sessionKey, IAppClientRequestKey requestKey, AuthenticatorAppClientVersion version) : base(httpClientFactory, xtiTokenAccessorFactory, clientUrl, sessionKey, requestKey, "Authenticator", version.Value)
    {
        Home = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new HomeGroup(_clientFactory, _tokenAccessor, _url, _options));
        Configure();
    }

    partial void Configure();
    public AuthenticatorRoleNames RoleNames { get; } = AuthenticatorRoleNames.Instance;
    public string AppName { get; } = "Authenticator";
    public HomeGroup Home { get; }
}