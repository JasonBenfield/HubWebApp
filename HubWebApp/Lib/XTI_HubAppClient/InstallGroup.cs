// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallGroup : AppClientGroup
{
    public InstallGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Install")
    {
        Actions = new InstallGroupActions(RegisterApp: CreatePostAction<RegisterAppRequest, AppModel>("RegisterApp"), AddOrUpdateApps: CreatePostAction<AddOrUpdateAppsRequest, AppModel[]>("AddOrUpdateApps"), AddOrUpdateVersions: CreatePostAction<AddOrUpdateVersionsRequest, EmptyActionResult>("AddOrUpdateVersions"), GetVersion: CreatePostAction<GetVersionRequest, XtiVersionModel>("GetVersion"), GetVersions: CreatePostAction<GetVersionsRequest, XtiVersionModel[]>("GetVersions"), AddSystemUser: CreatePostAction<AddSystemUserRequest, AppUserModel>("AddSystemUser"), AddAdminUser: CreatePostAction<AddAdminUserRequest, AppUserModel>("AddAdminUser"), AddInstallationUser: CreatePostAction<AddInstallationUserRequest, AppUserModel>("AddInstallationUser"), NewInstallation: CreatePostAction<NewInstallationRequest, NewInstallationResult>("NewInstallation"), BeginInstallation: CreatePostAction<InstallationRequest, EmptyActionResult>("BeginInstallation"), Installed: CreatePostAction<InstallationRequest, EmptyActionResult>("Installed"), SetUserAccess: CreatePostAction<SetUserAccessRequest, EmptyActionResult>("SetUserAccess"));
    }

    public InstallGroupActions Actions { get; }

    public Task<AppModel> RegisterApp(RegisterAppRequest model, CancellationToken ct = default) => Actions.RegisterApp.Post("", model, ct);
    public Task<AppModel[]> AddOrUpdateApps(AddOrUpdateAppsRequest model, CancellationToken ct = default) => Actions.AddOrUpdateApps.Post("", model, ct);
    public Task<EmptyActionResult> AddOrUpdateVersions(AddOrUpdateVersionsRequest model, CancellationToken ct = default) => Actions.AddOrUpdateVersions.Post("", model, ct);
    public Task<XtiVersionModel> GetVersion(GetVersionRequest model, CancellationToken ct = default) => Actions.GetVersion.Post("", model, ct);
    public Task<XtiVersionModel[]> GetVersions(GetVersionsRequest model, CancellationToken ct = default) => Actions.GetVersions.Post("", model, ct);
    public Task<AppUserModel> AddSystemUser(AddSystemUserRequest model, CancellationToken ct = default) => Actions.AddSystemUser.Post("", model, ct);
    public Task<AppUserModel> AddAdminUser(AddAdminUserRequest model, CancellationToken ct = default) => Actions.AddAdminUser.Post("", model, ct);
    public Task<AppUserModel> AddInstallationUser(AddInstallationUserRequest model, CancellationToken ct = default) => Actions.AddInstallationUser.Post("", model, ct);
    public Task<NewInstallationResult> NewInstallation(NewInstallationRequest model, CancellationToken ct = default) => Actions.NewInstallation.Post("", model, ct);
    public Task<EmptyActionResult> BeginInstallation(InstallationRequest model, CancellationToken ct = default) => Actions.BeginInstallation.Post("", model, ct);
    public Task<EmptyActionResult> Installed(InstallationRequest model, CancellationToken ct = default) => Actions.Installed.Post("", model, ct);
    public Task<EmptyActionResult> SetUserAccess(SetUserAccessRequest model, CancellationToken ct = default) => Actions.SetUserAccess.Post("", model, ct);
    public sealed record InstallGroupActions(AppClientPostAction<RegisterAppRequest, AppModel> RegisterApp, AppClientPostAction<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps, AppClientPostAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions, AppClientPostAction<GetVersionRequest, XtiVersionModel> GetVersion, AppClientPostAction<GetVersionsRequest, XtiVersionModel[]> GetVersions, AppClientPostAction<AddSystemUserRequest, AppUserModel> AddSystemUser, AppClientPostAction<AddAdminUserRequest, AppUserModel> AddAdminUser, AppClientPostAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser, AppClientPostAction<NewInstallationRequest, NewInstallationResult> NewInstallation, AppClientPostAction<InstallationRequest, EmptyActionResult> BeginInstallation, AppClientPostAction<InstallationRequest, EmptyActionResult> Installed, AppClientPostAction<SetUserAccessRequest, EmptyActionResult> SetUserAccess);
}