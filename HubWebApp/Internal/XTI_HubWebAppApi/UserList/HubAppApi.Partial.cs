using XTI_HubWebAppApi.UserList;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private UserListGroup? users;

    public UserListGroup Users
    {
        get => users ?? throw new ArgumentNullException(nameof(users));
    }

    partial void createUserList(IServiceProvider sp)
    {
        users = new UserListGroup
        (
            source.AddGroup
            (
                nameof(Users), 
                HubInfo.ModCategories.UserGroups
            ),
            sp
        );
    }
}