namespace XTI_HubWebAppApiActions.CurrentUser;

public sealed class GetUserAction : AppAction<EmptyRequest, AppUserModel>
{
    private readonly IUserContext userContext;

    public GetUserAction(IUserContext userContext)
    {
        this.userContext = userContext;
    }

    public Task<AppUserModel> Execute(EmptyRequest model, CancellationToken stoppingToken) =>
        userContext.User();
}
