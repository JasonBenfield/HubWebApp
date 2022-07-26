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

    private ODataGroup<EmptyRequest, ExpandedRequest>? _RequestQuery;

    public ODataGroup<EmptyRequest, ExpandedRequest> RequestQuery
    {
        get => _RequestQuery ?? throw new ArgumentNullException(nameof(_RequestQuery));
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
        _RequestQuery = new ODataGroup<EmptyRequest, ExpandedRequest>
        (
            source.AddGroup(nameof(RequestQuery)),
            () => sp.GetRequiredService<RequestQueryAction>()
        );
    }
}