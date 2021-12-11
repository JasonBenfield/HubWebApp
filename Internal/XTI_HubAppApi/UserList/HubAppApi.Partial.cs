using System;
using XTI_Hub;
using XTI_HubAppApi.UserList;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        partial void userList(IServiceProvider services)
        {
            Users = new UserListGroup
            (
                source.AddGroup(nameof(Users), Access.WithAllowed(HubInfo.Roles.ViewUser)),
                services
            );
        }

        public UserListGroup Users { get; private set; }
    }
}
