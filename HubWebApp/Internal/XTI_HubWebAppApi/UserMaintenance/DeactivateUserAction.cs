using XTI_Core;

namespace XTI_HubWebAppApi.UserMaintenance;

internal sealed class DeactivateUserAction : AppAction<int, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly IClock clock;

    public DeactivateUserAction(UserGroupFromPath userGroupFromPath, IClock clock)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.clock = clock;
    }

    public async Task<AppUserModel> Execute(int userID, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(userID);
        var userModel = user.ToModel();
        if (!userModel.IsActive())
        {
            throw new Exception($"User '{userModel.UserName}' has already been deactivated");
        }
        await user.Deactivate(clock.Now());
        return user.ToModel();
    }
}
