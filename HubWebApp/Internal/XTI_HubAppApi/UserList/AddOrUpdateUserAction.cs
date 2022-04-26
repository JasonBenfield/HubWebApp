﻿using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.UserList;

public sealed class AddOrUpdateUserAction : AppAction<AddUserModel, int>
{
    private readonly AppFactory appFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public AddOrUpdateUserAction(AppFactory appFactory, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public async Task<int> Execute(AddUserModel model)
    {
        var userName = new AppUserName(model.UserName);
        var hashedPassword = hashedPasswordFactory.Create(model.Password);
        var timeAdded = clock.Now();
        var user = await appFactory.Users.AddOrUpdate
        (
            userName,
            hashedPassword,
            new PersonName(model.PersonName),
            new EmailAddress(model.Email),
            timeAdded
        );
        return user.ID.Value;
    }
}