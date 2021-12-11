using System.Threading.Tasks;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;

namespace LocalInstallApp
{
    internal sealed class HubClientInstallationService : InstallationService
    {
        private readonly HubAppClient hubClient;

        public HubClientInstallationService(HubAppClient hubClient, XTI_App.Abstractions.AppKey appKey, XTI_App.Abstractions.AppVersionKey installVersionKey, string machineName)
            : base(appKey, installVersionKey, machineName)
        {
            this.hubClient = hubClient;
        }

        protected override Task<int> _BeginCurrentInstall(XTI_App.Abstractions.AppKey appKey, XTI_App.Abstractions.AppVersionKey installVersionKey, string machineName)
        {
            var request = new BeginInstallationRequest
            {
                AppKey = new AppKey
                {
                    Name = appKey.Name,
                    Type = AppType.Values.Value(appKey.Type.Value)
                },
                VersionKey = installVersionKey.Value,
                QualifiedMachineName = machineName
            };
            return hubClient.Install.BeginCurrentInstallation("", request);
        }

        protected override Task<int> _BeginVersionInstall(XTI_App.Abstractions.AppKey appKey, XTI_App.Abstractions.AppVersionKey versionKey, string machineName)
        {
            var request = new BeginInstallationRequest
            {
                AppKey = new AppKey
                {
                    Name = appKey.Name,
                    Type = AppType.Values.Value(appKey.Type.Value)
                },
                VersionKey = versionKey.Value,
                QualifiedMachineName = machineName
            };
            return hubClient.Install.BeginVersionInstallation("", request);
        }

        protected override Task _Installed(int installationID)
            => hubClient.Install.Installed("", new InstalledRequest { InstallationID = installationID });
    }
}
