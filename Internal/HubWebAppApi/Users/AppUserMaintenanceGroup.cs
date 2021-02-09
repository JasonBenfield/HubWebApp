using XTI_App.Api;

namespace HubWebAppApi.Users
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
        public AppApiAction<AssignRoleRequest, int> AssignRole { get; }
        public AppApiAction<int, EmptyActionResult> UnassignRole { get; }
    }
}
