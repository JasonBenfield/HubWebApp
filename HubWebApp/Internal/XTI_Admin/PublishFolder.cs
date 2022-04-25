using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class PublishFolder
{
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly AppVersionName versionName;

    public PublishFolder(XtiFolder xtiFolder, XtiEnvironment xtiEnv, AppVersionNameAccessor versionNameAccessor)
    {
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
        versionName = versionNameAccessor.Value;
    }

    public string VersionDir() => Path.Combine(xtiFolder.FolderPath(), xtiEnv.EnvironmentName, versionName.Value);

    public string VersionsPath() => Path.Combine(VersionDir(), "versions.json");

    public void TryCreateVersionDir()
    {
        var dir = Path.GetDirectoryName(VersionsPath()) ?? "";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    public string AppDir(AppKey appKey, AppVersionKey versionKey) =>
        Path.Combine
        (
            VersionDir(),
            $"{appKey.Name.DisplayText}{appKey.Type.DisplayText}".Replace(" ", ""),
            xtiEnv.IsProduction() ? versionKey.DisplayText : AppVersionKey.Current.DisplayText
        );
}
