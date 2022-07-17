using XTI_HubDB.Entities;

namespace XTI_HubWebAppApi.UserGroups;

public sealed class AddUserGroupIfNotExistsAction : AppAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>
{
    private readonly IHubDbContext db;
    private readonly HubFactory hubFactory;

    public AddUserGroupIfNotExistsAction(IHubDbContext db, HubFactory hubFactory)
    {
        this.db = db;
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserGroupModel> Execute(AddUserGroupIfNotExistsRequest model, CancellationToken ct)
    {
        var userGroup = await hubFactory.UserGroups.AddIfNotExists(new AppUserGroupName(model.GroupName));
        return userGroup.ToModel();
    }
}