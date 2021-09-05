using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetAppAction : AppAction<EmptyRequest, AppModel>
    {
        private readonly AppFromPath appFromPath;

        public GetAppAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppModel> Execute(EmptyRequest model)
        {
            var app = await appFromPath.Value();
            return app.ToAppModel();
        }
    }
}
