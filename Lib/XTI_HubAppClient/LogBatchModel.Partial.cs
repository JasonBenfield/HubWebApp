using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient;

partial class LogBatchModel : ILogBatchModel
{
    public LogBatchModel(ILogBatchModel source)
    {
        StartSessions = source.StartSessions
            ?.Select(session => new StartSessionModel(session)).ToArray()
            ?? new StartSessionModel[] { };
        AuthenticateSessions = source.AuthenticateSessions
            ?.Select(session => new AuthenticateSessionModel(session)).ToArray()
            ?? new AuthenticateSessionModel[] { };
        StartRequests = source.StartRequests
            ?.Select(request => new StartRequestModel(request)).ToArray()
            ?? new StartRequestModel[] { };
        LogEvents = source.LogEvents
            ?.Select(evt => new LogEventModel(evt)).ToArray()
            ?? new LogEventModel[] { };
        EndRequests = source.EndRequests
            ?.Select(request => new EndRequestModel(request)).ToArray()
            ?? new EndRequestModel[] { };
        EndSessions = source.EndSessions
            ?.Select(session => new EndSessionModel(session)).ToArray()
            ?? new EndSessionModel[] { };
    }

    IAuthenticateSessionModel[] ILogBatchModel.AuthenticateSessions
    {
        get => AuthenticateSessions;
        set => AuthenticateSessions = value.Cast<AuthenticateSessionModel>().ToArray();
    }
    IEndRequestModel[] ILogBatchModel.EndRequests
    {
        get => EndRequests;
        set => EndRequests = value.Cast<EndRequestModel>().ToArray();
    }
    ILogEventModel[] ILogBatchModel.LogEvents
    {
        get => LogEvents;
        set => LogEvents = value.Cast<LogEventModel>().ToArray();
    }
    IStartRequestModel[] ILogBatchModel.StartRequests
    {
        get => StartRequests;
        set => StartRequests = value.Cast<StartRequestModel>().ToArray();
    }
    IStartSessionModel[] ILogBatchModel.StartSessions
    {
        get => StartSessions;
        set => StartSessions = value.Cast<StartSessionModel>().ToArray();
    }
    IEndSessionModel[] ILogBatchModel.EndSessions
    {
        get => EndSessions;
        set => EndSessions = value.Cast<EndSessionModel>().ToArray();
    }
}