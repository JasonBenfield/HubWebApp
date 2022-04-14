using System.Text.Json;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class PersistedVersions
{
    private readonly AppFactory appFactory;
    private readonly AppKey appKey;
    private readonly string path;

    public PersistedVersions(AppFactory appFactory, AppKey appKey, string path)
    {
        this.appFactory = appFactory;
        this.appKey = appKey;
        this.path = path;
    }

    public async Task Store()
    {
        var app = await appFactory.Apps.App(appKey);
        var versions = await app.Versions();
        var versionModels = versions.Select(v => v.ToModel()).ToArray();
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