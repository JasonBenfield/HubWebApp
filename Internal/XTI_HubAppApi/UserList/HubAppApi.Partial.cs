using XTI_Hub;
using XTI_HubAppApi.UserList;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private UserListGroup? users;

    public UserListGroup Users
    {
        get => users ?? throw new ArgumentNullException(nameof(users));
    }

    partial void createUserList(IServiceProvider services)
    {
        users = new UserListGroup
        (
            source.AddGroup(nameof(Users), Access.WithAllowed(HubInfo.Roles.ViewUser)),
            services
        );
    }
}