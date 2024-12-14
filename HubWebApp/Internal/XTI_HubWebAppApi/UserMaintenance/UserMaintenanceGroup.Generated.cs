using XTI_HubWebAppApiActions.UserMaintenance;

// Generated Code
namespace XTI_HubWebAppApi.UserMaintenance;
public sealed partial class UserMaintenanceGroup : AppApiGroupWrapper
{
    internal UserMaintenanceGroup(AppApiGroup source, UserMaintenanceGroupBuilder builder) : base(source)
    {
        ChangePassword = builder.ChangePassword.Build();
        DeactivateUser = builder.DeactivateUser.Build();
        EditUser = builder.EditUser.Build();
        GetUserForEdit = builder.GetUserForEdit.Build();
        ReactivateUser = builder.ReactivateUser.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<ChangePasswordForm, EmptyActionResult> ChangePassword { get; }
    public AppApiAction<int, AppUserModel> DeactivateUser { get; }
    public AppApiAction<EditUserForm, EmptyActionResult> EditUser { get; }
    public AppApiAction<int, IDictionary<string, object?>> GetUserForEdit { get; }
    public AppApiAction<int, AppUserModel> ReactivateUser { get; }
}