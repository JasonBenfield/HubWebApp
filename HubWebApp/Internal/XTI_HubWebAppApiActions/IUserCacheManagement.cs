namespace XTI_HubWebAppApiActions;

public interface IUserCacheManagement
{
    public Task ClearCache(AppUserName userName, CancellationToken ct);
}
