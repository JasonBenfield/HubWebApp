// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly XtiTokenAccessorFactory xtiTokenAccessorFactory;
    private readonly AppClientUrl clientUrl;
    private readonly AppClientOptions options;
    private readonly HubAppClientVersion version;
    public HubAppClientFactory(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, AppClientOptions options, HubAppClientVersion version)
    {
        this.httpClientFactory = httpClientFactory;
        this.xtiTokenAccessorFactory = xtiTokenAccessorFactory;
        this.clientUrl = clientUrl;
        this.options = options;
        this.version = version;
    }

    public HubAppClient Create() => new HubAppClient(httpClientFactory, xtiTokenAccessorFactory, clientUrl, options, version);
}