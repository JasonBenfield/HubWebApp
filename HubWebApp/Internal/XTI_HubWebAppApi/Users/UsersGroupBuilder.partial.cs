namespace XTI_HubWebAppApi.Users;

partial class UsersGroupBuilder
{
    partial void Configure()
    {
        source.WithModCategory(HubInfo.ModCategories.UserGroups);
    }
}
