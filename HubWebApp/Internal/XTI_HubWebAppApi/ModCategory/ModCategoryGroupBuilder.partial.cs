namespace XTI_HubWebAppApi.ModCategory;

partial class ModCategoryGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.Apps)
            .ResetAccessWithAllowed(HubInfo.Roles.AppViewerRoles);
    }
}
