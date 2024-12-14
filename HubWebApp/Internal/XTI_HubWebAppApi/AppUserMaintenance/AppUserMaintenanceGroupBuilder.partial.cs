namespace XTI_HubWebAppApi.AppUserMaintenance;

partial class AppUserMaintenanceGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.UserGroups)
            .WithAllowed(HubInfo.Roles.EditUser);
    }
}
