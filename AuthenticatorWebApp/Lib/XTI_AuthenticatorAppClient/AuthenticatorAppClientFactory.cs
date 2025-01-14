// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class AuthenticatorAppClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly XtiTokenAccessorFactory xtiTokenAccessorFactory;
    private readonly AppClientUrl clientUrl;
    private readonly IAppClientSessionKey sessionKey;
    private readonly IAppClientRequestKey requestKey;
    private readonly AuthenticatorAppClientVersion version;
    public AuthenticatorAppClientFactory(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, IAppClientSessionKey sessionKey, IAppClientRequestKey requestKey, AuthenticatorAppClientVersion version)
    {
        this.httpClientFactory = httpClientFactory;
        this.xtiTokenAccessorFactory = xtiTokenAccessorFactory;
        this.clientUrl = clientUrl;
        this.sessionKey = sessionKey;
        this.requestKey = requestKey;
        this.version = version;
    }

    public AuthenticatorAppClient Create() => new AuthenticatorAppClient(httpClientFactory, xtiTokenAccessorFactory, clientUrl, sessionKey, requestKey, version);
}