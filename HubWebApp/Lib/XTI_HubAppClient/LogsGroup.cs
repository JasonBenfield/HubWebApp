// Generated Code
namespace XTI_HubAppClient;
public sealed partial class LogsGroup : AppClientGroup
{
    public LogsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Logs")
    {
        Actions = new LogsGroupActions(AppRequest: CreateGetAction<AppRequestRequest>("AppRequest"), AppRequests: CreateGetAction<AppRequestQueryRequest>("AppRequests"), GetLogEntryDetail: CreatePostAction<int, AppLogEntryDetailModel>("GetLogEntryDetail"), GetLogEntryOrDefaultByKey: CreatePostAction<string, AppLogEntryModel>("GetLogEntryOrDefaultByKey"), GetRequestDetail: CreatePostAction<int, AppRequestDetailModel>("GetRequestDetail"), GetSessionDetail: CreatePostAction<int, AppSessionDetailModel>("GetSessionDetail"), LogEntries: CreateGetAction<LogEntryQueryRequest>("LogEntries"), LogEntry: CreateGetAction<LogEntryRequest>("LogEntry"), Session: CreateGetAction<SessionViewRequest>("Session"), Sessions: CreateGetAction<EmptyRequest>("Sessions"));
    }

    public LogsGroupActions Actions { get; }

    public Task<AppLogEntryDetailModel> GetLogEntryDetail(int model, CancellationToken ct = default) => Actions.GetLogEntryDetail.Post("", model, ct);
    public Task<AppLogEntryModel> GetLogEntryOrDefaultByKey(string model, CancellationToken ct = default) => Actions.GetLogEntryOrDefaultByKey.Post("", model, ct);
    public Task<AppRequestDetailModel> GetRequestDetail(int model, CancellationToken ct = default) => Actions.GetRequestDetail.Post("", model, ct);
    public Task<AppSessionDetailModel> GetSessionDetail(int model, CancellationToken ct = default) => Actions.GetSessionDetail.Post("", model, ct);
    public sealed record LogsGroupActions(AppClientGetAction<AppRequestRequest> AppRequest, AppClientGetAction<AppRequestQueryRequest> AppRequests, AppClientPostAction<int, AppLogEntryDetailModel> GetLogEntryDetail, AppClientPostAction<string, AppLogEntryModel> GetLogEntryOrDefaultByKey, AppClientPostAction<int, AppRequestDetailModel> GetRequestDetail, AppClientPostAction<int, AppSessionDetailModel> GetSessionDetail, AppClientGetAction<LogEntryQueryRequest> LogEntries, AppClientGetAction<LogEntryRequest> LogEntry, AppClientGetAction<SessionViewRequest> Session, AppClientGetAction<EmptyRequest> Sessions);
}