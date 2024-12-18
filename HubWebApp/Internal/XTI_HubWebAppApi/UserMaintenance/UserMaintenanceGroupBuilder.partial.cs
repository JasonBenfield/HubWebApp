namespace XTI_HubWebAppApi.UserMaintenance;

partial class UserMaintenanceGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.UserGroups)
            .WithAllowed(HubInfo.Roles.EditUser);
    }
}
