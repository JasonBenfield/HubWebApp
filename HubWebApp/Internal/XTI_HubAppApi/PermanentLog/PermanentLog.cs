using XTI_Core;
using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class PermanentLog
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public PermanentLog(HubFactory appFactory, IClock clock)
    {
        this.hubFactory = appFactory;
        this.clock = clock;
    }

    public async Task LogBatch(LogBatchModel model)
    {
        foreach (var startSession in model.StartSessions)
        {
            await StartSession(startSession);
        }
        foreach (var authSession in model.AuthenticateSessions)
        {
            await AuthenticateSession(authSession);
        }
        foreach (var startRequest in model.StartRequests)
        {
            await StartRequest(startRequest);
        }
        foreach (var logEvent in model.LogEntries)
        {
            await LogEvent(logEvent);
        }
        foreach (var endRequest in model.EndRequests)
        {
            await EndRequest(endRequest);
        }
        foreach (var endSession in model.EndSessions)
        {
            await EndSession(endSession);
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
                startSession.RequesterKey,
                startSession.UserAgent,
                startSession.RemoteAddress
            );
        }
        catch (Exception ex)
        {
            await handleError(ex);
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
            await handleError(ex);
        }
    }

    private async Task StartRequest(StartRequestModel startRequest)
    {
        try
        {
            var session = await hubFactory.Sessions.SessionOrPlaceHolder(startRequest.SessionKey, clock.Now());
            var installation = await hubFactory.Installations.Installation(startRequest.InstallationID);
            var request = await session.LogRequest
            (
                startRequest.RequestKey,
                installation,
                startRequest.Path,
                startRequest.TimeStarted,
                startRequest.ActualCount
            );
        }
        catch (Exception ex)
        {
            await handleError(ex);
        }
    }

    private async Task LogEvent(LogEntryModel model)
    {
        try
        {
            await logEvent(model);
        }
        catch (Exception ex)
        {
            await handleError(ex);
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
            await handleError(ex);
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
            await handleError(ex);
        }
    }

    private Task handleError(Exception ex)
    {
        return logEvent(new LogEntryModel
        {
            Caption = "Error Updating Permanent Log",
            Message = ex.Message,
            Detail = ex.StackTrace ?? "",
            Severity = AppEventSeverity.Values.AppError,
            TimeOccurred = clock.Now()
        });
    }

    private async Task logEvent(LogEntryModel model)
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
            model.ActualCount
        );
    }
}