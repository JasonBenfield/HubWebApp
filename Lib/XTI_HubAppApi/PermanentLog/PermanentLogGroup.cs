using XTI_App.Api;
using XTI_TempLog;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.PermanentLog
{
    public sealed class PermanentLogGroup : AppApiGroupWrapper
    {
        public PermanentLogGroup(AppApiGroup source, PermanentLogGroupActionFactory actionFactory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            LogBatch = source.AddAction(actions.Action(nameof(LogBatch), actionFactory.CreateLogBatch));
            StartSession = source.AddAction(actions.Action(nameof(StartSession), actionFactory.CreateStartSession));
            StartRequest = source.AddAction(actions.Action(nameof(StartRequest), actionFactory.CreateStartRequest));
            EndRequest = source.AddAction(actions.Action(nameof(EndRequest), actionFactory.CreateEndRequest));
            EndSession = source.AddAction(actions.Action(nameof(EndSession), actionFactory.CreateEndSession));
            LogEvent = source.AddAction(actions.Action(nameof(LogEvent), actionFactory.CreateLogEvent));
            AuthenticateSession = source.AddAction(actions.Action(nameof(AuthenticateSession), actionFactory.CreateAuthenticateSession));
            EndExpiredSessions = source.AddAction(actions.Action(nameof(EndExpiredSessions), actionFactory.CreateEndExpiredSessions));
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
}
