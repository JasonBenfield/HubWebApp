using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserGroupRepository
{
    private readonly HubFactory factory;

    internal AppUserGroupRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUserGroup> GetXti() => UserGroup(AppUserGroupName.XTI);

    public Task<AppUserGroup> GetGeneral() => UserGroup(AppUserGroupName.General);

    public Task<AppUserGroup> UserGroup(int id) =>
        factory.DB.UserGroups.Retrieve()
            .Where(ug => ug.ID == id)
            .Select(ug => new AppUserGroup(factory, ug))
            .FirstAsync();

    public Task<AppUserGroup> UserGroup(AppUserGroupName name) =>
        factory.DB.UserGroups.Retrieve()
            .Where(ug => ug.GroupName == name.Value)
            .Select(ug => new AppUserGroup(factory, ug))
            .FirstAsync();

    public Task<AppUserGroup[]> UserGroups() =>
        factory.DB.UserGroups.Retrieve()
            .Select(ug => new AppUserGroup(factory, ug))
            .ToArrayAsync();

    internal Task<AppUserGroup> AddXtiIfNotExists() => AddIfNotExists(AppUserGroupName.XTI);

    internal Task<AppUserGroup> AddGeneralIfNotExists() => AddIfNotExists(AppUserGroupName.General);

    public async Task<AppUserGroup> AddIfNotExists(AppUserGroupName groupName)
    {
        AppUserGroup? userGroup = null;
        await factory.DB.Transaction
        (
            async () => userGroup = await _AddIfNotExists(groupName)
        );
        return userGroup ?? throw new ArgumentNullException(nameof(userGroup));
    }

    private async Task<AppUserGroup> _AddIfNotExists(AppUserGroupName groupName)
    {
        var entity = await factory.DB.UserGroups.Retrieve().FirstOrDefaultAsync(ug => ug.GroupName == groupName.Value);
        if (entity == null)
        {
            entity = new UserGroupEntity
            {
                GroupName = groupName.Value,
                DisplayText = groupName.DisplayText
            };
            await factory.DB.UserGroups.Create(entity);
        }
        var userGroup = new AppUserGroup(factory, entity);
        var userGroupModel = userGroup.ToModel();
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        if (hubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var userGroupsModCategory = await hubApp.AddModCategoryIfNotFound(HubInfo.ModCategories.UserGroups);
            await userGroupsModCategory.AddOrUpdateModifier
            (
                userGroupModel.PublicKey,
                userGroupModel.ID,
                userGroupModel.GroupName.DisplayText
            );
        }
        return userGroup;
    }
}
