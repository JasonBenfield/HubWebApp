using XTI_HubAppApi.AppUserMaintenance;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private AppUserMaintenanceGroup? appUserMaintenance;

    public AppUserMaintenanceGroup AppUserMaintenance
    {
        get => appUserMaintenance ?? throw new ArgumentNullException(nameof(appUserMaintenance));
    }

    partial void createAppUserMaintenance(IServiceProvider sp)
    {
        appUserMaintenance = new AppUserMaintenanceGroup
        (
            source.AddGroup
            (
                nameof(AppUserMaintenance),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.EditUser)
            ),
            sp
        );
    }
}