// Generated Code
namespace XTI_HubAppClient;
public sealed partial class LogsGroup : AppClientGroup
{
    public LogsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Logs")
    {
        Actions = new LogsGroupActions(AppRequest: CreateGetAction<AppRequestRequest>("AppRequest"), AppRequests: CreateGetAction<AppRequestQueryRequest>("AppRequests"), GetLogEntryDetail: CreatePostAction<int, AppLogEntryDetailModel>("GetLogEntryDetail"), GetLogEntryOrDefaultByKey: CreatePostAction<string, AppLogEntryModel>("GetLogEntryOrDefaultByKey"), GetRequestDetail: CreatePostAction<int, AppRequestDetailModel>("GetRequestDetail"), GetSessionDetail: CreatePostAction<int, AppSessionDetailModel>("GetSessionDetail"), LogEntries: CreateGetAction<LogEntryQueryRequest>("LogEntries"), LogEntry: CreateGetAction<LogEntryRequest>("LogEntry"), Session: CreateGetAction<SessionViewRequest>("Session"), Sessions: CreateGetAction<EmptyRequest>("Sessions"));
        Configure();
    }

    partial void Configure();
    public LogsGroupActions Actions { get; }

    public Task<AppLogEntryDetailModel> GetLogEntryDetail(int requestData, CancellationToken ct = default) => Actions.GetLogEntryDetail.Post("", requestData, ct);
    public Task<AppLogEntryModel> GetLogEntryOrDefaultByKey(string requestData, CancellationToken ct = default) => Actions.GetLogEntryOrDefaultByKey.Post("", requestData, ct);
    public Task<AppRequestDetailModel> GetRequestDetail(int requestData, CancellationToken ct = default) => Actions.GetRequestDetail.Post("", requestData, ct);
    public Task<AppSessionDetailModel> GetSessionDetail(int requestData, CancellationToken ct = default) => Actions.GetSessionDetail.Post("", requestData, ct);
    public sealed record LogsGroupActions(AppClientGetAction<AppRequestRequest> AppRequest, AppClientGetAction<AppRequestQueryRequest> AppRequests, AppClientPostAction<int, AppLogEntryDetailModel> GetLogEntryDetail, AppClientPostAction<string, AppLogEntryModel> GetLogEntryOrDefaultByKey, AppClientPostAction<int, AppRequestDetailModel> GetRequestDetail, AppClientPostAction<int, AppSessionDetailModel> GetSessionDetail, AppClientGetAction<LogEntryQueryRequest> LogEntries, AppClientGetAction<LogEntryRequest> LogEntry, AppClientGetAction<SessionViewRequest> Session, AppClientGetAction<EmptyRequest> Sessions);
}