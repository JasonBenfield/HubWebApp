using XTI_Core;

namespace XTI_HubWebAppApiActions.UserMaintenance;

public sealed class ReactivateUserAction : AppAction<int, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly IClock clock;

    public ReactivateUserAction(UserGroupFromPath userGroupFromPath, IClock clock)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.clock = clock;
    }

    public async Task<AppUserModel> Execute(int userID, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(userID, stoppingToken);
        await user.Reactivate(stoppingToken);
        return user.ToModel();
    }
}
