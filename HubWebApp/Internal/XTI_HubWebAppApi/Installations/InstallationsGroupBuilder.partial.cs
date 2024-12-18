namespace XTI_HubWebAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        BeginDelete.WithAllowed(HubInfo.Roles.InstallationManager);
        Deleted.WithAllowed(HubInfo.Roles.InstallationManager);
        GetPendingDeletes.WithAllowed(HubInfo.Roles.InstallationManager);
    }
}
