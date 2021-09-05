using System.Threading.Tasks;
using XTI_App.Api;
using XTI_TempLog;

namespace HubWebAppApi.PermanentLog
{
    public sealed class EndRequestAction : AppAction<EndRequestModel, EmptyActionResult>
    {
        private readonly PermanentLog permanentLog;

        public EndRequestAction(PermanentLog permanentLog)
        {
            this.permanentLog = permanentLog;
        }

        public async Task<EmptyActionResult> Execute(EndRequestModel model)
        {
            await permanentLog.EndRequest(model);
            return new EmptyActionResult();
        }
    }
}
