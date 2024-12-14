using XTI_HubWebAppApiActions.UserMaintenance;

// Generated Code
namespace XTI_HubWebAppApi.UserMaintenance;
public sealed partial class UserMaintenanceGroupBuilder
{
    private readonly AppApiGroup source;
    internal UserMaintenanceGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        ChangePassword = source.AddAction<ChangePasswordForm, EmptyActionResult>("ChangePassword").WithExecution<ChangePasswordAction>();
        DeactivateUser = source.AddAction<int, AppUserModel>("DeactivateUser").WithExecution<DeactivateUserAction>();
        EditUser = source.AddAction<EditUserForm, EmptyActionResult>("EditUser").WithExecution<EditUserAction>();
        GetUserForEdit = source.AddAction<int, IDictionary<string, object?>>("GetUserForEdit").WithExecution<GetUserForEditAction>();
        ReactivateUser = source.AddAction<int, AppUserModel>("ReactivateUser").WithExecution<ReactivateUserAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<ChangePasswordForm, EmptyActionResult> ChangePassword { get; }
    public AppApiActionBuilder<int, AppUserModel> DeactivateUser { get; }
    public AppApiActionBuilder<EditUserForm, EmptyActionResult> EditUser { get; }
    public AppApiActionBuilder<int, IDictionary<string, object?>> GetUserForEdit { get; }
    public AppApiActionBuilder<int, AppUserModel> ReactivateUser { get; }

    public UserMaintenanceGroup Build() => new UserMaintenanceGroup(source, this);
}