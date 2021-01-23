using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetCurrentVersionAction : AppAction<EmptyRequest, AppVersionModel>
    {
        private readonly AppFromPath appFromPath;

        public GetCurrentVersionAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppVersionModel> Execute(EmptyRequest model)
        {
            var app = await appFromPath.Value();
            var currentVersion = await app.CurrentVersion();
            return currentVersion.ToModel();
        }
    }
}
