using XTI_HubWebAppApiActions.UserGroups;

// Generated Code
namespace XTI_HubWebAppApi.UserGroups;
public sealed partial class UserGroupsGroup : AppApiGroupWrapper
{
    internal UserGroupsGroup(AppApiGroup source, UserGroupsGroupBuilder builder) : base(source)
    {
        AddUserGroupIfNotExists = builder.AddUserGroupIfNotExists.Build();
        GetUserDetailOrAnon = builder.GetUserDetailOrAnon.Build();
        GetUserGroupForUser = builder.GetUserGroupForUser.Build();
        GetUserGroups = builder.GetUserGroups.Build();
        Index = builder.Index.Build();
        UserQuery = builder.UserQuery.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists { get; }
    public AppApiAction<AppUserNameRequest, AppUserDetailModel> GetUserDetailOrAnon { get; }
    public AppApiAction<AppUserIDRequest, AppUserGroupModel> GetUserGroupForUser { get; }
    public AppApiAction<EmptyRequest, AppUserGroupModel[]> GetUserGroups { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<UserGroupKey, WebViewResult> UserQuery { get; }
}