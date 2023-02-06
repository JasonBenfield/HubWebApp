// Generated Code
namespace XTI_HubAppClient;
public sealed partial class LogsGroup : AppClientGroup
{
    public LogsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Logs")
    {
        Actions = new LogsGroupActions(GetLogEntryByKey: CreatePostAction<string, AppLogEntryModel>("GetLogEntryByKey"), GetLogEntryDetail: CreatePostAction<int, AppLogEntryDetailModel>("GetLogEntryDetail"), GetRequestDetail: CreatePostAction<int, AppRequestDetailModel>("GetRequestDetail"), GetSessionDetail: CreatePostAction<int, AppSessionDetailModel>("GetSessionDetail"), Sessions: CreateGetAction<EmptyRequest>("Sessions"), Session: CreateGetAction<SessionViewRequest>("Session"), AppRequests: CreateGetAction<AppRequestQueryRequest>("AppRequests"), AppRequest: CreateGetAction<AppRequestRequest>("AppRequest"), LogEntry: CreateGetAction<LogEntryRequest>("LogEntry"), LogEntries: CreateGetAction<LogEntryQueryRequest>("LogEntries"));
    }

    public LogsGroupActions Actions { get; }

    public Task<AppLogEntryModel> GetLogEntryByKey(string model, CancellationToken ct = default) => Actions.GetLogEntryByKey.Post("", model, ct);
    public Task<AppLogEntryDetailModel> GetLogEntryDetail(int model, CancellationToken ct = default) => Actions.GetLogEntryDetail.Post("", model, ct);
    public Task<AppRequestDetailModel> GetRequestDetail(int model, CancellationToken ct = default) => Actions.GetRequestDetail.Post("", model, ct);
    public Task<AppSessionDetailModel> GetSessionDetail(int model, CancellationToken ct = default) => Actions.GetSessionDetail.Post("", model, ct);
    public sealed record LogsGroupActions(AppClientPostAction<string, AppLogEntryModel> GetLogEntryByKey, AppClientPostAction<int, AppLogEntryDetailModel> GetLogEntryDetail, AppClientPostAction<int, AppRequestDetailModel> GetRequestDetail, AppClientPostAction<int, AppSessionDetailModel> GetSessionDetail, AppClientGetAction<EmptyRequest> Sessions, AppClientGetAction<SessionViewRequest> Session, AppClientGetAction<AppRequestQueryRequest> AppRequests, AppClientGetAction<AppRequestRequest> AppRequest, AppClientGetAction<LogEntryRequest> LogEntry, AppClientGetAction<LogEntryQueryRequest> LogEntries);
}