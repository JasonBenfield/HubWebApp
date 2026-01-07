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

    public Task<AppUserGroup> GetXti(CancellationToken ct) => UserGroup(AppUserGroupName.XTI, ct);

    public Task<AppUserGroup> GetGeneral(CancellationToken ct) => UserGroup(AppUserGroupName.General, ct);

    public Task<AppUserGroup> UserGroup(int id, CancellationToken ct) =>
        factory.DB.UserGroups.Retrieve()
            .Where(ug => ug.ID == id)
            .Select(ug => new AppUserGroup(factory, ug))
            .FirstAsync(ct);

    public Task<AppUserGroup> UserGroup(AppUserGroupName name, CancellationToken ct) =>
        factory.DB.UserGroups.Retrieve()
            .Where(ug => ug.GroupName == name.Value)
            .Select(ug => new AppUserGroup(factory, ug))
            .FirstAsync(ct);

    public Task<AppUserGroup[]> UserGroups(CancellationToken ct) =>
        factory.DB.UserGroups.Retrieve()
            .Select(ug => new AppUserGroup(factory, ug))
            .ToArrayAsync(ct);

    internal Task<AppUserGroup> AddXtiIfNotExists(CancellationToken ct) => AddIfNotExists(AppUserGroupName.XTI, ct);

    internal Task<AppUserGroup> AddGeneralIfNotExists(CancellationToken ct) => AddIfNotExists(AppUserGroupName.General, ct);

    public async Task<AppUserGroup> AddIfNotExists(AppUserGroupName groupName, CancellationToken ct)
    {
        AppUserGroup? userGroup = null;
        await factory.DB.Transaction
        (
            async () => userGroup = await _AddIfNotExists(groupName, ct)
        );
        return userGroup ?? throw new ArgumentNullException(nameof(userGroup));
    }

    private async Task<AppUserGroup> _AddIfNotExists(AppUserGroupName groupName, CancellationToken ct)
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
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey, ct);
        if (hubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var userGroupsModCategory = await hubApp.AddOrUpdateModCategory(HubInfo.ModCategories.UserGroups, ct);
            await userGroupsModCategory.AddOrUpdateModifier
            (
                userGroupModel.PublicKey,
                userGroupModel.ID,
                userGroupModel.GroupName.DisplayText,
                ct
            );
        }
        return userGroup;
    }
}
