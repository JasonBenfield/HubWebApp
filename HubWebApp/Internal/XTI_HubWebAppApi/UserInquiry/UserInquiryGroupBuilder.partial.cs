namespace XTI_HubWebAppApi.UserInquiry;

partial class UserInquiryGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.UserGroups)
            .WithAllowed(HubInfo.Roles.ViewUser);
    }
}
