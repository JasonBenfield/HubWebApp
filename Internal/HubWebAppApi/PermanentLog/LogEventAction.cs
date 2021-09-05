using System.Threading.Tasks;
using XTI_App.Api;
using XTI_TempLog;

namespace HubWebAppApi.PermanentLog
{
    public sealed class LogEventAction : AppAction<LogEventModel, EmptyActionResult>
    {
        private readonly PermanentLog permanentLog;

        public LogEventAction(PermanentLog permanentLog)
        {
            this.permanentLog = permanentLog;
        }

        public async Task<EmptyActionResult> Execute(LogEventModel model)
        {
            await permanentLog.LogEvent(model);
            return new EmptyActionResult();
        }
    }
}
