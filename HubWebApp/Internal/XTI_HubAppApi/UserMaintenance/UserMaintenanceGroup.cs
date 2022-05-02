namespace XTI_HubAppApi.UserMaintenance;

public sealed class UserMaintenanceGroup : AppApiGroupWrapper
{
    public UserMaintenanceGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new AppApiActionFactory(source);
        EditUser = source.AddAction(actions.Action(nameof(EditUser), () => sp.GetRequiredService<EditUserAction>()));
        GetUserForEdit = source.AddAction(actions.Action(nameof(GetUserForEdit), () => sp.GetRequiredService<GetUserForEditAction>()));
    }

    public AppApiAction<EditUserForm, EmptyActionResult> EditUser { get; }
    public AppApiAction<int, IDictionary<string, object?>> GetUserForEdit { get; }
}