namespace XTI_HubWebAppApi.System;

public sealed class GetAppContextAction : AppAction<GetAppContextRequest, AppContextModel>
{
    private readonly AppFromSystemUser appFromSystemUser;

    public GetAppContextAction(AppFromSystemUser appFromSystemUser)
    {
        this.appFromSystemUser = appFromSystemUser;
    }

    public Task<AppContextModel> Execute(GetAppContextRequest model, CancellationToken ct) =>
        appFromSystemUser.App(model.InstallationID);
}