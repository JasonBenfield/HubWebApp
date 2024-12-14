namespace XTI_HubWebAppApi.ResourceGroupInquiry;

partial class ResourceGroupInquiryGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.Apps)
            .ResetAccessWithAllowed(HubInfo.Roles.AppViewerRoles);
    }
}
