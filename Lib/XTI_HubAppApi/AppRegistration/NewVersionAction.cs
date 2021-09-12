using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppRegistration
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
            var app = await appFactory.Apps().App(model.AppKey);
            if (!app.Key().Name.Equals(model.AppKey.Name))
            {
                app = await appFactory.Apps().Add(model.AppKey, model.AppKey.Name.DisplayText, clock.Now());
            }
            var currentVersion = await app.CurrentVersion();
            if (!currentVersion.IsCurrent())
            {
                currentVersion = await app.StartNewMajorVersion(clock.Now());
                await currentVersion.Publishing();
                await currentVersion.Published();
            }
            AppVersion version;
            if (model.VersionType.Equals(AppVersionType.Values.Major))
            {
                version = await app.StartNewMajorVersion(clock.Now());
            }
            else if (model.VersionType.Equals(AppVersionType.Values.Minor))
            {
                version = await app.StartNewMinorVersion(clock.Now());
            }
            else if (model.VersionType.Equals(AppVersionType.Values.Patch))
            {
                version = await app.StartNewPatch(clock.Now());
            }
            else
            {
                version = null;
            }
            return version.ToModel();
        }
    }
}
