using System.Text.Json;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class PersistedVersions
{
    private readonly string path;

    public PersistedVersions(string path)
    {
        this.path = path;
    }

    public async Task Store(XtiVersionModel[] versions)
    {
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