using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_Core;

namespace XTI_HubAppApi.PermanentLog
{
    public sealed class PermanentLogGroupActionFactory
    {
        private readonly IServiceProvider services;

        public PermanentLogGroupActionFactory(IServiceProvider services)
        {
            this.services = services;
        }

        public LogBatchAction CreateLogBatch()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new LogBatchAction(permanentLog);
        }

        public StartSessionAction CreateStartSession()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new StartSessionAction(permanentLog);
        }

        public StartRequestAction CreateStartRequest()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new StartRequestAction(permanentLog);
        }

        public AuthenticateSessionAction CreateAuthenticateSession()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new AuthenticateSessionAction(permanentLog);
        }

        public LogEventAction CreateLogEvent()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new LogEventAction(permanentLog);
        }

        public EndRequestAction CreateEndRequest()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new EndRequestAction(permanentLog);
        }

        public EndSessionAction CreateEndSession()
        {
            var permanentLog = services.GetService<PermanentLog>();
            return new EndSessionAction(permanentLog);
        }

        public EndExpiredSessionsAction CreateEndExpiredSessions()
        {
            var appFactory = services.GetService<AppFactory>();
            var clock = services.GetService<Clock>();
            return new EndExpiredSessionsAction(appFactory, clock);
        }

    }
}
