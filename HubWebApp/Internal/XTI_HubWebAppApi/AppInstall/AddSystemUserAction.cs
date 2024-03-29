﻿namespace XTI_HubWebAppApi.AppInstall;

public sealed class AddSystemUserAction : AppAction<AddSystemUserRequest, AppUserModel>
{
    private readonly IHubAdministration hubAdmin;

    public AddSystemUserAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppUserModel> Execute(AddSystemUserRequest model, CancellationToken stoppingToken) =>
        hubAdmin.AddOrUpdateSystemUser(model.AppKey, model.MachineName, model.Password);
}