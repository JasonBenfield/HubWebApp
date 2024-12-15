using XTI_HubWebAppApiActions.AppUserInquiry;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.AppUserInquiry;
public sealed partial class AppUserInquiryGroupBuilder
{
    private readonly AppApiGroup source;
    internal AppUserInquiryGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetAssignedRoles = source.AddAction<UserModifierKey, AppRoleModel[]>("GetAssignedRoles").WithExecution<GetAssignedRolesAction>();
        GetExplicitlyUnassignedRoles = source.AddAction<UserModifierKey, AppRoleModel[]>("GetExplicitlyUnassignedRoles").WithExecution<GetExplicitlyUnassignedRolesAction>();
        GetExplicitUserAccess = source.AddAction<UserModifierKey, UserAccessModel>("GetExplicitUserAccess").WithExecution<GetExplicitUserAccessAction>();
        Index = source.AddAction<GetAppUserRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<UserModifierKey, AppRoleModel[]> GetAssignedRoles { get; }
    public AppApiActionBuilder<UserModifierKey, AppRoleModel[]> GetExplicitlyUnassignedRoles { get; }
    public AppApiActionBuilder<UserModifierKey, UserAccessModel> GetExplicitUserAccess { get; }
    public AppApiActionBuilder<GetAppUserRequest, WebViewResult> Index { get; }

    public AppUserInquiryGroup Build() => new AppUserInquiryGroup(source, this);
}