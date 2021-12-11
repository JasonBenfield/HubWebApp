using System.Threading.Tasks;
using XTI_App.Api;
using XTI_TempLog;

namespace XTI_HubAppApi.PermanentLog
{
    public sealed class StartRequestAction : AppAction<StartRequestModel, EmptyActionResult>
    {
        private readonly PermanentLog permanentLog;

        public StartRequestAction(PermanentLog permanentLog)
        {
            this.permanentLog = permanentLog;
        }

        public async Task<EmptyActionResult> Execute(StartRequestModel model)
        {
            await permanentLog.StartRequest(model);
            return new EmptyActionResult();
        }

    }
}
