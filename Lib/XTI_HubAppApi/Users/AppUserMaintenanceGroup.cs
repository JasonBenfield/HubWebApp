using XTI_App.Api;

namespace XTI_HubAppApi.Users
{
    public sealed class AppUserMaintenanceGroup : AppApiGroupWrapper
    {
        public AppUserMaintenanceGroup(AppApiGroup source, AppUserMaintenanceGroupFactory factory)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            AssignRole = source.AddAction(actions.Action(nameof(AssignRole), factory.CreateAssignRole));
            UnassignRole = source.AddAction(actions.Action(nameof(UnassignRole), factory.CreateUnassignRole));
        }
        public AppApiAction<UserRoleRequest, int> AssignRole { get; }
        public AppApiAction<UserRoleRequest, EmptyActionResult> UnassignRole { get; }
    }
}
