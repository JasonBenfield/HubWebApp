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

    public Task<AppUser[]> SystemUsers() =>
        factory.DB
            .Users
            .Retrieve()
            .Where(u => u.UserName.StartsWith("xti_sys_"))
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public Task<AppUser[]> SystemUsers(AppKey appKey) =>
        factory.DB
            .Users
            .Retrieve()
            .Where(u => u.UserName.StartsWith(AppUserName.SystemUser(appKey, "").Value))
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public async Task<AppUser> AddOrUpdateSystemUser(AppKey appKey, string machineName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var systemUser = await SystemUserOrAnon(appKey, machineName);
        if (systemUser.UserName().Equals(AppUserName.SystemUser(appKey, machineName)))
        {
            await systemUser.ChangePassword(hashedPassword);
        }
        else
        {
            systemUser = await AddSystemUser
            (
                appKey,
                machineName,
                hashedPassword,
                now
            );
        }
        var app = await factory.Apps.App(appKey);
        var selfAdminRole = await app.AddRoleIfNotFound(AppRoleName.Admin);
        await systemUser.AssignRole(selfAdminRole);
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        if (hubApp.Key().Equals(HubInfo.AppKey))
        {
            var hubSystemRole = await hubApp.AddRoleIfNotFound(AppRoleName.System);
            await systemUser.AssignRole(hubSystemRole);
            var viewUserRole = await hubApp.AddRoleIfNotFound(HubInfo.Roles.ViewUser);
            await systemUser.AssignRole(viewUserRole);
            var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var appModifier = await appModCategory.AddOrUpdateModifier(app.ID, app.Title);
            var hubAdmin = await hubApp.AddRoleIfNotFound(AppRoleName.Admin);
            await systemUser.Modifier(appModifier).AssignRole(hubAdmin);
            var addStoredObject = await hubApp.AddRoleIfNotFound(HubInfo.Roles.AddStoredObject);
            await systemUser.AssignRole(addStoredObject);
        }
        return systemUser;
    }

    public Task<AppUser> SystemUserOrAnon(AppKey appKey, string machineName) =>
        factory.Users.UserOrAnon(AppUserName.SystemUser(appKey, machineName));

    private Task<AppUser> AddSystemUser
    (
        AppKey appKey,
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    ) =>
        factory.Users.AddOrUpdate
        (
            AppUserName.SystemUser(appKey, machineName),
            password,
            new PersonName($"{appKey.Name.DisplayText.Replace(" ", "")} {appKey.Type.DisplayText.Replace(" ", "")} {machineName}"),
            new EmailAddress(""),
            timeAdded
        );
}