using XTI_HubWebAppApiActions.UserGroups;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class UserGroupsGroupExtensions
{
    internal static void AddUserGroupsServices(this IServiceCollection services)
    {
        services.AddScoped<AddUserGroupIfNotExistsAction>();
        services.AddScoped<AddUserGroupIfNotExistsValidation>();
        services.AddScoped<GetUserDetailOrAnonAction>();
        services.AddScoped<GetUserGroupForUserAction>();
        services.AddScoped<GetUserGroupsAction>();
        services.AddScoped<IndexPage>();
        services.AddScoped<UserQueryPage>();
    }
}