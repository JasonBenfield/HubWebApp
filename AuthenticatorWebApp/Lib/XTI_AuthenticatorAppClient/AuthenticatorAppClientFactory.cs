// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class AuthenticatorAppClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly XtiTokenAccessorFactory xtiTokenAccessorFactory;
    private readonly AppClientUrl clientUrl;
    private readonly AppClientOptions options;
    private readonly AuthenticatorAppClientVersion version;
    public AuthenticatorAppClientFactory(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, AppClientOptions options, AuthenticatorAppClientVersion version)
    {
        this.httpClientFactory = httpClientFactory;
        this.xtiTokenAccessorFactory = xtiTokenAccessorFactory;
        this.clientUrl = clientUrl;
        this.options = options;
        this.version = version;
    }

    public AuthenticatorAppClient Create() => new AuthenticatorAppClient(httpClientFactory, xtiTokenAccessorFactory, clientUrl, options, version);
}