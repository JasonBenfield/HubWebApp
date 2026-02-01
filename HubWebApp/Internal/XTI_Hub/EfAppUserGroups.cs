using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppUserGroups
{
    private readonly EfHubDB factory;

    internal EfAppUserGroups(EfHubDB factory)
    {
        this.factory = factory;
    }

    public Task<EfAppUserGroup> GetXti(CancellationToken ct) => UserGroup(AppUserGroupName.XTI, ct);

    public Task<EfAppUserGroup> GetGeneral(CancellationToken ct) => UserGroup(AppUserGroupName.General, ct);

    public Task<EfAppUserGroup> UserGroup(int id, CancellationToken ct) =>
        factory.Context.UserGroups.Retrieve()
            .Where(ug => ug.ID == id)
            .Select(ug => new EfAppUserGroup(factory, ug))
            .FirstAsync(ct);

    public Task<EfAppUserGroup> UserGroup(AppUserGroupName name, CancellationToken ct) =>
        factory.Context.UserGroups.Retrieve()
            .Where(ug => ug.GroupName == name.Value)
            .Select(ug => new EfAppUserGroup(factory, ug))
            .FirstAsync(ct);

    public Task<EfAppUserGroup[]> UserGroups(CancellationToken ct) =>
        factory.Context.UserGroups.Retrieve()
            .Select(ug => new EfAppUserGroup(factory, ug))
            .ToArrayAsync(ct);

    internal Task<EfAppUserGroup> AddXtiIfNotExists(CancellationToken ct) => AddIfNotExists(AppUserGroupName.XTI, ct);

    internal Task<EfAppUserGroup> AddGeneralIfNotExists(CancellationToken ct) => AddIfNotExists(AppUserGroupName.General, ct);

    public async Task<EfAppUserGroup> AddIfNotExists(AppUserGroupName groupName, CancellationToken ct)
    {
        EfAppUserGroup? userGroup = null;
        await factory.Context.Transaction
        (
            async () => userGroup = await _AddIfNotExists(groupName, ct)
        );
        return userGroup ?? throw new ArgumentNullException(nameof(userGroup));
    }

    private async Task<EfAppUserGroup> _AddIfNotExists(AppUserGroupName groupName, CancellationToken ct)
    {
        var entity = await factory.Context.UserGroups.Retrieve().FirstOrDefaultAsync(ug => ug.GroupName == groupName.Value);
        if (entity == null)
        {
            entity = new UserGroupEntity
            {
                GroupName = groupName.Value,
                DisplayText = groupName.DisplayText
            };
            await factory.Context.UserGroups.Create(entity);
        }
        var userGroup = new EfAppUserGroup(factory, entity);
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
