// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallGroup : AppClientGroup
{
    public InstallGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Install")
    {
        Actions = new InstallGroupActions(RegisterApp: CreatePostAction<RegisterAppRequest, AppModel>("RegisterApp"), AddOrUpdateApps: CreatePostAction<AddOrUpdateAppsRequest, AppModel[]>("AddOrUpdateApps"), AddOrUpdateVersions: CreatePostAction<AddOrUpdateVersionsRequest, EmptyActionResult>("AddOrUpdateVersions"), GetVersion: CreatePostAction<GetVersionRequest, XtiVersionModel>("GetVersion"), GetVersions: CreatePostAction<GetVersionsRequest, XtiVersionModel[]>("GetVersions"), AddSystemUser: CreatePostAction<AddSystemUserRequest, AppUserModel>("AddSystemUser"), AddAdminUser: CreatePostAction<AddAdminUserRequest, AppUserModel>("AddAdminUser"), AddInstallationUser: CreatePostAction<AddInstallationUserRequest, AppUserModel>("AddInstallationUser"), NewInstallation: CreatePostAction<NewInstallationRequest, NewInstallationResult>("NewInstallation"), BeginInstallation: CreatePostAction<InstallationRequest, EmptyActionResult>("BeginInstallation"), Installed: CreatePostAction<InstallationRequest, EmptyActionResult>("Installed"), SetUserAccess: CreatePostAction<SetUserAccessRequest, EmptyActionResult>("SetUserAccess"));
    }

    public InstallGroupActions Actions { get; }

    public Task<AppModel> RegisterApp(RegisterAppRequest model) => Actions.RegisterApp.Post("", model);
    public Task<AppModel[]> AddOrUpdateApps(AddOrUpdateAppsRequest model) => Actions.AddOrUpdateApps.Post("", model);
    public Task<EmptyActionResult> AddOrUpdateVersions(AddOrUpdateVersionsRequest model) => Actions.AddOrUpdateVersions.Post("", model);
    public Task<XtiVersionModel> GetVersion(GetVersionRequest model) => Actions.GetVersion.Post("", model);
    public Task<XtiVersionModel[]> GetVersions(GetVersionsRequest model) => Actions.GetVersions.Post("", model);
    public Task<AppUserModel> AddSystemUser(AddSystemUserRequest model) => Actions.AddSystemUser.Post("", model);
    public Task<AppUserModel> AddAdminUser(AddAdminUserRequest model) => Actions.AddAdminUser.Post("", model);
    public Task<AppUserModel> AddInstallationUser(AddInstallationUserRequest model) => Actions.AddInstallationUser.Post("", model);
    public Task<NewInstallationResult> NewInstallation(NewInstallationRequest model) => Actions.NewInstallation.Post("", model);
    public Task<EmptyActionResult> BeginInstallation(InstallationRequest model) => Actions.BeginInstallation.Post("", model);
    public Task<EmptyActionResult> Installed(InstallationRequest model) => Actions.Installed.Post("", model);
    public Task<EmptyActionResult> SetUserAccess(SetUserAccessRequest model) => Actions.SetUserAccess.Post("", model);
    public sealed record InstallGroupActions(AppClientPostAction<RegisterAppRequest, AppModel> RegisterApp, AppClientPostAction<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps, AppClientPostAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions, AppClientPostAction<GetVersionRequest, XtiVersionModel> GetVersion, AppClientPostAction<GetVersionsRequest, XtiVersionModel[]> GetVersions, AppClientPostAction<AddSystemUserRequest, AppUserModel> AddSystemUser, AppClientPostAction<AddAdminUserRequest, AppUserModel> AddAdminUser, AppClientPostAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser, AppClientPostAction<NewInstallationRequest, NewInstallationResult> NewInstallation, AppClientPostAction<InstallationRequest, EmptyActionResult> BeginInstallation, AppClientPostAction<InstallationRequest, EmptyActionResult> Installed, AppClientPostAction<SetUserAccessRequest, EmptyActionResult> SetUserAccess);
}