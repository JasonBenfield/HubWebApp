using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class SystemUserRepository
{
    private readonly HubFactory factory;

    internal SystemUserRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUser[]> SystemUsers(AppKey appKey) =>
        factory.DB
            .Users
            .Retrieve()
            .Where(u => u.UserName.StartsWith($"xti_sys[{appKey.Serialize()}]") || u.UserName.StartsWith($"xti_sys2[{appKey.Serialize()}]"))
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public async Task<AppUser> AddOrUpdateSystemUser(SystemUserName systemUserName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var systemUser = await SystemUserOrAnon(systemUserName);
        if (systemUser.ToModel().UserName.Equals(systemUserName.UserName))
        {
            await systemUser.ChangePassword(hashedPassword);
        }
        else
        {
            systemUser = await AddSystemUser
            (
                systemUserName,
                hashedPassword,
                now
            );
        }
        var app = await factory.Apps.App(systemUserName.AppKey);
        var appModel = app.ToModel();
        var selfAdminRole = await app.AddOrUpdateRole(AppRoleName.Admin);
        await systemUser.AssignRole(selfAdminRole);
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        if (hubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var hubSystemRole = await hubApp.AddOrUpdateRole(AppRoleName.System);
            await systemUser.AssignRole(hubSystemRole);
            var viewUserRole = await hubApp.AddOrUpdateRole(HubInfo.Roles.ViewUser);
            await systemUser.AssignRole(viewUserRole);
            var appModCategory = await hubApp.AddOrUpdateModCategory(HubInfo.ModCategories.Apps);
            var appModifier = await appModCategory.AddOrUpdateModifier(appModel.PublicKey, appModel.ID, appModel.AppKey.Format());
            var hubAdmin = await hubApp.AddOrUpdateRole(AppRoleName.Admin);
            await systemUser.Modifier(appModifier).AssignRole(hubAdmin);
            var addStoredObject = await hubApp.AddOrUpdateRole(HubInfo.Roles.AddStoredObject);
            await systemUser.AssignRole(addStoredObject);
        }
        return systemUser;
    }

    public Task<AppUser> SystemUserOrAnon(SystemUserName systemUserName) =>
        factory.Users.UserOrAnon(systemUserName.UserName);

    private async Task<AppUser> AddSystemUser
    (
        SystemUserName systemUserName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    )
    {
        var xtiUserGroup = await factory.UserGroups.GetXti();
        var user = await xtiUserGroup.AddOrUpdate
        (
            systemUserName.UserName,
            password,
            new PersonName(systemUserName.UserName.DisplayText),
            new EmailAddress(""),
            timeAdded
        );
        return user;
    }
}