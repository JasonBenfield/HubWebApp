using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppRegistration
{
    public sealed class BeginPublishAction : AppAction<GetVersionRequest, AppVersionModel>
    {
        private readonly AppFactory appFactory;

        public BeginPublishAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppVersionModel> Execute(GetVersionRequest model)
        {
            var app = await appFactory.Apps().App(model.AppKey);
            var version = await app.Version(model.VersionKey);
            if (!version.IsNew() && !version.IsPublishing())
            {
                throw new AppException($"Unable to begin publishing version '{model.VersionKey.DisplayText}' when it's status is not 'New' or 'Publishing'");
            }
            await version.Publishing();
            return version.ToModel();
        }
    }
}
