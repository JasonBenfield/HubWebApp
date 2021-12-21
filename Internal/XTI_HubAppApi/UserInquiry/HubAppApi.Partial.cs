using XTI_Hub;
using XTI_HubAppApi.UserInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        private UserInquiryGroup? userInquiry;

        public UserInquiryGroup UserInquiry
        {
            get => userInquiry ?? throw new ArgumentNullException(nameof(userInquiry));
        }

        partial void createUserInquiry(IServiceProvider services)
        {
            userInquiry = new UserInquiryGroup
            (
                source.AddGroup(nameof(UserInquiry), Access.WithAllowed(HubInfo.Roles.ViewUser)),
                services
            );
        }
    }
}
