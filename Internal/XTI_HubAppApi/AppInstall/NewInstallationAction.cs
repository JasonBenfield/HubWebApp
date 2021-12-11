using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall
{

    public sealed class NewInstallationAction : AppAction<NewInstallationRequest, NewInstallationResult>
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;

        public NewInstallationAction(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task<NewInstallationResult> Execute(NewInstallationRequest model)
        {
            var version = await appFactory.Versions.Version(model.VersionID);
            var installLocation = await appFactory.InstallLocations.TryAdd(model.QualifiedMachineName);
            var now = clock.Now();
            var app = await version.App();
            Installation currentInstallation;
            var hasCurrentInstallation = await installLocation.HasCurrentInstallation(app);
            if (hasCurrentInstallation)
            {
                currentInstallation = await installLocation.CurrentInstallation(app);
            }
            else
            {
                currentInstallation = await installLocation.NewCurrentInstallation(version, now);
            }
            Installation versionInstallation;
            var hasVersionInstallation = await installLocation.HasVersionInstallation(version);
            if (hasVersionInstallation)
            {
                versionInstallation = await installLocation.VersionInstallation(version);
                await versionInstallation.InstallPending();
            }
            else
            {
                versionInstallation = await installLocation.NewVersionInstallation(version, now);
            }
            return new NewInstallationResult(currentInstallation.ID.Value, versionInstallation.ID.Value);
        }
    }
}
