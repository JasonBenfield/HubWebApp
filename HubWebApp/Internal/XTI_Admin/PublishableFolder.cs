using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class PublishableFolder
{
    private readonly string folderPath;

    public PublishableFolder(string folderPath)
    {
        this.folderPath = folderPath;
    }

    public AppKey[] AppKeys()
    {
        var appKeys = new List<AppKey>();
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
        return appKeys.ToArray();
    }

    private AppKey? GetAppKey(string folderPath)
    {
        var folderName = new DirectoryInfo(folderPath).Name ?? "";
        AppType? appType;
        AppKey? appKey;
        var packageNames = new[] { "XTI_WebApp", "XTI_ConsoleApp", "XTI_ServiceApp", "SharedWebApp" };
        if (packageNames.Contains(folderName, StringComparer.OrdinalIgnoreCase))
        {
            appType = AppType.Values.Package;
            if(folderName.Equals("SharedWebApp", StringComparison.OrdinalIgnoreCase))
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