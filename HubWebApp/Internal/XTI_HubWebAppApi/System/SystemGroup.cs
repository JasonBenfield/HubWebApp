﻿using XTI_HubWebAppApi.Storage;

namespace XTI_HubWebAppApi.System;

public sealed class SystemGroup : AppApiGroupWrapper
{
    public SystemGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetAppContext = source.AddAction
        (
            nameof(GetAppContext), 
            () => sp.GetRequiredService<GetAppContextAction>()
        );
        GetModifier = source.AddAction
        (
            nameof(GetModifier),
            () => sp.GetRequiredService<GetModifierAction>()
        );
        AddOrUpdateModifierByTargetKey = source.AddAction
        (
            nameof(AddOrUpdateModifierByTargetKey), 
            () => sp.GetRequiredService<AddOrUpdateModifierByTargetKeyAction>()
        );
        AddOrUpdateModifierByModKey = source.AddAction
        (
            nameof(AddOrUpdateModifierByModKey),
            () => sp.GetRequiredService<AddOrUpdateModifierByModKeyAction>()
        );
        GetUserOrAnon = source.AddAction
        (
            nameof(GetUserOrAnon),
            () => sp.GetRequiredService<GetUserOrAnonAction>()
        );
        GetUserRoles = source.AddAction
        (
            nameof(GetUserRoles),
            () => sp.GetRequiredService<GetUserRolesAction>()
        );
        GetUserAuthenticators = source.AddAction
        (
            nameof(GetUserAuthenticators),
            () => sp.GetRequiredService<GetUserAuthenticatorsAction>()
        );
        GetUsersWithAnyRole = source.AddAction
        (
            nameof(GetUsersWithAnyRole),
            () => sp.GetRequiredService<GetUsersWithAnyRoleAction>()
        );
        StoreObject = source.AddAction
        (
            nameof(StoreObject),
            () => sp.GetRequiredService<SystemStoreObjectAction>(),
            () => sp.GetRequiredService<StoreObjectValidation>()
        );
        GetStoredObject = source.AddAction
        (
            nameof(GetStoredObject),
            () => sp.GetRequiredService<SystemGetStoredObjectAction>(),
            () => sp.GetRequiredService<GetStoredObjectValidation>()
        );
        SetUserAccess = source.AddAction
        (
            nameof(SetUserAccess),
            () => sp.GetRequiredService<SystemSetUserAccessAction>()
        );
    }

    public AppApiAction<GetAppContextRequest, AppContextModel> GetAppContext { get; }
    public AppApiAction<GetModifierRequest, ModifierModel> GetModifier { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles { get; }
    public AppApiAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole { get; }
    public AppApiAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey { get; }
    public AppApiAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey { get; }
    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
    public AppApiAction<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
}