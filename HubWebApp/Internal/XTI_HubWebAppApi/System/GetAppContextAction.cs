namespace XTI_HubWebAppApi.System;

public sealed class GetAppContextAction : AppAction<EmptyRequest, AppContextModel>
{
    private readonly AppFromSystemUser appFromSystemUser;

    public GetAppContextAction(AppFromSystemUser appFromSystemUser)
    {
        this.appFromSystemUser = appFromSystemUser;
    }

    public async Task<AppContextModel> Execute(EmptyRequest model, CancellationToken ct)
    {
        var appContextModel = await appFromSystemUser.App();
        return appContextModel;
    }
}