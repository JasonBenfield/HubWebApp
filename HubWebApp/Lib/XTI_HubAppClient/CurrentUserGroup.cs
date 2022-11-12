// Generated Code
namespace XTI_HubAppClient;
public sealed partial class CurrentUserGroup : AppClientGroup
{
    public CurrentUserGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "CurrentUser")
    {
        Actions = new CurrentUserGroupActions(Index: CreateGetAction<EmptyRequest>("Index"));
    }

    public CurrentUserGroupActions Actions { get; }

    public sealed record CurrentUserGroupActions(AppClientGetAction<EmptyRequest> Index);
}