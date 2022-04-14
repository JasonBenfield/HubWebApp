using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_HubSetup;

public sealed class FileVersionReader : IVersionReader
{
    private readonly string path;

    public FileVersionReader(string path)
    {
        this.path = path;
    }

    public async Task<XtiVersionModel[]> Versions()
    {
        var versions = new XtiVersionModel[0];
        if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
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