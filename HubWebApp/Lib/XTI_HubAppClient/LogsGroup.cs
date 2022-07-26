// Generated Code
namespace XTI_HubAppClient;
public sealed partial class LogsGroup : AppClientGroup
{
    public LogsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Logs")
    {
        Actions = new LogsGroupActions(Sessions: CreateGetAction<EmptyRequest>("Sessions"), Requests: CreateGetAction<EmptyRequest>("Requests"), LogEntries: CreateGetAction<EmptyRequest>("LogEntries"));
    }

    public LogsGroupActions Actions { get; }

    public sealed record LogsGroupActions(AppClientGetAction<EmptyRequest> Sessions, AppClientGetAction<EmptyRequest> Requests, AppClientGetAction<EmptyRequest> LogEntries);
}