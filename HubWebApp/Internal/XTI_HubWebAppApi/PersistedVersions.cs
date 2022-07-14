using System.Text.Json;

namespace XTI_HubWebAppApi;

public sealed class PersistedVersions
{
    private readonly HubAppApi hubApi;
    private readonly AppKey appKey;
    private readonly string path;

    public PersistedVersions(HubAppApi hubApi, AppKey appKey, string path)
    {
        this.hubApi = hubApi;
        this.appKey = appKey;
        this.path = path;
    }

    public async Task Store()
    {
        var versions = await hubApi.Publish.GetVersions.Invoke(appKey);
        var serialized = JsonSerializer.Serialize
        (
            versions,
            new JsonSerializerOptions { WriteIndented = true }
        );
        var dir = Path.GetDirectoryName(path) ?? "";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        using var writer = new StreamWriter(path, false);
        await writer.WriteAsync(serialized);
    }
}