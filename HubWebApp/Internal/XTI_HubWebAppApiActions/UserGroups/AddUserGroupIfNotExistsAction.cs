namespace XTI_HubWebAppApiActions.UserGroups;

public sealed class AddUserGroupIfNotExistsAction : AppAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>
{
    private readonly EfHubDB hubFactory;

    public AddUserGroupIfNotExistsAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserGroupModel> Execute(AddUserGroupIfNotExistsRequest model, CancellationToken ct)
    {
        var userGroup = await hubFactory.UserGroups.AddIfNotExists(new AppUserGroupName(model.GroupName), ct);
        return userGroup.ToModel();
    }
}