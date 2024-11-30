using XTI_HubWebAppApi.Storage;

namespace XTI_HubWebAppApi.System;

public sealed class SystemGroup : AppApiGroupWrapper
{
    public SystemGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetAppContext = source.AddAction<GetAppContextRequest, AppContextModel>()
            .Named(nameof(GetAppContext))
            .WithExecution<GetAppContextAction>()
            .Build();
        GetModifier = source.AddAction<GetModifierRequest, ModifierModel>()
            .Named(nameof(GetModifier))
            .WithExecution<GetModifierAction>()
            .ThrottleRequestLogging().For(5).Minutes()
            .Build();
        AddOrUpdateModifierByTargetKey =
            source.AddAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>()
                .Named(nameof(AddOrUpdateModifierByTargetKey))
                .WithExecution<AddOrUpdateModifierByTargetKeyAction>()
                .Build();
        AddOrUpdateModifierByModKey =
            source.AddAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>()
                .Named(nameof(AddOrUpdateModifierByModKey))
                .WithExecution<AddOrUpdateModifierByModKeyAction>()
                .Build();
        GetUserByUserName = source.AddAction<AppUserNameRequest, AppUserModel>()
            .Named(nameof(GetUserByUserName))
            .WithExecution<GetUserByUserNameAction>()
            .ThrottleRequestLogging().For(5).Minutes()
            .Build();
        GetUserOrAnon = source.AddAction<AppUserNameRequest, AppUserModel>()
            .Named(nameof(GetUserOrAnon))
            .WithExecution<GetUserOrAnonAction>()
            .ThrottleRequestLogging().For(5).Minutes()
            .Build();
        GetUserRoles = source.AddAction<GetUserRolesRequest, AppRoleModel[]>()
            .Named(nameof(GetUserRoles))
            .WithExecution<GetUserRolesAction>()
            .ThrottleRequestLogging().For(5).Minutes()
            .Build();
        GetUserAuthenticators = source.AddAction<AppUserIDRequest, UserAuthenticatorModel[]>()
            .Named(nameof(GetUserAuthenticators))
            .WithExecution<GetUserAuthenticatorsAction>()
            .Build();
        GetUsersWithAnyRole = source.AddAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>()
            .Named(nameof(GetUsersWithAnyRole))
            .WithExecution<GetUsersWithAnyRoleAction>()
            .Build();
        StoreObject = source.AddAction<StoreObjectRequest, string>()
            .Named(nameof(StoreObject))
            .WithExecution<SystemStoreObjectAction>()
            .WithValidation<StoreObjectValidation>()
            .ThrottleRequestLogging().For(5).Minutes()
            .Build();
        GetStoredObject = source.AddAction<GetStoredObjectRequest, string>()
            .Named(nameof(GetStoredObject))
            .WithExecution<SystemGetStoredObjectAction>()
            .WithValidation<GetStoredObjectValidation>()
            .ThrottleRequestLogging().For(5).Minutes()
            .Build();
        SetUserAccess = source.AddAction<SystemSetUserAccessRequest, EmptyActionResult>()
            .Named(nameof(SetUserAccess))
            .WithExecution<SystemSetUserAccessAction>()
            .Build();
    }

    public AppApiAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey { get; }
    public AppApiAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey { get; }
    public AppApiAction<GetAppContextRequest, AppContextModel> GetAppContext { get; }
    public AppApiAction<GetModifierRequest, ModifierModel> GetModifier { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserByUserName { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles { get; }
    public AppApiAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole { get; }
    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
    public AppApiAction<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
}