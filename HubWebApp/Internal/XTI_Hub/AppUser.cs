using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUser
{
    private readonly HubFactory factory;
    private readonly AppUserEntity record;

    internal AppUser(HubFactory factory, AppUserEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppUserEntity();
        ID = this.record.ID;
    }

    internal int ID { get; }

    public bool HasID(int id) => ID == id;

    public bool IsUserName(AppUserName userName) => new AppUserName(record.UserName).Equals(userName);

    public bool IsPasswordCorrect(IHashedPassword hashedPassword) =>
        hashedPassword.Equals(record.Password);

    public async Task AssignRole(AppRole role, CancellationToken ct)
    {
        var app = await role.App(ct);
        var modifier = await app.DefaultModifier(ct);
        await Modifier(modifier).AssignRole(role, ct);
    }

    public async Task<AppUserModifier[]> Modifiers(App app, CancellationToken ct)
    {
        var modifiers = await app.Modifiers(ct);
        var userModifiers = new List<AppUserModifier>();
        foreach (var modifier in modifiers)
        {
            userModifiers.Add(Modifier(modifier));
        }
        return userModifiers.ToArray();
    }

    public AppUserModifier Modifier(Modifier modifier) =>
        new AppUserModifier(factory, this, modifier);

    public Task ChangePassword(IHashedPassword password) =>
        factory.DB.Users.Update(record, u => u.Password = password.Value());

    public Task Deactivate(DateTimeOffset timeDeactivated) =>
        factory.DB.Users.Update(record, u => u.TimeDeactivated = timeDeactivated);

    public Task Reactivate() =>
        factory.DB.Users.Update(record, u => u.TimeDeactivated = DateTimeOffset.MaxValue);

    public Task Edit(PersonName name, EmailAddress email)
    {
        if (name.IsBlank())
        {
            name = new PersonName(record.UserName);
        }
        return factory.DB.Users.Update
        (
            record,
            u =>
            {
                u.Name = name.Value;
                u.Email = email.Value;
            }
        );
    }

    public async Task DeleteAuthenticator(AuthenticatorKey authenticatorKey, string externalUserKey)
    {
        var authenticatorIDs = QueryAuthenticatorIDs(authenticatorKey);
        var userAuthenticator = await GetUserAuthenticator(authenticatorIDs, externalUserKey);
        if (userAuthenticator != null)
        {
            await factory.DB.UserAuthenticators.Delete(userAuthenticator);
        }
    }

    private Task<UserAuthenticatorEntity?> GetUserAuthenticator(IQueryable<int> authenticatorIDs, string externalUserKey) =>
        factory.DB
            .UserAuthenticators.Retrieve()
            .Where
            (
                ua =>
                    authenticatorIDs.Contains(ua.AuthenticatorID)
                    && ua.UserID == ID
                    && ua.ExternalUserKey == externalUserKey
            )
            .FirstOrDefaultAsync();

    public async Task<AuthenticatorModel> AddAuthenticator(AuthenticatorKey authenticatorKey, string externalUserKey)
    {
        var authenticatorIDs = QueryAuthenticatorIDs(authenticatorKey);
        var userAuthenticator = await GetUserAuthenticator(authenticatorIDs);
        int authenticatorID;
        if (userAuthenticator == null)
        {
            authenticatorID = await authenticatorIDs.FirstAsync();
            userAuthenticator = new UserAuthenticatorEntity
            {
                AuthenticatorID = authenticatorID,
                UserID = ID,
                ExternalUserKey = externalUserKey
            };
            await factory.DB.UserAuthenticators.Create(userAuthenticator);
        }
        else
        {
            authenticatorID = userAuthenticator.AuthenticatorID;
            await factory.DB.UserAuthenticators.Update
            (
                userAuthenticator,
                ua => ua.ExternalUserKey = externalUserKey
            );
        }
        return new AuthenticatorModel(authenticatorID, authenticatorKey);
    }

    private IQueryable<int> QueryAuthenticatorIDs(AuthenticatorKey authenticatorKey) =>
        factory.DB
            .Authenticators.Retrieve()
            .Where(a => a.AuthenticatorKey == authenticatorKey.Value)
            .Select(a => a.ID);

    private Task<UserAuthenticatorEntity?> GetUserAuthenticator(IQueryable<int> authenticatorIDs) =>
        factory.DB
            .UserAuthenticators.Retrieve()
            .Where
            (
                ua =>
                    authenticatorIDs.Contains(ua.AuthenticatorID)
                    && ua.UserID == ID
            )
            .FirstOrDefaultAsync();

    public async Task<UserAuthenticatorModel[]> Authenticators()
    {
        var joinedEntities = await factory.DB
            .UserAuthenticators.Retrieve()
            .Where
            (
                ua => ua.UserID == ID
            )
            .Join
            (
                factory.DB.Authenticators.Retrieve(),
                ua => ua.AuthenticatorID,
                a => a.ID,
                (ua, a) => new
                {
                    AuthenticatorID = a.ID,
                    a.AuthenticatorKey,
                    a.AuthenticatorName,
                    ua.ExternalUserKey
                }
            )
            .ToArrayAsync();
        return joinedEntities
            .Select
            (
                j => new UserAuthenticatorModel
                (
                    new AuthenticatorModel
                    (
                        j.AuthenticatorID,
                        new AuthenticatorKey(j.AuthenticatorName)
                    ),
                    j.ExternalUserKey
                )
            )
            .ToArray();
    }

    public async Task<AppPermission[]> GetAppPermissions(CancellationToken ct)
    {
        var apps = await factory.Apps.All(ct);
        var hubApp = apps.First(a => a.AppKeyEquals(HubInfo.AppKey));
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var appPermissions = new List<AppPermission>();
        var appsWithPermissions = apps
            .Where
            (
                a => !a.ToModel().AppKey.IsAnyAppType
                (
                    AppType.Values.NotFound,
                    AppType.Values.Package,
                    AppType.Values.WebPackage
                )
            );
        foreach (var app in appsWithPermissions)
        {
            var permission = await GetAppPermission(hubApp, appsModCategory, app, ct);
            appPermissions.Add(permission);
        }
        return appPermissions.ToArray();
    }

    public async Task<AppPermission> GetAppPermission(App app, CancellationToken ct)
    {
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var permission = await GetAppPermission(hubApp, appsModCategory, app, ct);
        return permission;
    }

    private async Task<AppPermission> GetAppPermission(App hubApp, ModifierCategory appsModCategory, App app, CancellationToken ct)
    {
        var appModel = app.ToModel();
        Modifier modifier;
        if
        (
            appModel.AppKey.IsUnknown() ||
            appModel.AppKey.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage)
        )
        {
            modifier = await hubApp.DefaultModifier(ct);
        }
        else
        {
            modifier = await appsModCategory.ModifierByTargetID(appModel.ID, ct);
        }
        var userRoles = await Modifier(modifier).AssignedRoles(ct);
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
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.AppViewerRoles)),
                CanEdit: userRoleModels
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.AppEditorRoles))
            );
        }
        return permission;
    }

    public Task<AppUserGroup> UserGroup(CancellationToken ct) => factory.UserGroups.UserGroup(record.GroupID, ct);

    public Task EditUserGroup(AppUserGroup userGroup, CancellationToken ct) =>
        factory.DB.Users.Update
        (
            record,
            u =>
            {
                u.GroupID = userGroup.ID;
            },
            ct
        );

    public async Task<AppUserGroupPermission[]> GetUserGroupPermissions(CancellationToken ct)
    {
        var userGroups = await factory.UserGroups.UserGroups(ct);
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct);
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var userGroupPermissions = new List<AppUserGroupPermission>();
        foreach (var userGroup in userGroups)
        {
            var userGroupPermission = await GetUserGroupPermission(userGroupsModCategory, userGroup, ct);
            userGroupPermissions.Add(userGroupPermission);
        }
        return userGroupPermissions.ToArray();
    }

    public async Task<AppUserGroupPermission> GetUserGroupPermission(AppUserGroup userGroup, CancellationToken ct)
    {
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct);
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var permission = await GetUserGroupPermission(userGroupsModCategory, userGroup, ct);
        return permission;
    }

    private async Task<AppUserGroupPermission> GetUserGroupPermission(ModifierCategory userGroupsModCategory, AppUserGroup userGroup, CancellationToken ct)
    {
        AppUserGroupPermission userGroupPermission;
        var userGroupModel = userGroup.ToModel();
        var modifier = await userGroupsModCategory.AddOrUpdateModifier(userGroupModel.PublicKey, userGroupModel.ID, userGroupModel.GroupName.DisplayText, ct);
        var userRoles = await Modifier(modifier).AssignedRoles(ct);
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
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.UserViewerRoles)),
                CanEdit: userRoleModels
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.UserEditorRoles))
            );
        }
        return userGroupPermission;
    }

    public async Task<LoggedInAppModel[]> GetLoggedInApps()
    {
        var userIDs = factory.DB.Users.Retrieve()
            .Where(u => u.UserName == new AppUserName(record.UserName).Value)
            .Select(u => u.ID);
        var sessionIDs = factory.DB.Sessions.Retrieve()
            .Where(s => userIDs.Contains(s.UserID) && s.TimeEnded.Year == 9999)
            .Select(s => s.ID);
        var installationIDs = factory.DB.Requests.Retrieve()
            .Where(r => sessionIDs.Contains(r.SessionID))
            .Select(r => r.InstallationID)
            .Distinct();
        var loggedInApps = await factory.DB.Installations.Retrieve()
            .Where(inst => installationIDs.Contains(inst.ID))
            .Join
            (
                factory.DB.AppVersions.Retrieve(),
                inst => inst.AppVersionID,
                av => av.ID,
                (inst, av) => new { inst.IsCurrent, inst.Domain, av.AppID, av.VersionID }
            )
            .Join
            (
                factory.DB.Apps.Retrieve()
                    .Where(a => a.Type == AppType.Values.WebApp),
                joined => joined.AppID,
                a => a.ID,
                (joined, a) => new { joined.IsCurrent, joined.Domain, AppDisplayText = a.DisplayText, joined.VersionID }
            )
            .Join
            (
                factory.DB.Versions.Retrieve(),
                joined => joined.VersionID,
                v => v.ID,
                (joined, v) => new { joined.IsCurrent, joined.Domain, joined.AppDisplayText, v.VersionKey }
            )
            .Distinct()
            .ToArrayAsync();
        return loggedInApps
            .Select
            (
                joined => new LoggedInAppModel
                (
                    new AppName(joined.AppDisplayText),
                    joined.IsCurrent ? AppVersionKey.Current : AppVersionKey.Parse(joined.VersionKey),
                    joined.Domain
                )
            )
            .Distinct()
            .ToArray();
    }

    public Task LoggedIn(DateTimeOffset timeLoggedIn) =>
        factory.DB.Users.Update
        (
            record,
            u =>
            {
                u.TimeLoggedIn = timeLoggedIn;
            }
        );

    public AppUserModel ToModel() =>
        new AppUserModel
        (
            ID: ID,
            UserName: new AppUserName(record.UserName),
            Name: new PersonName(record.Name),
            Email: new EmailAddress(record.Email).DisplayText,
            TimeDeactivated: record.TimeDeactivated
        );

    public override string ToString() => $"{nameof(AppUser)} {ID}";

}