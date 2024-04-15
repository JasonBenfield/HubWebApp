using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class SlnFolder
{
    private readonly List<AppKey> appKeys = new();

    public SlnFolder(string folderPath)
    {
        if(folderPath.Contains("\\src\\", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var folder in Directory.GetDirectories(folderPath))
            {
                var appKey = GetAppKey(folder);
                if (appKey != null)
                {
                    appKeys.Add(appKey);
                }
            }
            if (!appKeys.Any())
            {
                var appKey = GetAppKey(folderPath);
                if (appKey != null)
                {
                    appKeys.Add(appKey);
                }
            }
        }
    }

    public AppKey[] AppKeys() => appKeys.ToArray();

    private AppKey? GetAppKey(string folderPath)
    {
        var folderName = new DirectoryInfo(folderPath).Name ?? "";
        AppType? appType;
        if (folderName.EndsWith(AppType.Values.WebPackage.DisplayText.Replace(" ", "")))
        {
            appType = AppType.Values.WebPackage;
        }
        else
        {
            appType = AppType.Values.GetAll()
                .FirstOrDefault
                (
                    type => folderPath.EndsWith(type.DisplayText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)
                );
        }
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
        AppKey? appKey;
        if (appType == null)
        {
            appKey = null;
        }
        else
        {
            var appName = folderName.Remove(folderName.Length - typeLength);
            appKey = new AppKey(appName, appType);
        }
        return appKey;
    }
}