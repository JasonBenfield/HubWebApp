using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class VersionInstallation : Installation
{
    internal VersionInstallation(AppFactory appFactory, InstallationEntity entity)
        : base(appFactory, entity)
    {
    }

    public Task Start() => StartVersion();
}