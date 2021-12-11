using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppPublish
{
    public sealed class NewVersionAction : AppAction<NewVersionRequest, AppVersionModel>
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;

        public NewVersionAction(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task<AppVersionModel> Execute(NewVersionRequest model)
        {
            var version = await appFactory.Apps.StartNewVersion
            (
                model.AppKey, 
                model.VersionType, 
                clock.Now()
            );
            return version.ToModel();
        }
    }
}
