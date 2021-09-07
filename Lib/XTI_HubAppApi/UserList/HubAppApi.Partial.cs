using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new UserListGroupFactory(services)
            );
        }

        public UserListGroup Users { get; private set; }
    }
}
