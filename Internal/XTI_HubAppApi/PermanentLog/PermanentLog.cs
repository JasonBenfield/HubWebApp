using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class PermanentLog
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public PermanentLog(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task LogBatch(ILogBatchModel model)
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
        foreach (var logEvent in model.LogEvents)
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

    public async Task StartSession(IStartSessionModel startSession)
    {
        try
        {
            var user = await appFactory.Users.UserOrAnon(new AppUserName(startSession.UserName));
            var session = await appFactory.Sessions.AddOrUpdate
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

    public async Task AuthenticateSession(IAuthenticateSessionModel model)
    {
        try
        {
            var session = await appFactory.Sessions.SessionOrPlaceHolder(model.SessionKey, clock.Now());
            var user = await appFactory.Users.UserOrAnon(new AppUserName(model.UserName));
            await session.Authenticate(user);
        }
        catch (Exception ex)
        {
            await handleError(ex);
        }
    }

    public async Task StartRequest(IStartRequestModel startRequest)
    {
        try
        {
            var session = await appFactory.Sessions.SessionOrPlaceHolder(startRequest.SessionKey, clock.Now());
            XtiPath path;
            try
            {
                path = XtiPath.Parse(startRequest.Path);
            }
            catch
            {
                path = new XtiPath(AppName.Unknown.Value);
            }
            if (string.IsNullOrWhiteSpace(path.Group))
            {
                path = path.WithGroup("Home");
            }
            if (string.IsNullOrWhiteSpace(path.Action))
            {
                path = path.WithAction("Index");
            }
            var appKey = new AppKey(path.App, AppType.Values.Value(startRequest.AppType));
            var app = await appFactory.Apps.App(appKey);
            var version = await app.VersionOrDefault(path.Version);
            var resourceGroup = await version.ResourceGroupOrDefault(path.Group);
            var resource = await resourceGroup.ResourceOrDefault(path.Action);
            var modCategory = await resourceGroup.ModCategory();
            var modifier = await modCategory.ModifierByModKeyOrDefault(path.Modifier);
            var request = await appFactory.Requests.AddOrUpdate
            (
                session,
                startRequest.RequestKey,
                resource,
                modifier,
                startRequest.Path,
                startRequest.TimeStarted
            );
        }
        catch (Exception ex)
        {
            await handleError(ex);
        }
    }

    public async Task LogEvent(ILogEventModel model)
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

    public async Task EndRequest(IEndRequestModel model)
    {
        try
        {
            var request = await appFactory.Requests.RequestOrPlaceHolder(model.RequestKey, clock.Now());
            await request.End(model.TimeEnded);
        }
        catch (Exception ex)
        {
            await handleError(ex);
        }
    }

    public async Task EndSession(IEndSessionModel model)
    {
        try
        {
            var session = await appFactory.Sessions.SessionOrPlaceHolder(model.SessionKey, clock.Now());
            await session.End(model.TimeEnded);
        }
        catch (Exception ex)
        {
            await handleError(ex);
        }
    }

    private Task handleError(Exception ex)
    {
        return logEvent(new LogEventModel
        {
            Caption = "Error Updating Permanent Log",
            Message = ex.Message,
            Detail = ex.StackTrace ?? "",
            Severity = AppEventSeverity.Values.AppError,
            TimeOccurred = clock.Now()
        });
    }

    private async Task logEvent(ILogEventModel model)
    {
        var request = await appFactory.Requests.RequestOrPlaceHolder(model.RequestKey, clock.Now());
        var severity = AppEventSeverity.Values.Value(model.Severity);
        await request.LogEvent
        (
            model.EventKey,
            severity,
            model.TimeOccurred,
            model.Caption,
            model.Message,
            model.Detail
        );
    }
}