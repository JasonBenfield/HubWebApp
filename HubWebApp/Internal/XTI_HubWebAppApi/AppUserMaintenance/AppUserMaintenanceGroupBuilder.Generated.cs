using XTI_HubWebAppApiActions.AppUserMaintenance;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.AppUserMaintenance;
public sealed partial class AppUserMaintenanceGroupBuilder
{
    private readonly AppApiGroup source;
    internal AppUserMaintenanceGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AllowAccess = source.AddAction<UserModifierKey, EmptyActionResult>("AllowAccess").WithExecution<AllowAccessAction>();
        AssignRole = source.AddAction<UserRoleRequest, int>("AssignRole").WithExecution<AssignRoleAction>();
        DenyAccess = source.AddAction<UserModifierKey, EmptyActionResult>("DenyAccess").WithExecution<DenyAccessAction>();
        EditUserGroup = source.AddAction<EditUserGroupRequest, EmptyActionResult>("EditUserGroup").WithExecution<EditUserGroupAction>().WithValidation<EditUserGroupValidation>();
        UnassignRole = source.AddAction<UserRoleRequest, EmptyActionResult>("UnassignRole").WithExecution<UnassignRoleAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<UserModifierKey, EmptyActionResult> AllowAccess { get; }
    public AppApiActionBuilder<UserRoleRequest, int> AssignRole { get; }
    public AppApiActionBuilder<UserModifierKey, EmptyActionResult> DenyAccess { get; }
    public AppApiActionBuilder<EditUserGroupRequest, EmptyActionResult> EditUserGroup { get; }
    public AppApiActionBuilder<UserRoleRequest, EmptyActionResult> UnassignRole { get; }

    public AppUserMaintenanceGroup Build() => new AppUserMaintenanceGroup(source, this);
}