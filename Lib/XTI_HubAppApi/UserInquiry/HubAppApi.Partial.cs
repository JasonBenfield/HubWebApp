using System;
using XTI_HubAppApi.UserInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public UserInquiryGroup UserInquiry { get; private set; }

        partial void userInquiry(IServiceProvider services)
        {
            UserInquiry = new UserInquiryGroup
            (
                source.AddGroup(nameof(UserInquiry), Access.WithAllowed(HubInfo.Roles.ViewUser)),
                new UserInquiryGroupFactory(services)
            );
        }
    }
}
