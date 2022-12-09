using XTI_HubWebAppApi.Storage;

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
        GetUserContext = source.AddAction
        (
            nameof(GetUserContext), 
            () => sp.GetRequiredService<GetUserContextAction>()
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
    }

    public AppApiAction<GetAppContextRequest, AppContextModel> GetAppContext { get; }
    public AppApiAction<GetUserContextRequest, UserContextModel> GetUserContext { get; }
    public AppApiAction<string, AppUserModel> GetUserOrAnon { get; }
    public AppApiAction<int, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiAction<GetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole { get; }
    public AppApiAction<AddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey { get; }
    public AppApiAction<AddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey { get; }
    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
}