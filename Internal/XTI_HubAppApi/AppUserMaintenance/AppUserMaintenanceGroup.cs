using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;

namespace XTI_HubAppApi.AppUserMaintenance;

public sealed class AppUserMaintenanceGroup : AppApiGroupWrapper
{
    public AppUserMaintenanceGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new AppApiActionFactory(source);
        AssignRole = source.AddAction
        (
            actions.Action(nameof(AssignRole), () => sp.GetRequiredService<AssignRoleAction>())
        );
        UnassignRole = source.AddAction
        (
            actions.Action(nameof(UnassignRole), () => sp.GetRequiredService<UnassignRoleAction>())
        );
        DenyAccess = source.AddAction
        (
            actions.Action(nameof(DenyAccess), () => sp.GetRequiredService<DenyAccessAction>())
        );
        AllowAccess = source.AddAction
        (
            actions.Action(nameof(AllowAccess), () => sp.GetRequiredService<AllowAccessAction>())
        );
    }
    public AppApiAction<UserRoleRequest, int> AssignRole { get; }
    public AppApiAction<UserRoleRequest, EmptyActionResult> UnassignRole { get; }
    public AppApiAction<UserModifierKey, EmptyActionResult> DenyAccess { get; }
    public AppApiAction<UserModifierKey, EmptyActionResult> AllowAccess { get; }
}