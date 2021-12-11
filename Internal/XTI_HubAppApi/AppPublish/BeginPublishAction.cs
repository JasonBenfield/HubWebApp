using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppPublish
{
    public sealed class BeginPublishAction : AppAction<PublishVersionRequest, AppVersionModel>
    {
        private readonly AppFactory appFactory;

        public BeginPublishAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppVersionModel> Execute(PublishVersionRequest model)
        {
            var app = await appFactory.Apps.App(model.AppKey);
            var version = await app.Version(model.VersionKey);
            await version.Publishing();
            return version.ToModel();
        }
    }
}
