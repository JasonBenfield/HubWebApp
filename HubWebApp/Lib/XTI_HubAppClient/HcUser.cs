﻿namespace XTI_HubAppClient;

internal sealed class HcUser : IAppUser
{
    private readonly HubAppClient hubClient;
    private readonly HcAppContext appContext;
    private readonly AppUserName userName;

    public HcUser(HubAppClient hubClient, HcAppContext appContext, AppUserModel model)
    {
        this.hubClient = hubClient;
        this.appContext = appContext;
        ID = model.ID;
        userName = new AppUserName(model.UserName);
    }

    public int ID { get; }
    public AppUserName UserName() => userName;

    public async Task<IAppRole[]> Roles(IModifier modifier)
    {
        var appModifier = await appContext.GetModifierKey();
        var request = new UserModifierKey
        {
            UserID = ID,
            ModifierID = modifier.ID
        };
        var userAccess = await hubClient.AppUser.GetUserAccess(appModifier, request);
        return userAccess.AssignedRoles.Select(r => new HcRole(r)).ToArray();
    }
}