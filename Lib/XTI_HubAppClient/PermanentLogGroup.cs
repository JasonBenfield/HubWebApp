// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class PermanentLogGroup : AppClientGroup
    {
        public PermanentLogGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "PermanentLog")
        {
        }

        public Task<EmptyActionResult> LogBatch(LogBatchModel model) => Post<EmptyActionResult, LogBatchModel>("LogBatch", "", model);
        public Task<EmptyActionResult> StartSession(StartSessionModel model) => Post<EmptyActionResult, StartSessionModel>("StartSession", "", model);
        public Task<EmptyActionResult> StartRequest(StartRequestModel model) => Post<EmptyActionResult, StartRequestModel>("StartRequest", "", model);
        public Task<EmptyActionResult> EndRequest(EndRequestModel model) => Post<EmptyActionResult, EndRequestModel>("EndRequest", "", model);
        public Task<EmptyActionResult> EndSession(EndSessionModel model) => Post<EmptyActionResult, EndSessionModel>("EndSession", "", model);
        public Task<EmptyActionResult> LogEvent(LogEventModel model) => Post<EmptyActionResult, LogEventModel>("LogEvent", "", model);
        public Task<EmptyActionResult> AuthenticateSession(AuthenticateSessionModel model) => Post<EmptyActionResult, AuthenticateSessionModel>("AuthenticateSession", "", model);
        public Task<EmptyActionResult> EndExpiredSessions() => Post<EmptyActionResult, EmptyRequest>("EndExpiredSessions", "", new EmptyRequest());
    }
}