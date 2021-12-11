using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions
{
    public abstract class InstallationService
    {
        private readonly AppKey appKey;
        private readonly AppVersionKey versionKey;
        private readonly string machineName;

        protected InstallationService(AppKey appKey, AppVersionKey versionKey, string machineName)
        {
            this.appKey = appKey;
            this.versionKey = versionKey;
            this.machineName = machineName;
        }

        public Task<int> BeginCurrentInstall(AppVersionKey installVersionKey)
            => _BeginCurrentInstall(appKey, installVersionKey, machineName);

        protected abstract Task<int> _BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName);

        public Task<int> BeginVersionInstall() => _BeginCurrentInstall(appKey, versionKey, machineName);

        protected abstract Task<int> _BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName);

        public Task Installed(int installationID) => _Installed(installationID);

        protected abstract Task _Installed(int installationID);
    }
}
