// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserMaintenanceGroup : AppClientGroup
{
    public UserMaintenanceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserMaintenance")
    {
        Actions = new UserMaintenanceGroupActions(DeactivateUser: CreatePostAction<int, AppUserModel>("DeactivateUser"), ReactivateUser: CreatePostAction<int, AppUserModel>("ReactivateUser"), EditUser: CreatePostAction<EditUserForm, EmptyActionResult>("EditUser"), ChangePassword: CreatePostAction<ChangePasswordForm, EmptyActionResult>("ChangePassword"), GetUserForEdit: CreatePostAction<int, IDictionary<string, object>>("GetUserForEdit"));
    }

    public UserMaintenanceGroupActions Actions { get; }

    public Task<AppUserModel> DeactivateUser(string modifier, int model, CancellationToken ct = default) => Actions.DeactivateUser.Post(modifier, model, ct);
    public Task<AppUserModel> ReactivateUser(string modifier, int model, CancellationToken ct = default) => Actions.ReactivateUser.Post(modifier, model, ct);
    public Task<EmptyActionResult> EditUser(string modifier, EditUserForm model, CancellationToken ct = default) => Actions.EditUser.Post(modifier, model, ct);
    public Task<EmptyActionResult> ChangePassword(string modifier, ChangePasswordForm model, CancellationToken ct = default) => Actions.ChangePassword.Post(modifier, model, ct);
    public Task<IDictionary<string, object>> GetUserForEdit(string modifier, int model, CancellationToken ct = default) => Actions.GetUserForEdit.Post(modifier, model, ct);
    public sealed record UserMaintenanceGroupActions(AppClientPostAction<int, AppUserModel> DeactivateUser, AppClientPostAction<int, AppUserModel> ReactivateUser, AppClientPostAction<EditUserForm, EmptyActionResult> EditUser, AppClientPostAction<ChangePasswordForm, EmptyActionResult> ChangePassword, AppClientPostAction<int, IDictionary<string, object>> GetUserForEdit);
}