// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly XtiTokenAccessorFactory xtiTokenAccessorFactory;
    private readonly AppClientUrl clientUrl;
    private readonly IAppClientSessionKey sessionKey;
    private readonly IAppClientRequestKey requestKey;
    private readonly HubAppClientVersion version;
    public HubAppClientFactory(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, IAppClientSessionKey sessionKey, IAppClientRequestKey requestKey, HubAppClientVersion version)
    {
        this.httpClientFactory = httpClientFactory;
        this.xtiTokenAccessorFactory = xtiTokenAccessorFactory;
        this.clientUrl = clientUrl;
        this.sessionKey = sessionKey;
        this.requestKey = requestKey;
        this.version = version;
    }

    public HubAppClient Create() => new HubAppClient(httpClientFactory, xtiTokenAccessorFactory, clientUrl, sessionKey, requestKey, version);
}