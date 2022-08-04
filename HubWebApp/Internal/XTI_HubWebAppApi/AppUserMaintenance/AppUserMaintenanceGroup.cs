namespace XTI_HubWebAppApi.AppUserMaintenance;

public sealed class AppUserMaintenanceGroup : AppApiGroupWrapper
{
    public AppUserMaintenanceGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        AssignRole = source.AddAction
        (
            nameof(AssignRole), () => sp.GetRequiredService<AssignRoleAction>()
        );
        UnassignRole = source.AddAction
        (
            nameof(UnassignRole), () => sp.GetRequiredService<UnassignRoleAction>()
        );
        DenyAccess = source.AddAction
        (
            nameof(DenyAccess), () => sp.GetRequiredService<DenyAccessAction>()
        );
        AllowAccess = source.AddAction
        (
            nameof(AllowAccess), () => sp.GetRequiredService<AllowAccessAction>()
        );
    }
    public AppApiAction<UserRoleRequest, int> AssignRole { get; }
    public AppApiAction<UserRoleRequest, EmptyActionResult> UnassignRole { get; }
    public AppApiAction<UserModifierKey, EmptyActionResult> DenyAccess { get; }
    public AppApiAction<UserModifierKey, EmptyActionResult> AllowAccess { get; }
}