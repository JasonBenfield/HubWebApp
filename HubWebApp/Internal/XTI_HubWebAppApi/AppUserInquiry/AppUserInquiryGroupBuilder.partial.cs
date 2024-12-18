namespace XTI_HubWebAppApi.AppUserInquiry;

partial class AppUserInquiryGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.UserGroups)
            .ResetAccessWithAllowed(HubInfo.Roles.UserViewerRoles);
    }
}
