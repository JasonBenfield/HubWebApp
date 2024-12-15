using XTI_HubWebAppApiActions.System;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.System;
public sealed partial class SystemGroupBuilder
{
    private readonly AppApiGroup source;
    internal SystemGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AddOrUpdateModifierByModKey = source.AddAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey").WithExecution<AddOrUpdateModifierByModKeyAction>();
        AddOrUpdateModifierByTargetKey = source.AddAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey").WithExecution<AddOrUpdateModifierByTargetKeyAction>();
        GetAppContext = source.AddAction<GetAppContextRequest, AppContextModel>("GetAppContext").WithExecution<GetAppContextAction>();
        GetModifier = source.AddAction<GetModifierRequest, ModifierModel>("GetModifier").WithExecution<GetModifierAction>();
        GetStoredObject = source.AddAction<GetStoredObjectRequest, string>("GetStoredObject").WithExecution<GetStoredObjectAction>().WithValidation<GetStoredObjectValidation>();
        GetUserAuthenticators = source.AddAction<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators").WithExecution<GetUserAuthenticatorsAction>();
        GetUserByUserName = source.AddAction<AppUserNameRequest, AppUserModel>("GetUserByUserName").WithExecution<GetUserByUserNameAction>();
        GetUserOrAnon = source.AddAction<AppUserNameRequest, AppUserModel>("GetUserOrAnon").WithExecution<GetUserOrAnonAction>();
        GetUserRoles = source.AddAction<GetUserRolesRequest, AppRoleModel[]>("GetUserRoles").WithExecution<GetUserRolesAction>();
        GetUsersWithAnyRole = source.AddAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole").WithExecution<GetUsersWithAnyRoleAction>();
        SetUserAccess = source.AddAction<SystemSetUserAccessRequest, EmptyActionResult>("SetUserAccess").WithExecution<SetUserAccessAction>();
        StoreObject = source.AddAction<StoreObjectRequest, string>("StoreObject").WithExecution<StoreObjectAction>().WithValidation<StoreObjectValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey { get; }
    public AppApiActionBuilder<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey { get; }
    public AppApiActionBuilder<GetAppContextRequest, AppContextModel> GetAppContext { get; }
    public AppApiActionBuilder<GetModifierRequest, ModifierModel> GetModifier { get; }
    public AppApiActionBuilder<GetStoredObjectRequest, string> GetStoredObject { get; }
    public AppApiActionBuilder<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiActionBuilder<AppUserNameRequest, AppUserModel> GetUserByUserName { get; }
    public AppApiActionBuilder<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiActionBuilder<GetUserRolesRequest, AppRoleModel[]> GetUserRoles { get; }
    public AppApiActionBuilder<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole { get; }
    public AppApiActionBuilder<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
    public AppApiActionBuilder<StoreObjectRequest, string> StoreObject { get; }

    public SystemGroup Build() => new SystemGroup(source, this);
}