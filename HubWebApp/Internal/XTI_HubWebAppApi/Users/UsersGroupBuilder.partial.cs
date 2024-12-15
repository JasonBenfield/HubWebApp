namespace XTI_HubWebAppApi.Users;

partial class UsersGroupBuilder
{
    partial void Configure()
    {
        source.WithModCategory(HubInfo.ModCategories.UserGroups);
        Index.WithAllowed(HubInfo.Roles.ViewUser);
        GetUserGroup.WithAllowed(HubInfo.Roles.ViewUser);
        GetUsers.WithAllowed(HubInfo.Roles.ViewUser);
        AddOrUpdateUser.WithAllowed(HubInfo.Roles.AddUser);
        AddUser.WithAllowed(HubInfo.Roles.AddUser);
    }
}
