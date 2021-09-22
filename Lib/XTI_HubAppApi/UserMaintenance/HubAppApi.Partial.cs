using System;
using XTI_HubAppApi.Users;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public UserMaintenanceGroup UserMaintenance { get; private set; }

        partial void userMaintenance(IServiceProvider services)
        {
            UserMaintenance = new UserMaintenanceGroup
            (
                source.AddGroup
                (
                    nameof(UserMaintenance),
                    Access.WithAllowed(HubInfo.Roles.EditUser)
                ),
                new UserMaintenanceGroupFactory(services)
            );
        }
    }
}
