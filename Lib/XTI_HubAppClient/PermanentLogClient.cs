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

        public Task StartSession(StartSessionModel model)
            => client.PermanentLog.StartSession(model);

        public Task StartRequest(StartRequestModel model)
            => client.PermanentLog.StartRequest(model);
        public Task AuthenticateSession(AuthenticateSessionModel model)
            => client.PermanentLog.AuthenticateSession(model);

        public Task LogEvent(LogEventModel model)
            => client.PermanentLog.LogEvent(model);

        public Task EndRequest(EndRequestModel model)
            => client.PermanentLog.EndRequest(model);

        public Task EndSession(EndSessionModel model)
            => client.PermanentLog.EndSession(model);

        public Task LogBatch(LogBatchModel model)
            => client.PermanentLog.LogBatch(model);
    }
}
