using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Hub
{
    public sealed class DbInstallationService : InstallationService
    {
        private readonly AppFactory appFactory;

        public DbInstallationService(AppFactory appFactory, AppKey appKey, AppVersionKey versionKey, string machineName)
            : base(appKey, versionKey, machineName)
        {
            this.appFactory = appFactory;
        }

        protected override async Task<int> _BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName)
        {
            var beginInstallProcess = new InstallationProcess(appFactory);
            var installationID = await beginInstallProcess.BeginCurrentInstall(appKey, installVersionKey, machineName);
            return installationID;
        }

        protected override async Task<int> _BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName)
        {
            var beginInstallProcess = new InstallationProcess(appFactory);
            var installationID = await beginInstallProcess.BeginVersionInstall(appKey, versionKey, machineName);
            return installationID;
        }

        protected override async Task _Installed(int installationID)
        {
            var installation = await appFactory.Installations.Installation(installationID);
            await installation.Installed();
        }
    }
}
