// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HomeGroup : AppClientGroup
{
    public HomeGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Home")
    {
        Actions = new HomeGroupActions(Index: CreateGetAction<EmptyRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public HomeGroupActions Actions { get; }

    public sealed record HomeGroupActions(AppClientGetAction<EmptyRequest> Index);
}