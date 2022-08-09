namespace XTI_HubWebAppApi.System;

public sealed record GetUserContextRequest(string UserName);

internal sealed class GetUserContextAction : AppAction<GetUserContextRequest, UserContextModel>
{
    private readonly HubFactory hubFactory;
    private readonly ICurrentUserName currentUserName;
    private readonly AppFromSystemUser appFromSystemUser;

    public GetUserContextAction(HubFactory hubFactory, ICurrentUserName currentUserName, AppFromSystemUser appFromSystemUser)
    {
        this.hubFactory = hubFactory;
        this.currentUserName = currentUserName;
        this.appFromSystemUser = appFromSystemUser;
    }

    public async Task<UserContextModel> Execute(GetUserContextRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App();
        var userContext = new EfUserContext(hubFactory, appContextModel.App.AppKey, currentUserName);
        var user = await userContext.User(new AppUserName(model.UserName));
        return user;
    }
}
