using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient
{
    public sealed class PermanentLogClient : IPermanentLogClient
    {
        private readonly HubAppClient client;

        public PermanentLogClient(HubAppClient client)
        {
            this.client = client;
        }

        public Task StartSession(IStartSessionModel model)
            => client.PermanentLog.StartSession(new StartSessionModel(model));

        public Task StartRequest(IStartRequestModel model)
            => client.PermanentLog.StartRequest(new StartRequestModel(model));
        public Task AuthenticateSession(IAuthenticateSessionModel model)
            => client.PermanentLog.AuthenticateSession(new AuthenticateSessionModel(model));

        public Task LogEvent(ILogEventModel model)
            => client.PermanentLog.LogEvent(new LogEventModel(model));

        public Task EndRequest(IEndRequestModel model)
            => client.PermanentLog.EndRequest(new EndRequestModel(model));

        public Task EndSession(IEndSessionModel model)
            => client.PermanentLog.EndSession(new EndSessionModel(model));

        public Task LogBatch(ILogBatchModel model)
            => client.PermanentLog.LogBatch(new LogBatchModel(model));
    }
}
