namespace XTI_HubWebAppApi.InstallTemplates;

partial class InstallTemplatesGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.InstallationManager);
    }
}
