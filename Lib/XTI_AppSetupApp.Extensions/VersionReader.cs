using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using XTI_HubAppClient;

namespace XTI_AppSetupApp.Extensions
{
    public sealed class VersionReader
    {
        private readonly string path;

        public VersionReader(string path)
        {
            this.path = path;
        }

        public async Task<AppVersionModel[]> Versions()
        {
            var versions = new AppVersionModel[] { };
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    var serialized = await reader.ReadToEndAsync();
                    if (!string.IsNullOrWhiteSpace(serialized))
                    {
                        var jsonOptions = new JsonSerializerOptions();
                        versions = JsonSerializer.Deserialize<AppVersionModel[]>(serialized, jsonOptions);
                    }
                }
            }
            return versions;
        }
    }
}
