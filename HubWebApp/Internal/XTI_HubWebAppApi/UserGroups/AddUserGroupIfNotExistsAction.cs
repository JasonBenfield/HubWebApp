namespace XTI_HubWebAppApi.UserGroups;

public sealed class AddUserGroupIfNotExistsAction : AppAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>
{
    private readonly HubFactory hubFactory;

    public AddUserGroupIfNotExistsAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserGroupModel> Execute(AddUserGroupIfNotExistsRequest model, CancellationToken ct)
    {
        var userGroup = await hubFactory.UserGroups.AddIfNotExists(new AppUserGroupName(model.GroupName));
        return userGroup.ToModel();
    }
}