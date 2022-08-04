using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUser
{
    private readonly HubFactory factory;
    private readonly AppUserEntity record;

    private static readonly AppRoleName[] viewAppRoles = new[] { AppRoleName.Admin, HubInfo.Roles.ViewApp };
    private static readonly AppRoleName[] editAppRoles = new[] { AppRoleName.Admin };
    private static readonly AppRoleName[] viewUserRoles = new[] { AppRoleName.Admin, HubInfo.Roles.ViewUser };
    private static readonly AppRoleName[] editUserRoles = new[] { AppRoleName.Admin, HubInfo.Roles.EditUser };

    internal AppUser(HubFactory factory, AppUserEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppUserEntity();
        ID = this.record.ID;
    }

    internal int ID { get; }

    public bool IsUserName(AppUserName userName) => new AppUserName(record.UserName).Equals(userName);

    public bool IsPasswordCorrect(IHashedPassword hashedPassword) =>
        hashedPassword.Equals(record.Password);

    public async Task AssignRole(AppRole role)
    {
        var app = await role.App();
        var modifier = await app.DefaultModifier();
        await Modifier(modifier).AssignRole(role);
    }

    public async Task<AppUserModifier[]> Modifiers(App app)
    {
        var modifiers = await app.Modifiers();
        var userModifiers = new List<AppUserModifier>();
        foreach (var modifier in modifiers)
        {
            userModifiers.Add(Modifier(modifier));
        }
        return userModifiers.ToArray();
    }

    public AppUserModifier Modifier(Modifier modifier) =>
        new AppUserModifier(factory, this, modifier);

    public Task ChangePassword(IHashedPassword password)
        => factory.DB.Users.Update(record, u => u.Password = password.Value());

    public Task Edit(PersonName name, EmailAddress email)
        => factory.DB.Users.Update
        (
            record,
            u =>
            {
                u.Name = name.Value;
                u.Email = email.Value;
            }
        );

    public async Task AddAuthenticator(App authenticatorApp, string externalUserKey)
    {
        var authenticatorIDs = factory.DB
            .Authenticators.Retrieve()
            .Where(a => a.AppID == authenticatorApp.ID)
            .Select(a => a.ID);
        var entity = await factory.DB
            .UserAuthenticators.Retrieve()
            .Where
            (
                ua =>
                    authenticatorIDs.Contains(ua.AuthenticatorID)
                    && ua.UserID == ID
            )
            .FirstOrDefaultAsync();
        if (entity == null)
        {
            var authenticatorID = await authenticatorIDs.FirstAsync();
            entity = new UserAuthenticatorEntity
            {
                AuthenticatorID = authenticatorID,
                UserID = ID,
                ExternalUserKey = externalUserKey
            };
            await factory.DB.UserAuthenticators.Create(entity);
        }
        else
        {
            await factory.DB.UserAuthenticators.Update
            (
                entity,
                ua => ua.ExternalUserKey = externalUserKey
            );
        }
    }

    public async Task<AppPermission[]> GetAppPermissions()
    {
        var apps = await factory.Apps.All();
        var hubApp = apps.First(a => a.AppKeyEquals(HubInfo.AppKey));
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var appPermissions = new List<AppPermission>();
        foreach (var app in apps)
        {
            var permission = await GetAppPermission(hubApp, appsModCategory, app);
            appPermissions.Add(permission);
        }
        return appPermissions.ToArray();
    }

    public async Task<AppPermission> GetAppPermission(App app)
    {
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var permission = await GetAppPermission(hubApp, appsModCategory, app);
        return permission;
    }

    private async Task<AppPermission> GetAppPermission(App hubApp, ModifierCategory appsModCategory, App app)
    {
        var appModel = app.ToModel();
        Modifier modifier;
        if(appModel.AppKey.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage))
        {
            modifier = await hubApp.DefaultModifier();
        }
        else
        {
            modifier = await appsModCategory.ModifierByTargetID(appModel.ID);
        }
        var userRoles = await Modifier(modifier).AssignedRoles();
        var userRoleModels = userRoles.Select(ur => ur.ToModel());
        AppPermission permission;
        if (userRoleModels.Any(ur => ur.Name.Equals(AppRoleName.DenyAccess)))
        {
            permission = new AppPermission(app, false, false);
        }
        else
        {
            permission = new AppPermission
            (
                App: app,
                CanView: userRoleModels
                    .Any(ur => ur.Name.EqualsAny(viewAppRoles)),
                CanEdit: userRoleModels
                    .Any(ur => ur.Name.EqualsAny(editAppRoles))
            );
        }
        return permission;
    }

    public Task<AppUserGroup> UserGroup() => factory.UserGroups.UserGroup(record.GroupID);

    public async Task<AppUserGroupPermission[]> GetUserGroupPermissions()
    {
        var userGroups = await factory.UserGroups.UserGroups();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups);
        var userGroupPermissions = new List<AppUserGroupPermission>();
        foreach (var userGroup in userGroups)
        {
            var userGroupPermission = await GetUserGroupPermission(userGroupsModCategory, userGroup);
            userGroupPermissions.Add(userGroupPermission);
        }
        return userGroupPermissions.ToArray();
    }

    public async Task<AppUserGroupPermission> GetUserGroupPermission(AppUserGroup userGroup)
    {
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups);
        var permission = await GetUserGroupPermission(userGroupsModCategory, userGroup);
        return permission;
    }

    private async Task<AppUserGroupPermission> GetUserGroupPermission(ModifierCategory userGroupsModCategory, AppUserGroup userGroup)
    {
        AppUserGroupPermission userGroupPermission;
        var userGroupModel = userGroup.ToModel();
        var modifier = await userGroupsModCategory.AddOrUpdateModifier(userGroupModel.PublicKey, userGroupModel.ID, userGroupModel.GroupName.DisplayText);
        var userRoles = await Modifier(modifier).AssignedRoles();
        var userRoleModels = userRoles.Select(ur => ur.ToModel());
        if (userRoles.Any(ur => ur.IsDenyAccess()))
        {
            userGroupPermission = new AppUserGroupPermission(userGroup, false, false);
        }
        else
        {
            userGroupPermission = new AppUserGroupPermission
            (
                UserGroup: userGroup,
                CanView: userRoleModels
                    .Any(ur => ur.Name.EqualsAny(viewUserRoles)),
                CanEdit: userRoleModels
                    .Any(ur => ur.Name.EqualsAny(editUserRoles))
            );
        }
        return userGroupPermission;
    }

    public AppUserModel ToModel() => new AppUserModel
    {
        ID = ID,
        UserName = new AppUserName(record.UserName),
        Name = new PersonName(record.Name),
        Email = new EmailAddress(record.Email).DisplayText
    };

    public override string ToString() => $"{nameof(AppUser)} {ID}";
}