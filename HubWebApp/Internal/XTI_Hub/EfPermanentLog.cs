using XTI_App.Abstractions;
using XTI_Core;
using XTI_TempLog.Abstractions;

namespace XTI_Hub;

public sealed class EfPermanentLog : XTI_PermanentLog.IPermanentLog
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public EfPermanentLog(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task LogBatch(LogBatchModel batch, CancellationToken ct)
    {
        foreach (var startSession in batch.StartSessions)
        {
            await StartSession(startSession);
        }
        foreach (var authSession in batch.AuthenticateSessions)
        {
            await AuthenticateSession(authSession);
        }
        foreach (var startRequest in batch.StartRequests)
        {
            await StartRequest(startRequest);
        }
        foreach (var logEvent in batch.LogEntries)
        {
            await LogEvent(logEvent);
        }
        foreach (var endRequest in batch.EndRequests)
        {
            await EndRequest(endRequest);
        }
        foreach (var endSession in batch.EndSessions)
        {
            await EndSession(endSession);
        }
    }

    public async Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct)
    {
        foreach (var sessionDetailRequest in sessionDetails)
        {
            var user = await hubFactory.Users.UserOrAnon(new AppUserName(sessionDetailRequest.Session.SessionKey.UserName));
            var session = await hubFactory.Sessions.AddOrUpdate
            (
                sessionKey: sessionDetailRequest.Session.SessionKey.ID,
                user: user,
                timeStarted: sessionDetailRequest.Session.TimeStarted,
                timeEnded: sessionDetailRequest.Session.TimeEnded,
                requesterKey: sessionDetailRequest.Session.RequesterKey,
                userAgent: sessionDetailRequest.Session.UserAgent,
                remoteAddress: sessionDetailRequest.Session.RemoteAddress
            );
            foreach (var requestDetail in sessionDetailRequest.RequestDetails)
            {
                var installation = await hubFactory.Installations.InstallationOrDefault(requestDetail.Request.InstallationID);
                var request = await session.LogRequest
                (
                    requestKey: requestDetail.Request.RequestKey,
                    installation: installation,
                    path: requestDetail.Request.Path,
                    timeStarted: requestDetail.Request.TimeStarted,
                    timeEnded: requestDetail.Request.TimeEnded,
                    actualCount: requestDetail.Request.ActualCount,
                    sourceRequestKey: requestDetail.Request.SourceRequestKey
                );
                foreach (var logEntry in requestDetail.LogEntries)
                {
                    await request.LogEvent
                    (
                        logEntryKey: logEntry.EventKey,
                        severity: AppEventSeverity.Values.Value(logEntry.Severity),
                        timeOccurred: logEntry.TimeOccurred,
                        caption: logEntry.Caption,
                        message: logEntry.Message,
                        detail: logEntry.Detail,
                        actualCount: logEntry.ActualCount,
                        sourceLogEntryKey: logEntry.ParentEventKey,
                        category: logEntry.Category
                    );
                }
            }
        }
    }

    private async Task StartSession(StartSessionModel startSession)
    {
        try
        {
            var user = await hubFactory.Users.UserOrAnon(new AppUserName(startSession.UserName));
            var session = await hubFactory.Sessions.AddOrUpdate
            (
                startSession.SessionKey,
                user,
                startSession.TimeStarted,
                DateTimeOffset.MaxValue,
                startSession.RequesterKey,
                startSession.UserAgent,
                startSession.RemoteAddress
            );
        }
        catch (Exception ex)
        {
            await HandleError(ex);
        }
    }

    private async Task AuthenticateSession(AuthenticateSessionModel model)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(model.SessionKey, clock.Now());
            var user = await hubFactory.Users.UserOrAnon(new AppUserName(model.UserName));
            await session.Authenticate(user);
        }
        catch (Exception ex)
        {
            await HandleError(ex);
        }
    }

    private async Task StartRequest(StartRequestModel startRequest)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(startRequest.SessionKey, clock.Now());
            var installation = await hubFactory.Installations.InstallationOrDefault(startRequest.InstallationID);
            var request = await session.LogRequest
            (
                startRequest.RequestKey,
                installation,
                startRequest.Path,
                startRequest.TimeStarted,
                DateTimeOffset.MaxValue,
                startRequest.ActualCount,
                startRequest.SourceRequestKey
            );
        }
        catch (Exception ex)
        {
            await HandleError(ex);
        }
    }

    private async Task LogEvent(LogEntryModelV1 model)
    {
        try
        {
            await logEvent(model);
        }
        catch (Exception ex)
        {
            await HandleError(ex);
        }
    }

    private async Task EndRequest(EndRequestModel model)
    {
        try
        {
            var request = await hubFactory.Requests.RequestOrPlaceHolder(model.RequestKey, clock.Now());
            await request.End(model.TimeEnded);
        }
        catch (Exception ex)
        {
            await HandleError(ex);
        }
    }

    private async Task EndSession(EndSessionModel model)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(model.SessionKey, clock.Now());
            await session.End(model.TimeEnded);
        }
        catch (Exception ex)
        {
            await HandleError(ex);
        }
    }

    private Task HandleError(Exception ex) =>
        logEvent
        (
            new LogEntryModelV1
            {
                Caption = "Error Updating Permanent Log",
                Message = ex.Message,
                Detail = ex.StackTrace ?? "",
                Severity = AppEventSeverity.Values.AppError,
                TimeOccurred = clock.Now()
            }
        );

    private async Task logEvent(LogEntryModelV1 model)
    {
        var request = await hubFactory.Requests.RequestOrPlaceHolder(model.RequestKey, clock.Now());
        var severity = AppEventSeverity.Values.Value(model.Severity);
        await request.LogEvent
        (
            model.EventKey,
            severity,
            model.TimeOccurred,
            model.Caption,
            model.Message,
            model.Detail,
            model.ActualCount,
            model.ParentEventKey,
            model.Category
        );
    }
}
