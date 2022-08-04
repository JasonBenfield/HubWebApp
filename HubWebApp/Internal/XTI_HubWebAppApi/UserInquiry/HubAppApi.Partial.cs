using XTI_HubWebAppApi.UserInquiry;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private UserInquiryGroup? userInquiry;

    public UserInquiryGroup UserInquiry
    {
        get => userInquiry ?? throw new ArgumentNullException(nameof(userInquiry));
    }

    partial void createUserInquiry(IServiceProvider sp)
    {
        userInquiry = new UserInquiryGroup
        (
            source.AddGroup
            (
                nameof(UserInquiry), 
                HubInfo.ModCategories.UserGroups,
                Access.WithAllowed(HubInfo.Roles.ViewUser)
            ),
            sp
        );
    }
}