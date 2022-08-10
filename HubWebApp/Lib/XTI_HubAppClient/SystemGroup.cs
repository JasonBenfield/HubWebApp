// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetUserContext: CreatePostAction<GetUserContextRequest, UserContextModel>("GetUserContext"));
    }

    public SystemGroupActions Actions { get; }

    public Task<AppContextModel> GetAppContext(GetAppContextRequest model) => Actions.GetAppContext.Post("", model);
    public Task<UserContextModel> GetUserContext(GetUserContextRequest model) => Actions.GetUserContext.Post("", model);
    public sealed record SystemGroupActions(AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetUserContextRequest, UserContextModel> GetUserContext);
}