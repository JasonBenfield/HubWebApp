using XTI_HubWebAppApi.UserMaintenance;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private UserMaintenanceGroup? userMaintenance;

    public UserMaintenanceGroup UserMaintenance
    {
        get => userMaintenance ?? throw new ArgumentNullException(nameof(userMaintenance));
    }

    partial void createUserMaintenance(IServiceProvider sp)
    {
        userMaintenance = new UserMaintenanceGroup
        (
            source.AddGroup
            (
                nameof(UserMaintenance),
                Access.WithAllowed(HubInfo.Roles.EditUser)
            ),
            sp
        );
    }
}