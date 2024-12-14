namespace XTI_HubWebAppApi.ResourceInquiry;

partial class ResourceInquiryGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.Apps)
            .ResetAccessWithAllowed(HubInfo.Roles.AppViewerRoles);
    }
}
