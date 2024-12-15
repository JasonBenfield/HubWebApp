using XTI_HubWebAppApiActions.UserGroups;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.UserGroups;
public sealed partial class UserGroupsGroupBuilder
{
    private readonly AppApiGroup source;
    internal UserGroupsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AddUserGroupIfNotExists = source.AddAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>("AddUserGroupIfNotExists").WithExecution<AddUserGroupIfNotExistsAction>().WithValidation<AddUserGroupIfNotExistsValidation>();
        GetUserDetailOrAnon = source.AddAction<AppUserNameRequest, AppUserDetailModel>("GetUserDetailOrAnon").WithExecution<GetUserDetailOrAnonAction>();
        GetUserGroupForUser = source.AddAction<AppUserIDRequest, AppUserGroupModel>("GetUserGroupForUser").WithExecution<GetUserGroupForUserAction>();
        GetUserGroups = source.AddAction<EmptyRequest, AppUserGroupModel[]>("GetUserGroups").WithExecution<GetUserGroupsAction>();
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexPage>();
        UserQuery = source.AddAction<UserGroupKey, WebViewResult>("UserQuery").WithExecution<UserQueryPage>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists { get; }
    public AppApiActionBuilder<AppUserNameRequest, AppUserDetailModel> GetUserDetailOrAnon { get; }
    public AppApiActionBuilder<AppUserIDRequest, AppUserGroupModel> GetUserGroupForUser { get; }
    public AppApiActionBuilder<EmptyRequest, AppUserGroupModel[]> GetUserGroups { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<UserGroupKey, WebViewResult> UserQuery { get; }

    public UserGroupsGroup Build() => new UserGroupsGroup(source, this);
}