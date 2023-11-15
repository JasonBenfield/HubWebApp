using XTI_App.Abstractions;
using XTI_Core;
using XTI_ServiceAppInstallation;
using XTI_WebAppInstallation;

namespace XTI_Admin;

internal sealed class RestartAppCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly RemoteCommandService remoteCommandService;

    public RestartAppCommand(AdminOptions options, XtiEnvironment xtiEnv, XtiFolder xtiFolder, RemoteCommandService remoteCommandService)
    {
        this.options = options;
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.remoteCommandService = remoteCommandService;
    }

    public async Task Execute()
    {
        var appKey = options.AppKey();
        if (string.IsNullOrWhiteSpace(options.DestinationMachine))
        {
            if (appKey.IsAppType(AppType.Values.WebApp))
            {
                var versionKey = AppVersionKey.Parse(options.VersionKey);
                if (versionKey.Equals(AppVersionKey.None))
                {
                    var versionKeys = Versions(appKey);
                    if (versionKeys.Any())
                    {
                        var versionKeyArrays = SplitVersionKeys(versionKeys);
                        if (versionKeyArrays.Length > 1)
                        {
                            var tasks = new List<Task>();
                            foreach(var versionKeyArr in versionKeyArrays)
                            {
                                var t = Task.Run
                                (
                                    async () =>
                                    {
                                        foreach (var specificVersionKey in versionKeyArr)
                                        {
                                            await RestartWebApp(appKey, specificVersionKey);
                                        }
                                    }
                                );
                                tasks.Add(t);
                            }
                            await Task.WhenAll(tasks);
                        }
                        else
                        {
                            foreach (var specificVersionKey in versionKeyArrays[0])
                            {
                                await RestartWebApp(appKey, specificVersionKey);
                            }
                        }
                    }
                }
                else
                {
                    await RestartWebApp(appKey, versionKey);
                }
            }
            else if (appKey.IsAppType(AppType.Values.ServiceApp))
            {
                var serviceApp = new WinServiceApp(xtiEnv, appKey);
                await serviceApp.RestartService();
            }
        }
        else
        {
            var remoteOptions = options.Copy();
            remoteOptions.DestinationMachine = "";
            await remoteCommandService.Run
            (
                options.DestinationMachine,
                CommandNames.RestartApp.ToString(),
                remoteOptions
            );
        }
    }

    private async Task RestartWebApp(AppKey appKey, AppVersionKey versionKey)
    {
        var appOfflineFile = new AppOfflineFile(xtiFolder, appKey, versionKey);
        await appOfflineFile.Write();
        await Task.Delay(TimeSpan.FromSeconds(5));
        appOfflineFile.Delete();
    }

    private AppVersionKey[] Versions(AppKey appKey) =>
        Directory.GetDirectories
        (
            xtiFolder.InstallPath
            (
                appKey.Name.DisplayText.Replace(" ", ""),
                appKey.Type.DisplayText.Replace(" ", "")
            )
        )
        .Select(dir => new DirectoryInfo(dir).Name)
        .Where
        (
            dir =>
                dir.Equals("Current", StringComparison.OrdinalIgnoreCase) ||
                dir.StartsWith("V", StringComparison.OrdinalIgnoreCase)
        )
        .Select
        (
            dir =>
            {
                AppVersionKey versionKey;
                try
                {
                    versionKey = AppVersionKey.Parse(dir);
                }
                catch (Exception)
                {
                    versionKey = AppVersionKey.None;
                }
                return versionKey;
            }
        )
        .Where(v => !v.Equals(AppVersionKey.None))
        .ToArray();

    private AppVersionKey[][] SplitVersionKeys(AppVersionKey[] versionKeys)
    {
        const int max = 5;
        var versionKeyArrays = new List<AppVersionKey[]>();
        int i = 0;
        while (i < versionKeys.Length)
        {
            versionKeyArrays.Add(versionKeys.Skip(i * max).Take(max).ToArray());
            i += max;
        }
        return versionKeyArrays.ToArray();
    }

}
