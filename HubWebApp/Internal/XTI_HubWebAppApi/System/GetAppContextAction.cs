namespace XTI_HubWebAppApi.System;

public sealed class GetAppContextAction : AppAction<GetAppContextRequest, AppContextModel>
{
    private readonly AppFromSystemUser appFromSystemUser;

    public GetAppContextAction(AppFromSystemUser appFromSystemUser)
    {
        this.appFromSystemUser = appFromSystemUser;
    }

    public Task<AppContextModel> Execute(GetAppContextRequest getRequest, CancellationToken ct) =>
        appFromSystemUser.App(getRequest.InstallationID);
}