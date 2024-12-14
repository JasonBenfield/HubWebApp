namespace XTI_HubWebAppApiActions.System;

public sealed class SystemStorageName
{
    private readonly ICurrentUserName currentUserName;
    private readonly string storageName;

    public SystemStorageName(ICurrentUserName currentUserName, string storageName)
    {
        this.currentUserName = currentUserName;
        this.storageName = storageName;
    }

    public async Task<StorageName> Value()
    {
        var userName = await currentUserName.Value();
        var systemUserName = SystemUserName.Parse(userName);
        var systemStorageName = $"XTI App[{systemUserName.AppKey.Serialize()}]{storageName}";
        return new StorageName(systemStorageName);
    }
}
