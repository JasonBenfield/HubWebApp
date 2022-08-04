using XTI_HubWebAppApi.AppInstall;

namespace XTI_HubWebAppApi;

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