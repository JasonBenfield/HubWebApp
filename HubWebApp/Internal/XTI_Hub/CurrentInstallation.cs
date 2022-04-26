using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class CurrentInstallation : Installation
{
    internal CurrentInstallation(AppFactory appFactory, InstallationEntity entity)
        : base(appFactory, entity)
    {
    }

    internal Task Start(AppVersion appVersion) => StartCurrent(appVersion);
}