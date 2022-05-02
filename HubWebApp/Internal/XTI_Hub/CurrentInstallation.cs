using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class CurrentInstallation : Installation
{
    internal CurrentInstallation(HubFactory appFactory, InstallationEntity entity)
        : base(appFactory, entity)
    {
    }

    internal Task Start(AppVersion appVersion, string domain) => StartCurrent(appVersion, domain);
}