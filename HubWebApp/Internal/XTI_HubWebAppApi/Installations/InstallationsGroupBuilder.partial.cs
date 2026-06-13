namespace XTI_HubWebAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.InstallationManager);
    }
}
