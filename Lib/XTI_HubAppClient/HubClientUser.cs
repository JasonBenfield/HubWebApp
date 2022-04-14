namespace XTI_HubAppClient;

internal sealed class HubClientUser : IAppUser
{
    private readonly HubAppClient hubClient;
    private readonly HubClientAppContext appContext;
    private readonly AppUserName userName;

    public HubClientUser(HubAppClient hubClient, HubClientAppContext appContext, AppUserModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        ID = new EntityID(model.ID);
        userName = new AppUserName(model.UserName);
    }

    public EntityID ID { get; }
    public AppUserName UserName() => userName;

    public async Task<IAppRole[]> Roles(IModifier modifier)
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new UserModifierKey
        {
            UserID = ID.Value,
            ModifierID = modifier.ID.Value
        };
        var userAccess = await hubClient.AppUser.GetUserAccess(appModifier, request);
        return userAccess.AssignedRoles.Select(r => new HubClientRole(r)).ToArray();
    }
}