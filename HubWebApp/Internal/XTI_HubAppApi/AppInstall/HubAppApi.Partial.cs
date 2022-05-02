using XTI_HubAppApi.AppInstall;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private AppInstallGroup? install;

    public AppInstallGroup Install { get => install ?? throw new ArgumentNullException(nameof(install)); }

    partial void createInstall(IServiceProvider sp)
    {
        install = new AppInstallGroup
        (
            source.AddGroup(nameof(Install)),
            sp
        );
    }
}