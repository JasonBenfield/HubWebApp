using XTI_HubWebAppApiActions.AppUserInquiry;

// Generated Code
namespace XTI_HubWebAppApi.AppUserInquiry;
public sealed partial class AppUserInquiryGroup : AppApiGroupWrapper
{
    internal AppUserInquiryGroup(AppApiGroup source, AppUserInquiryGroupBuilder builder) : base(source)
    {
        GetAssignedRoles = builder.GetAssignedRoles.Build();
        GetExplicitlyUnassignedRoles = builder.GetExplicitlyUnassignedRoles.Build();
        GetExplicitUserAccess = builder.GetExplicitUserAccess.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetAssignedRoles { get; }
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetExplicitlyUnassignedRoles { get; }
    public AppApiAction<UserModifierKey, UserAccessModel> GetExplicitUserAccess { get; }
    public AppApiAction<GetAppUserRequest, WebViewResult> Index { get; }
}