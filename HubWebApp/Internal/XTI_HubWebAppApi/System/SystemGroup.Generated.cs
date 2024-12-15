using XTI_HubWebAppApiActions.System;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.System;
public sealed partial class SystemGroup : AppApiGroupWrapper
{
    internal SystemGroup(AppApiGroup source, SystemGroupBuilder builder) : base(source)
    {
        AddOrUpdateModifierByModKey = builder.AddOrUpdateModifierByModKey.Build();
        AddOrUpdateModifierByTargetKey = builder.AddOrUpdateModifierByTargetKey.Build();
        GetAppContext = builder.GetAppContext.Build();
        GetModifier = builder.GetModifier.Build();
        GetStoredObject = builder.GetStoredObject.Build();
        GetUserAuthenticators = builder.GetUserAuthenticators.Build();
        GetUserByUserName = builder.GetUserByUserName.Build();
        GetUserOrAnon = builder.GetUserOrAnon.Build();
        GetUserRoles = builder.GetUserRoles.Build();
        GetUsersWithAnyRole = builder.GetUsersWithAnyRole.Build();
        SetUserAccess = builder.SetUserAccess.Build();
        StoreObject = builder.StoreObject.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey { get; }
    public AppApiAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey { get; }
    public AppApiAction<GetAppContextRequest, AppContextModel> GetAppContext { get; }
    public AppApiAction<GetModifierRequest, ModifierModel> GetModifier { get; }
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
    public AppApiAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserByUserName { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles { get; }
    public AppApiAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole { get; }
    public AppApiAction<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
}