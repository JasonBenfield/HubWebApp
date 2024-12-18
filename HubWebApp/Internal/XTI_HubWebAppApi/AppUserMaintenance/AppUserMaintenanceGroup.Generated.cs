using XTI_HubWebAppApiActions.AppUserMaintenance;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.AppUserMaintenance;
public sealed partial class AppUserMaintenanceGroup : AppApiGroupWrapper
{
    internal AppUserMaintenanceGroup(AppApiGroup source, AppUserMaintenanceGroupBuilder builder) : base(source)
    {
        AllowAccess = builder.AllowAccess.Build();
        AssignRole = builder.AssignRole.Build();
        DenyAccess = builder.DenyAccess.Build();
        UnassignRole = builder.UnassignRole.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<UserModifierKey, EmptyActionResult> AllowAccess { get; }
    public AppApiAction<UserRoleRequest, int> AssignRole { get; }
    public AppApiAction<UserModifierKey, EmptyActionResult> DenyAccess { get; }
    public AppApiAction<UserRoleRequest, EmptyActionResult> UnassignRole { get; }
}