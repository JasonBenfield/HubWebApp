﻿namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddAdminUserAction : AppAction<AddAdminUserRequest, AppUserModel>
{
    private readonly IHubAdministration hubAdmin;

    public AddAdminUserAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppUserModel> Execute(AddAdminUserRequest addRequest, CancellationToken stoppingToken) =>
        hubAdmin.AddOrUpdateAdminUser
        (
            addRequest.AppKey.ToAppKey(),
            addRequest.ToAppUserName(),
            addRequest.Password,
            stoppingToken
        );
}
