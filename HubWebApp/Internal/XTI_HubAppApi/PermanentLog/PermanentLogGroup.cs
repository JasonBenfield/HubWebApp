using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        LogBatch = source.AddAction(actions.Action(nameof(LogBatch), () => sp.GetRequiredService<LogBatchAction>()));
        StartSession = source.AddAction(actions.Action(nameof(StartSession), () => sp.GetRequiredService<StartSessionAction>()));
        StartRequest = source.AddAction(actions.Action(nameof(StartRequest), () => sp.GetRequiredService<StartRequestAction>()));
        EndRequest = source.AddAction(actions.Action(nameof(EndRequest), () => sp.GetRequiredService<EndRequestAction>()));
        EndSession = source.AddAction(actions.Action(nameof(EndSession), () => sp.GetRequiredService<EndSessionAction>()));
        LogEvent = source.AddAction(actions.Action(nameof(LogEvent), () => sp.GetRequiredService<LogEventAction>()));
        AuthenticateSession = source.AddAction(actions.Action(nameof(AuthenticateSession), () => sp.GetRequiredService<AuthenticateSessionAction>()));
        EndExpiredSessions = source.AddAction(actions.Action(nameof(EndExpiredSessions), () => sp.GetRequiredService<EndExpiredSessionsAction>()));
    }

    public AppApiAction<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiAction<StartSessionModel, EmptyActionResult> StartSession { get; }
    public AppApiAction<StartRequestModel, EmptyActionResult> StartRequest { get; }
    public AppApiAction<EndRequestModel, EmptyActionResult> EndRequest { get; }
    public AppApiAction<EndSessionModel, EmptyActionResult> EndSession { get; }
    public AppApiAction<LogEventModel, EmptyActionResult> LogEvent { get; }
    public AppApiAction<AuthenticateSessionModel, EmptyActionResult> AuthenticateSession { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
}