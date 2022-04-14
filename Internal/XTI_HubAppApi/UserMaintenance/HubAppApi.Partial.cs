using XTI_Hub;
using XTI_HubAppApi.UserMaintenance;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private UserMaintenanceGroup? userMaintenance;

    public UserMaintenanceGroup UserMaintenance
    {
        get => userMaintenance ?? throw new ArgumentNullException(nameof(userMaintenance));
    }

    partial void createUserMaintenance(IServiceProvider services)
    {
        userMaintenance = new UserMaintenanceGroup
        (
            source.AddGroup
            (
                nameof(UserMaintenance),
                Access.WithAllowed(HubInfo.Roles.EditUser)
            ),
            services
        );
    }
}