using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class VersionInstallation : Installation
{
    internal VersionInstallation(HubFactory appFactory, InstallationEntity entity)
        : base(appFactory, entity)
    {
    }

    public Task Start(string domain) => StartVersion(domain);
}