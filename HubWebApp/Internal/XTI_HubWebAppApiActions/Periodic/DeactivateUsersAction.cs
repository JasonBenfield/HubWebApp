using XTI_Core;

namespace XTI_HubWebAppApiActions.Periodic;

public sealed class DeactivateUsersAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;
    private readonly HubWebAppOptions options;
    private readonly IClock clock;

    public DeactivateUsersAction(HubFactory hubFactory, HubWebAppOptions options, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.options = options;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var users = await hubFactory.Users.UsersLoggedInBefore(clock.Now().AddDays(-options.Login.DaysBeforeDeactivation));
        foreach (var user in users)
        {
            await user.Deactivate(clock.Now());
        }
        return new EmptyActionResult();
    }
}
