using System.Text.Json;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubSetup;

public sealed class VersionReader
{
    private readonly string path;

    public VersionReader(string path)
    {
        this.path = path;
    }

    public async Task<AppVersionModel[]> Versions()
    {
        var versions = new AppVersionModel[0];
        if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
        {
            using (var reader = new StreamReader(path))
            {
                var serialized = await reader.ReadToEndAsync();
                if (!string.IsNullOrWhiteSpace(serialized))
                {
                    versions = XtiSerializer.Deserialize(serialized, () => new AppVersionModel[0]);
                }
            }
        }
        return versions;
    }
}