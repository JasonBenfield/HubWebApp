// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class AuthenticatorAppClient : AppClient
{
    public AuthenticatorAppClient(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AuthenticatorAppClientVersion version) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Authenticator", version.Value)
    {
        User = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserCache = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserCacheGroup(_clientFactory, _tokenAccessor, _url, _options));
        Home = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new HomeGroup(_clientFactory, _tokenAccessor, _url, _options));
    }

    public AuthenticatorRoleNames RoleNames { get; } = AuthenticatorRoleNames.Instance;
    public string AppName { get; } = "Authenticator";
    public UserGroup User { get; }

    public UserCacheGroup UserCache { get; }

    public HomeGroup Home { get; }
}