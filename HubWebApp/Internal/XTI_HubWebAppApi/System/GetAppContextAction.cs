namespace XTI_HubWebAppApi.System;

public sealed class GetAppContextRequest
{
    public string VersionKey { get; set; } = "";
}

public sealed class GetAppContextAction : AppAction<GetAppContextRequest, AppContextModel>
{
    private readonly AppFromSystemUser appFromSystemUser;

    public GetAppContextAction(AppFromSystemUser appFromSystemUser)
    {
        this.appFromSystemUser = appFromSystemUser;
    }

    public async Task<AppContextModel> Execute(GetAppContextRequest model, CancellationToken ct)
    {
        var appContextModel = await appFromSystemUser.App(AppVersionKey.Parse(model.VersionKey));
        return appContextModel;
    }
}