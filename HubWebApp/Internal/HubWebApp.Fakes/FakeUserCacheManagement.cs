using XTI_App.Abstractions;
using XTI_HubWebAppApiActions;

namespace HubWebApp.Fakes;

internal sealed class FakeUserCacheManagement : IUserCacheManagement
{
    public Task ClearCache(AppUserName userName, CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
