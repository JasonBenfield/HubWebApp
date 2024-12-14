// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserMaintenanceGroup : AppClientGroup
{
    public UserMaintenanceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserMaintenance")
    {
        Actions = new UserMaintenanceGroupActions(ChangePassword: CreatePostAction<ChangePasswordForm, EmptyActionResult>("ChangePassword"), DeactivateUser: CreatePostAction<int, AppUserModel>("DeactivateUser"), EditUser: CreatePostAction<EditUserForm, EmptyActionResult>("EditUser"), GetUserForEdit: CreatePostAction<int, IDictionary<string, object>>("GetUserForEdit"), ReactivateUser: CreatePostAction<int, AppUserModel>("ReactivateUser"));
    }

    public UserMaintenanceGroupActions Actions { get; }

    public Task<EmptyActionResult> ChangePassword(string modifier, ChangePasswordForm model, CancellationToken ct = default) => Actions.ChangePassword.Post(modifier, model, ct);
    public Task<AppUserModel> DeactivateUser(string modifier, int model, CancellationToken ct = default) => Actions.DeactivateUser.Post(modifier, model, ct);
    public Task<EmptyActionResult> EditUser(string modifier, EditUserForm model, CancellationToken ct = default) => Actions.EditUser.Post(modifier, model, ct);
    public Task<IDictionary<string, object>> GetUserForEdit(string modifier, int model, CancellationToken ct = default) => Actions.GetUserForEdit.Post(modifier, model, ct);
    public Task<AppUserModel> ReactivateUser(string modifier, int model, CancellationToken ct = default) => Actions.ReactivateUser.Post(modifier, model, ct);
    public sealed record UserMaintenanceGroupActions(AppClientPostAction<ChangePasswordForm, EmptyActionResult> ChangePassword, AppClientPostAction<int, AppUserModel> DeactivateUser, AppClientPostAction<EditUserForm, EmptyActionResult> EditUser, AppClientPostAction<int, IDictionary<string, object>> GetUserForEdit, AppClientPostAction<int, AppUserModel> ReactivateUser);
}