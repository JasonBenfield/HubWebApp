// Generated Code
namespace XTI_HubAppClient;
public sealed partial class LogsGroup : AppClientGroup
{
    public LogsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Logs")
    {
        Actions = new LogsGroupActions(Sessions: CreateGetAction<EmptyRequest>("Sessions"), Requests: CreateGetAction<RequestQueryRequest>("Requests"), LogEntries: CreateGetAction<LogEntryQueryRequest>("LogEntries"));
    }

    public LogsGroupActions Actions { get; }

    public sealed record LogsGroupActions(AppClientGetAction<EmptyRequest> Sessions, AppClientGetAction<RequestQueryRequest> Requests, AppClientGetAction<LogEntryQueryRequest> LogEntries);
}