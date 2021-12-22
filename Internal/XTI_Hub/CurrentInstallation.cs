using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class CurrentInstallation : Installation
{
    internal CurrentInstallation(AppFactory appFactory, InstallationEntity entity)
        : base(appFactory, entity)
    {
    }

    public Task Start(AppVersion appVersion) => StartCurrent(appVersion);
}