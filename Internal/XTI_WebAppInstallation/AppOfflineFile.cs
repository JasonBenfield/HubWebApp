using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;

namespace XTI_WebAppInstallation;

public sealed class AppOfflineFile
{
    public static readonly string FileName = "app_offline.htm";

    public AppOfflineFile(XtiFolder xtiFolder, AppKey appKey, AppVersionKey versionKey)
    {
        var installDir = xtiFolder.InstallPath(appKey, versionKey);
        FilePath = Path.Combine(installDir, FileName);
    }

    public string FilePath { get; }

    public async Task Write()
    {
        using (var writer = new StreamWriter(FilePath, false))
        {
            await writer.WriteAsync
            (
@"
<html>
    <head>
        <title>App Offline</title>
    </head>
    <body>
        <h1>Web App is Temporarily Offline</h1>
    </body>
</html>
"
            );
        }
    }

    public void Delete()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }
}
