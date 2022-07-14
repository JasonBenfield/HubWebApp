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
            .Where(u => u.UserName.StartsWith($"xti_sys[{appKey.Serialize()}]"))
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public async Task<AppUser> AddOrUpdateSystemUser(SystemUserName systemUserName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var systemUser = await SystemUserOrAnon(systemUserName);
        if (systemUser.UserName().Equals(systemUserName.Value))
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
        var selfAdminRole = await app.AddRoleIfNotFound(AppRoleName.Admin);
        await systemUser.AssignRole(selfAdminRole);
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        if (hubApp.Key().Equals(HubInfo.AppKey))
        {
            var hubSystemRole = await hubApp.AddRoleIfNotFound(AppRoleName.System);
            await systemUser.AssignRole(hubSystemRole);
            var viewUserRole = await hubApp.AddRoleIfNotFound(HubInfo.Roles.ViewUser);
            await systemUser.AssignRole(viewUserRole);
            var appModCategory = await hubApp.AddModCategoryIfNotFound(HubInfo.ModCategories.Apps);
            var appModifier = await appModCategory.AddOrUpdateModifier(app.ID, app.Title);
            var hubAdmin = await hubApp.AddRoleIfNotFound(AppRoleName.Admin);
            await systemUser.Modifier(appModifier).AssignRole(hubAdmin);
            var addStoredObject = await hubApp.AddRoleIfNotFound(HubInfo.Roles.AddStoredObject);
            await systemUser.AssignRole(addStoredObject);
        }
        return systemUser;
    }

    public Task<AppUser> SystemUserOrAnon(SystemUserName systemUserName) =>
        factory.Users.UserOrAnon(systemUserName.Value);

    private Task<AppUser> AddSystemUser
    (
        SystemUserName systemUserName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    ) =>
        factory.Users.AddOrUpdate
        (
            systemUserName.Value,
            password,
            new PersonName(systemUserName.Value.DisplayText),
            new EmailAddress(""),
            timeAdded
        );
}