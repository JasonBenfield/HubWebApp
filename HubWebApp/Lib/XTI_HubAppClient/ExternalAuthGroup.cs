// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExternalAuthGroup : AppClientGroup
{
    public ExternalAuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "ExternalAuth")
    {
    }

    public Task<string> ExternalAuthKey(string modifier, ExternalAuthKeyModel model) => Post<string, ExternalAuthKeyModel>("ExternalAuthKey", modifier, model);
}