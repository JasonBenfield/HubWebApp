using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall
{
    public sealed class InstalledAction : AppAction<InstalledRequest, EmptyActionResult>
    {
        private readonly AppFactory appFactory;

        public InstalledAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<EmptyActionResult> Execute(InstalledRequest model)
        {
            var installation = await appFactory.Installations.Installation(model.InstallationID);
            await installation.Installed();
            return new EmptyActionResult();
        }
    }
}
