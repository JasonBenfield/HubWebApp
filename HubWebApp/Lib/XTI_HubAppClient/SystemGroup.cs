// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "System")
    {
    }

    public Task<AppContextModel> GetAppContext() => Post<AppContextModel, EmptyRequest>("GetAppContext", "", new EmptyRequest());
    public Task<UserContextModel> GetUserContext(GetUserContextRequest model) => Post<UserContextModel, GetUserContextRequest>("GetUserContext", "", model);
}