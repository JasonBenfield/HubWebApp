namespace XTI_HubWebAppApi.Version;

partial class VersionGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.Apps)
            .ResetAccessWithAllowed(HubInfo.Roles.AppViewerRoles);
    }
}
