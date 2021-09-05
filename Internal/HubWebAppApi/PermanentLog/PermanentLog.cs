using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace HubWebAppApi.PermanentLog
{
    public sealed class PermanentLog
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;

        public PermanentLog(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task LogBatch(ILogBatchModel model)
        {
            if (model.StartSessions != null)
            {
                foreach (var startSession in model.StartSessions)
                {
                    await StartSession(startSession);
                }
            }
            if (model.AuthenticateSessions != null)
            {
                foreach (var authSession in model.AuthenticateSessions)
                {
                    await AuthenticateSession(authSession);
                }
            }
            if (model.StartRequests != null)
            {
                foreach (var startRequest in model.StartRequests)
                {
                    await StartRequest(startRequest);
                }
            }
            if (model.LogEvents != null)
            {
                foreach (var logEvent in model.LogEvents)
                {
                    await LogEvent(logEvent);
                }
            }
            if (model.EndRequests != null)
            {
                foreach (var endRequest in model.EndRequests)
                {
                    await EndRequest(endRequest);
                }
            }
            if (model.EndSessions != null)
            {
                foreach (var endSession in model.EndSessions)
                {
                    await EndSession(endSession);
                }
            }
        }

        public async Task StartSession(IStartSessionModel startSession)
        {
            try
            {
                var user = await retrieveUser(startSession.UserName);
                var session = await appFactory.Sessions().Session(startSession.SessionKey);
                if (session.ID.IsValid())
                {
                    await session.Edit
                    (
                        user,
                        startSession.TimeStarted,
                        startSession.RequesterKey,
                        startSession.UserAgent,
                        startSession.RemoteAddress
                    );
                }
                else
                {
                    await appFactory.Sessions().Create
                    (
                        startSession.SessionKey,
                        user,
                        startSession.TimeStarted,
                        startSession.RequesterKey,
                        startSession.UserAgent,
                        startSession.RemoteAddress
                    );
                }
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
                var session = await appFactory.Sessions().Session(model.SessionKey);
                if (session.ID.IsNotValid())
                {
                    session = await startPlaceholderSession(model.SessionKey, new GeneratedKey().Value());
                }
                var user = await retrieveUser(model.UserName);
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
                var session = await appFactory.Sessions().Session(startRequest.SessionKey);
                if (session.ID.IsNotValid())
                {
                    session = await startPlaceholderSession(startRequest.SessionKey, new GeneratedKey().Value());
                }
                XtiPath path;
                try
                {
                    path = XtiPath.Parse(startRequest.Path);
                }
                catch
                {
                    path = new XtiPath(AppName.Unknown);
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
                var app = await appFactory.Apps().App(appKey);
                var version = await app.Version(path.Version);
                var resourceGroup = await version.ResourceGroup(path.Group);
                var resource = await resourceGroup.Resource(path.Action);
                var modCategory = await resourceGroup.ModCategory();
                var modifier = await modCategory.Modifier(path.Modifier);
                var request = await appFactory.Requests().Request(startRequest.RequestKey);
                if (request.ID.IsValid())
                {
                    await request.Edit
                    (
                        session,
                        resource,
                        modifier,
                        startRequest.Path,
                        startRequest.TimeStarted
                    );
                }
                else
                {
                    await session.LogRequest
                    (
                        startRequest.RequestKey,
                        resource,
                        modifier,
                        startRequest.Path,
                        startRequest.TimeStarted
                    );
                }
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
                var request = await appFactory.Requests().Request(model.RequestKey);
                if (request.ID.IsNotValid())
                {
                    request = await startPlaceholderRequest(model.RequestKey);
                }
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
                var session = await appFactory.Sessions().Session(model.SessionKey);
                if (session.ID.IsNotValid())
                {
                    session = await startPlaceholderSession(model.SessionKey, new GeneratedKey().Value());
                }
                await session.End(model.TimeEnded);
            }
            catch (Exception ex)
            {
                await handleError(ex);
            }
        }

        private async Task<AppRequest> startPlaceholderRequest(string requestKey)
        {
            var session = await defaultSession();
            var app = await appFactory.Apps().App(AppKey.Unknown);
            var version = await app.CurrentVersion();
            var resourceGroup = await version.ResourceGroup(ResourceGroupName.Unknown);
            var resource = await resourceGroup.Resource(ResourceName.Unknown);
            var modCategory = await app.ModCategory(ModifierCategoryName.Default);
            var modifier = await modCategory.Modifier(ModifierKey.Default);
            var request = await session.LogRequest
            (
                string.IsNullOrWhiteSpace(requestKey) ? new GeneratedKey().Value() : requestKey,
                resource,
                modifier,
                "",
                clock.Now()
            );
            return request;
        }

        private async Task<AppUser> retrieveUser(string userName)
        {
            var user = await appFactory.Users().User(new AppUserName(userName));
            if (!user.Exists())
            {
                user = await appFactory.Users().User(AppUserName.Anon);
            }
            return user;
        }

        private async Task<AppSession> defaultSession()
        {
            var session = await appFactory.Sessions().DefaultSession(clock.Now());
            if (session.ID.IsNotValid() || session.HasEnded())
            {
                session = await startPlaceholderSession(new GeneratedKey().Value(), "default");
            }
            return session;
        }

        private async Task<AppSession> startPlaceholderSession(string sessionKey, string requesterKey)
        {
            var user = await appFactory.Users().User(AppUserName.Anon);
            var session = await appFactory.Sessions().Create
            (
                sessionKey,
                user,
                clock.Now(),
                requesterKey,
                "",
                ""
            );
            return session;
        }

        private Task handleError(Exception ex)
        {
            return logEvent(new LogEventModel
            {
                Caption = "Error Updating Permanent Log",
                Message = ex.Message,
                Detail = ex.StackTrace,
                Severity = AppEventSeverity.Values.AppError,
                TimeOccurred = clock.Now()
            });
        }

        private async Task logEvent(ILogEventModel model)
        {
            var request = await appFactory.Requests().Request(model.RequestKey);
            if (request.ID.IsNotValid())
            {
                request = await startPlaceholderRequest(model.RequestKey);
            }
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
}
