namespace XTI_HubWebAppApi.System;

public sealed record GetUserContextRequest(string UserName);

internal sealed class GetUserContextAction : AppAction<GetUserContextRequest, UserContextModel>
{
    private readonly ISourceUserContext userContext;

    public GetUserContextAction(ISourceUserContext userContext)
    {
        this.userContext = userContext;
    }

    public Task<UserContextModel> Execute(GetUserContextRequest model, CancellationToken stoppingToken) =>
        userContext.User(new AppUserName(model.UserName));
}
