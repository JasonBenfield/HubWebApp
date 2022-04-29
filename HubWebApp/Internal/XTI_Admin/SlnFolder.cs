using System.Text.Json;
using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Admin;

public sealed class SlnFolder
{
    private readonly List<SlnAppFolder> appFolders = new();
    private readonly XtiEnvironment xtiEnv;

    public SlnFolder(XtiEnvironment xtiEnv, string folderPath)
    {
        this.xtiEnv = xtiEnv;
        foreach (var folder in Directory.GetDirectories(folderPath))
        {
            var appKey = GetAppKey(folder);
            if (appKey != null)
            {
                var installOptions = getInstallOptions(folder);
                appFolders.Add(new SlnAppFolder(appKey, installOptions));
            }
        }
        if (!appFolders.Any())
        {
            var appKey = GetAppKey(folderPath);
            if (appKey != null)
            {
                var installOptions = getInstallOptions(folderPath);
                appFolders.Add(new SlnAppFolder(appKey, installOptions));
            }
        }
    }

    private InstallOptions getInstallOptions(string folder)
    {
        var installOptions = new InstallOptions
        (
            new InstallationOptions[] { new InstallationOptions("", "", "") },
            999
        );
        var installConfigPath = Path.Combine(folder, $"install.{xtiEnv.EnvironmentName}.private.json");
        if (File.Exists(installConfigPath))
        {
            var serialized = File.ReadAllText(installConfigPath);
            var deserialized = JsonSerializer.Deserialize<InstallOptions>(serialized);
            if (deserialized != null)
            {
                installOptions = deserialized;
            }
        }
        return installOptions;
    }

    internal SlnAppFolder[] Folders() => appFolders.ToArray();

    internal SlnAppFolder? Folder(AppKey appKey) => appFolders.FirstOrDefault(af => af.AppKey.Equals(appKey));

    public AppKey[] AppKeys() => appFolders.OrderBy(f => f.InstallOptions.Sequence).Select(af => af.AppKey).ToArray();

    private AppKey? GetAppKey(string folderPath)
    {
        var folderName = new DirectoryInfo(folderPath).Name ?? "";
        AppType? appType;
        AppKey? appKey;
        var packageNames = new[] { "XTI_WebApp", "XTI_ConsoleApp", "XTI_ServiceApp", "SharedWebApp" };
        if (packageNames.Contains(folderName, StringComparer.OrdinalIgnoreCase))
        {
            appType = AppType.Values.Package;
            if (folderName.Equals("SharedWebApp", StringComparison.OrdinalIgnoreCase))
            {
                folderName = "Shared";
            }
            appKey = new AppKey(new AppName(folderName), AppType.Values.Package);
        }
        else
        {
            appType = AppType.Values.GetAll()
                .FirstOrDefault
                (
                    type => folderPath.EndsWith(type.DisplayText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)
                );
            int typeLength;
            if (appType == null)
            {
                if (Directory.GetDirectories(folderPath, "Lib").Any())
                {
                    appType = AppType.Values.Package;
                }
                typeLength = 0;
            }
            else
            {
                typeLength = appType.DisplayText.Replace(" ", "").Length;
            }
            if (appType == null)
            {
                appKey = null;
            }
            else
            {
                var appName = folderName.Remove(folderName.Length - typeLength);
                appKey = new AppKey(appName, appType);
            }
        }
        return appKey;
    }
}