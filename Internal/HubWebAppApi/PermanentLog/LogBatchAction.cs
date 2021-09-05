using System.Threading.Tasks;
using XTI_App.Api;
using XTI_TempLog;

namespace HubWebAppApi.PermanentLog
{
    public sealed class LogBatchAction : AppAction<LogBatchModel, EmptyActionResult>
    {
        private readonly PermanentLog permanentLog;

        public LogBatchAction(PermanentLog permanentLog)
        {
            this.permanentLog = permanentLog;
        }

        public async Task<EmptyActionResult> Execute(LogBatchModel model)
        {
            await permanentLog.LogBatch(model);
            return new EmptyActionResult();
        }
    }
}
