namespace XTI_HubWebAppApi;

public interface IUserCacheManagement
{
    public Task ClearCache(AppUserName userName, CancellationToken ct);
}
