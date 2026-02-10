namespace XTI_HubWebAppApi.Command;

partial class CommandGroupBuilder
{
    partial void Configure()
    {
        source.WithModCategory(HubInfo.ModCategories.Apps);
        source.WithAllowed(HubInfo.Roles.InstallationManager);
    }
}
