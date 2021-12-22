using XTI_Hub;
using XTI_HubAppApi.AppInstall;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private InstallGroup? install;

    public InstallGroup Install { get=>install ?? throw new ArgumentNullException(nameof(install)); }

    partial void createInstall(IServiceProvider services)
    {
        install = new InstallGroup
        (
            source.AddGroup
            (
                nameof(Install),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.Installer)
            ),
            services
        );
    }
}