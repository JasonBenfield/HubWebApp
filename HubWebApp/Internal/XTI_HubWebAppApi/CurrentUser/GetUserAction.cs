namespace XTI_HubWebAppApi.CurrentUser;

internal sealed class GetUserAction : AppAction<EmptyRequest, AppUserModel>
{
    private readonly IUserContext userContext;

    public GetUserAction(IUserContext userContext)
    {
        this.userContext = userContext;
    }

    public async Task<AppUserModel> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var userContextModel = await userContext.User();
        return userContextModel.User;
    }
}
