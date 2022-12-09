using Microsoft.Web.Administration;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;

namespace XTI_WebAppInstallation;

public sealed class IisWebSite
{
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly AppKey appKey;
    private readonly AppVersionKey versionKey;
    private readonly string siteName;

    public IisWebSite(XtiFolder xtiFolder, XtiEnvironment xtiEnv, AppKey appKey, AppVersionKey versionKey, string siteName)
    {
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
        this.appKey = appKey;
        this.versionKey = versionKey;
        this.siteName = siteName;
    }

    public async Task CreateOrUpdate(string userName, string password)
    {
        using var server = new ServerManager();
        if (string.IsNullOrWhiteSpace(siteName))
        {
            throw new ArgumentException("siteName is required");
        }
        var site = server.Sites[siteName];
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        var appPoolName = $"Xti_{xtiEnv.EnvironmentName}_{appName}_{versionKey.DisplayText}";
        var appPool = server.ApplicationPools
            .FirstOrDefault(ap => ap.Name.Equals(appPoolName, StringComparison.OrdinalIgnoreCase));
        if (appPool == null)
        {
            var newAppPool = server.ApplicationPools.Add(appPoolName);
            newAppPool.ProcessModel.UserName = userName;
            newAppPool.ProcessModel.Password = password;
            newAppPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
            newAppPool.ManagedRuntimeVersion = "v4.0";
        }
        var baseApp = site.Applications
            .FirstOrDefault(a => a.Path == "/")
            ?? throw new Exception("App not found with path '/'");
        var virtDirPath = $"/{appName}";
        var virtDir = baseApp.VirtualDirectories.FirstOrDefault(vd => vd.Path.Equals(virtDirPath, StringComparison.OrdinalIgnoreCase));
        var virtDirPhysPath = xtiFolder.InstallPath(appKey);
        if (virtDir == null)
        {
            baseApp.VirtualDirectories.Add(virtDirPath, virtDirPhysPath);
        }
        var appPhysPath = Path.Combine(virtDirPhysPath, versionKey.DisplayText);
        var appPath = $"/{appName}/{versionKey.DisplayText}";
        var iisApp = site.Applications.FirstOrDefault(a => a.Path.Equals(appPath, StringComparison.OrdinalIgnoreCase));
        if (iisApp == null)
        {
            iisApp = site.Applications.Add(appPath, appPhysPath);
            iisApp.ApplicationPoolName = appPoolName;
        }
        server.CommitChanges();
        if (!Directory.Exists(appPhysPath))
        {
            Directory.CreateDirectory(appPhysPath);
        }
        var appOfflineFile = new AppOfflineFile(xtiFolder, appKey, versionKey);
        await appOfflineFile.Write();
        await Task.Delay(10000);
    }

    public async Task Delete()
    {
        using var server = new ServerManager();
        if (string.IsNullOrWhiteSpace(siteName))
        {
            throw new ArgumentException("siteName is required");
        }
        var site = server.Sites[siteName];
        var appOfflineFile = new AppOfflineFile(xtiFolder, appKey, versionKey);
        await appOfflineFile.Write();
        await Task.Delay(10000);
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        var appPath = $"/{appName}/{versionKey.DisplayText}";
        var iisApp = site.Applications.FirstOrDefault(a => a.Path.Equals(appPath, StringComparison.OrdinalIgnoreCase));
        if (iisApp != null)
        {
            site.Applications.Remove(iisApp);
        }
        var appPoolName = $"Xti_{xtiEnv.EnvironmentName}_{appName}_{versionKey.DisplayText}";
        var appPool = server.ApplicationPools
            .FirstOrDefault(ap => ap.Name.Equals(appPoolName, StringComparison.OrdinalIgnoreCase));
        if (appPool != null)
        {
            server.ApplicationPools.Remove(appPool);
        }
        server.CommitChanges();
    }
}
