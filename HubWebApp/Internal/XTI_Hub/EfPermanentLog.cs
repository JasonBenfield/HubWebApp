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
            await StartSession(startSession, ct);
        }
        foreach (var authSession in batch.AuthenticateSessions)
        {
            await AuthenticateSession(authSession, ct);
        }
        foreach (var startRequest in batch.StartRequests)
        {
            await StartRequest(startRequest, ct);
        }
        foreach (var logEvent in batch.LogEntries)
        {
            await LogEvent(logEvent, ct);
        }
        foreach (var endRequest in batch.EndRequests)
        {
            await EndRequest(endRequest, ct);
        }
        foreach (var endSession in batch.EndSessions)
        {
            await EndSession(endSession, ct);
        }
    }

    public async Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct)
    {
        foreach (var sessionDetailRequest in sessionDetails)
        {
            var user = await hubFactory.Users.UserOrAnon(new AppUserName(sessionDetailRequest.Session.SessionKey.UserName), ct);
            var session = await hubFactory.Sessions.AddOrUpdate
            (
                sessionKey: sessionDetailRequest.Session.SessionKey.ID,
                user: user,
                timeStarted: sessionDetailRequest.Session.TimeStarted,
                timeEnded: sessionDetailRequest.Session.TimeEnded,
                requesterKey: sessionDetailRequest.Session.RequesterKey,
                userAgent: sessionDetailRequest.Session.UserAgent,
                remoteAddress: sessionDetailRequest.Session.RemoteAddress,
                ct: ct
            );
            foreach (var requestDetail in sessionDetailRequest.RequestDetails)
            {
                var installation = await hubFactory.Installations.InstallationOrDefault(requestDetail.Request.InstallationID, ct);
                var request = await session.LogRequest
                (
                    requestKey: requestDetail.Request.RequestKey,
                    installation: installation,
                    path: requestDetail.Request.Path,
                    timeStarted: requestDetail.Request.TimeStarted,
                    timeEnded: requestDetail.Request.TimeEnded,
                    actualCount: requestDetail.Request.ActualCount,
                    sourceRequestKey: requestDetail.Request.SourceRequestKey,
                    requestData: requestDetail.Request.RequestData,
                    resultData: requestDetail.Request.ResultData,
                    ct: ct
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

    private async Task StartSession(StartSessionModel startSession, CancellationToken ct)
    {
        try
        {
            var user = await hubFactory.Users.UserOrAnon(new AppUserName(startSession.UserName), ct);
            var session = await hubFactory.Sessions.AddOrUpdate
            (
                startSession.SessionKey,
                user,
                startSession.TimeStarted,
                DateTimeOffset.MaxValue,
                startSession.RequesterKey,
                startSession.UserAgent,
                startSession.RemoteAddress,
                ct
            );
        }
        catch (Exception ex)
        {
            await HandleError(ex, ct);
        }
    }

    private async Task AuthenticateSession(AuthenticateSessionModel model, CancellationToken ct)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(model.SessionKey, clock.Now(), ct);
            var user = await hubFactory.Users.UserOrAnon(new AppUserName(model.UserName), ct);
            await session.Authenticate(user, ct);
        }
        catch (Exception ex)
        {
            await HandleError(ex, ct);
        }
    }

    private async Task StartRequest(StartRequestModel startRequest, CancellationToken ct)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(startRequest.SessionKey, clock.Now(), ct);
            var installation = await hubFactory.Installations.InstallationOrDefault(startRequest.InstallationID, ct);
            var request = await session.LogRequest
            (
                startRequest.RequestKey,
                installation,
                startRequest.Path,
                startRequest.TimeStarted,
                DateTimeOffset.MaxValue,
                startRequest.ActualCount,
                startRequest.SourceRequestKey,
                requestData: "",
                resultData: "",
                ct: ct
            );
        }
        catch (Exception ex)
        {
            await HandleError(ex, ct);
        }
    }

    private async Task LogEvent(LogEntryModelV1 model, CancellationToken ct)
    {
        try
        {
            await logEvent(model, ct);
        }
        catch (Exception ex)
        {
            await HandleError(ex, ct);
        }
    }

    private async Task EndRequest(EndRequestModel model, CancellationToken ct)
    {
        try
        {
            var request = await hubFactory.Requests.RequestOrPlaceHolder(model.RequestKey, clock.Now(), ct);
            await request.End(model.TimeEnded, ct);
        }
        catch (Exception ex)
        {
            await HandleError(ex, ct);
        }
    }

    private async Task EndSession(EndSessionModel model, CancellationToken ct)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(model.SessionKey, clock.Now(), ct);
            await session.End(model.TimeEnded, ct);
        }
        catch (Exception ex)
        {
            await HandleError(ex, ct);
        }
    }

    private Task HandleError(Exception ex, CancellationToken ct) =>
        logEvent
        (
            new LogEntryModelV1
            {
                Caption = "Error Updating Permanent Log",
                Message = ex.Message,
                Detail = ex.StackTrace ?? "",
                Severity = AppEventSeverity.Values.AppError,
                TimeOccurred = clock.Now()
            },
            ct
        );

    private async Task logEvent(LogEntryModelV1 model, CancellationToken ct)
    {
        var request = await hubFactory.Requests.RequestOrPlaceHolder(model.RequestKey, clock.Now(), ct);
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
