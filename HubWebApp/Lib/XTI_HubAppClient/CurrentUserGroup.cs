// Generated Code
namespace XTI_HubAppClient;
public sealed partial class CurrentUserGroup : AppClientGroup
{
    public CurrentUserGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "CurrentUser")
    {
        Actions = new CurrentUserGroupActions(EditUser: CreatePostAction<EditCurrentUserForm, AppUserModel>("EditUser"), GetUser: CreatePostAction<EmptyRequest, AppUserModel>("GetUser"), Index: CreateGetAction<EmptyRequest>("Index"));
    }

    public CurrentUserGroupActions Actions { get; }

    public Task<AppUserModel> EditUser(EditCurrentUserForm model, CancellationToken ct = default) => Actions.EditUser.Post("", model, ct);
    public Task<AppUserModel> GetUser(CancellationToken ct = default) => Actions.GetUser.Post("", new EmptyRequest(), ct);
    public sealed record CurrentUserGroupActions(AppClientPostAction<EditCurrentUserForm, AppUserModel> EditUser, AppClientPostAction<EmptyRequest, AppUserModel> GetUser, AppClientGetAction<EmptyRequest> Index);
}