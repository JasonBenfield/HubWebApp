using System.Threading.Tasks;
using XTI_App.Api;
using XTI_TempLog;

namespace HubWebAppApi.PermanentLog
{
    public sealed class StartSessionAction : AppAction<StartSessionModel, EmptyActionResult>
    {
        private readonly PermanentLog permanentLog;

        public StartSessionAction(PermanentLog permanentLog)
        {
            this.permanentLog = permanentLog;
        }

        public async Task<EmptyActionResult> Execute(StartSessionModel model)
        {
            await permanentLog.StartSession(model);
            return new EmptyActionResult();
        }
    }
}
