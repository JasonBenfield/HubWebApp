// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallationsGroup : AppClientGroup
{
    public InstallationsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Installations")
    {
        Actions = new InstallationsGroupActions(Index: CreateGetAction<InstallationQueryRequest>("Index"));
    }

    public InstallationsGroupActions Actions { get; }

    public sealed record InstallationsGroupActions(AppClientGetAction<InstallationQueryRequest> Index);
}