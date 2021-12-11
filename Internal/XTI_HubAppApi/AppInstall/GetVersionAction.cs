using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall
{
    public sealed class GetVersionAction : AppAction<GetVersionRequest, AppVersionModel>
    {
        private readonly AppFactory appFactory;

        public GetVersionAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppVersionModel> Execute(GetVersionRequest model)
        {
            var app = await appFactory.Apps.App(model.AppKey);
            var version = await app.Version(model.VersionKey);
            return version.ToModel();
        }
    }
}
