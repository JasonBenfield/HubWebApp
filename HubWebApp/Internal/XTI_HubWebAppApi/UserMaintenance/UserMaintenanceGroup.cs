namespace XTI_HubWebAppApi.UserMaintenance;

public sealed class UserMaintenanceGroup : AppApiGroupWrapper
{
    public UserMaintenanceGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        DeactivateUser = source.AddAction(nameof(DeactivateUser), () => sp.GetRequiredService<DeactivateUserAction>());
        ReactivateUser = source.AddAction(nameof(ReactivateUser), () => sp.GetRequiredService<ReactivateUserAction>());
        EditUser = source.AddAction(nameof(EditUser), () => sp.GetRequiredService<EditUserAction>());
        ChangePassword = source.AddAction(nameof(ChangePassword), () => sp.GetRequiredService<ChangePasswordAction>());
        GetUserForEdit = source.AddAction(nameof(GetUserForEdit), () => sp.GetRequiredService<GetUserForEditAction>());
    }

    public AppApiAction<int, AppUserModel> DeactivateUser { get; }
    public AppApiAction<int, AppUserModel> ReactivateUser { get; }
    public AppApiAction<EditUserForm, EmptyActionResult> EditUser { get; }
    public AppApiAction<int, IDictionary<string, object?>> GetUserForEdit { get; }
    public AppApiAction<ChangePasswordForm, EmptyActionResult> ChangePassword { get; }
}