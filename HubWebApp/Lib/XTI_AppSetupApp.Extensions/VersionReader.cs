using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_AppSetupApp.Extensions;

public sealed class VersionReader
{
    private readonly string path;

    public VersionReader(string path)
    {
        this.path = path;
    }

    public async Task<XtiVersionModel[]> Versions()
    {
        var versions = new XtiVersionModel[0];
        if (File.Exists(path))
        {
            using (var reader = new StreamReader(path))
            {
                var serialized = await reader.ReadToEndAsync();
                if (!string.IsNullOrWhiteSpace(serialized))
                {
                    versions = XtiSerializer.Deserialize(serialized, () => new XtiVersionModel[0]);
                }
            }
        }
        return versions;
    }
}