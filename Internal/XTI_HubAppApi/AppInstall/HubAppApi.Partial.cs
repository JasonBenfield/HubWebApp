using System;
using XTI_Hub;
using XTI_HubAppApi.AppInstall;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public InstallGroup Install { get; private set; }

        partial void install(IServiceProvider services)
        {
            Install = new InstallGroup
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
}
