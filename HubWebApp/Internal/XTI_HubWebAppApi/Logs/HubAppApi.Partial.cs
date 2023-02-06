using Microsoft.AspNetCore.Http;
using XTI_HubDB.Entities;
using XTI_HubWebAppApi.Logs;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private LogsGroup? _Logs;

    public LogsGroup Logs { get => _Logs ?? throw new ArgumentNullException(nameof(_Logs)); }

    private ODataGroup<EmptyRequest, ExpandedSession>? _SessionQuery;

    public ODataGroup<EmptyRequest, ExpandedSession> SessionQuery
    {
        get => _SessionQuery ?? throw new ArgumentNullException(nameof(_SessionQuery));
    }

    private ODataGroup<AppRequestQueryRequest, ExpandedRequest>? _RequestQuery;

    public ODataGroup<AppRequestQueryRequest, ExpandedRequest> RequestQuery
    {
        get => _RequestQuery ?? throw new ArgumentNullException(nameof(_RequestQuery));
    }

    private ODataGroup<LogEntryQueryRequest, ExpandedLogEntry>? _LogEntryQuery;

    public ODataGroup<LogEntryQueryRequest, ExpandedLogEntry> LogEntryQuery
    {
        get => _LogEntryQuery ?? throw new ArgumentNullException(nameof(_LogEntryQuery));
    }

    partial void createLogsGroup(IServiceProvider sp)
    {
        _Logs = new LogsGroup
        (
            source.AddGroup(nameof(Logs)),
            sp
        );
        _SessionQuery = new ODataGroup<EmptyRequest, ExpandedSession>
        (
            source.AddGroup(nameof(SessionQuery)),
            () => sp.GetRequiredService<SessionQueryAction>()
        );
        _RequestQuery = new ODataGroup<AppRequestQueryRequest, ExpandedRequest>
        (
            source.AddGroup(nameof(RequestQuery)),
            () => sp.GetRequiredService<AppRequestQueryAction>()
        );
        _LogEntryQuery = new ODataGroup<LogEntryQueryRequest, ExpandedLogEntry>
        (
            source.AddGroup(nameof(LogEntryQuery)),
            () => sp.GetRequiredService<LogEntryQueryAction>()
        );
    }
}