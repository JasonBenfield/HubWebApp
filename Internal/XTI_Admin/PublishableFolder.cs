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
            if(appKey != null)
            {
                appKeys.Add(appKey);
            }
        }
        return appKeys.ToArray();
    }

    private AppKey? GetAppKey(string folderPath)
    {
        var folderName = Path.GetDirectoryName(folderPath) ?? "";
        var appType = AppType.Values.GetAll()
            .FirstOrDefault
            (
                type => folderName.EndsWith(type.DisplayText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)
            );
        int typeLength;
        if (appType == null)
        {
            if (Directory.GetDirectories(folderName, "Lib").Any())
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