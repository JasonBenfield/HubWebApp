using System.Text.Json;
using XTI_Hub;
using XTI_Tool;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class VersionOutput
{
    public void Output(AppVersionModel versionModel, string outputPath)
    {
        var version = new Version(versionModel.Major, versionModel.Minor, versionModel.Patch);
        var nextVersion = new Version(versionModel.Major, versionModel.Minor, versionModel.Patch + 1);
        var output = new VersionToolOutput
        {
            VersionKey = versionModel.VersionKey,
            VersionType = versionModel.VersionType.DisplayText,
            VersionNumber = version.ToString(3),
            DevVersionNumber = nextVersion.ToString(3)
        };
        new XtiProcessData().Output(output);
        if (!string.IsNullOrWhiteSpace(outputPath))
        {
            var dirPath = Path.GetDirectoryName(outputPath) ?? "";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            using var writer = new StreamWriter(outputPath, false);
            writer.WriteLine
            (
                JsonSerializer.Serialize
                (
                    new VersionRecord
                    {
                        Key = versionModel.VersionKey,
                        Type = versionModel.VersionType.DisplayText,
                        Version = versionModel.Version().ToString()
                    }
                )
            );
        }
    }

    private sealed class VersionRecord
    {
        public string Key { get; set; } = "";
        public string Type { get; set; } = "";
        public string Version { get; set; } = "";
    }

}