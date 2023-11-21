// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly XtiTokenAccessorFactory xtiTokenAccessorFactory;
    private readonly AppClientUrl clientUrl;
    private readonly IAppClientRequestKey requestKey;
    private readonly HubAppClientVersion version;
    public HubAppClientFactory(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, IAppClientRequestKey requestKey, HubAppClientVersion version)
    {
        this.httpClientFactory = httpClientFactory;
        this.xtiTokenAccessorFactory = xtiTokenAccessorFactory;
        this.clientUrl = clientUrl;
        this.requestKey = requestKey;
        this.version = version;
    }

    public HubAppClient Create() => new HubAppClient(httpClientFactory, xtiTokenAccessorFactory, clientUrl, requestKey, version);
}