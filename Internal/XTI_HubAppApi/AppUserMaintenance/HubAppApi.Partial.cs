using System;
using XTI_Hub;
using XTI_HubAppApi.AppUserMaintenance;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public AppUserMaintenanceGroup AppUserMaintenance { get; private set; }

        partial void appUserMaintenance(IServiceProvider services)
        {
            AppUserMaintenance = new AppUserMaintenanceGroup
            (
                source.AddGroup
                (
                    nameof(AppUserMaintenance),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.EditUser)
                ),
                services
            );
        }
    }
}
