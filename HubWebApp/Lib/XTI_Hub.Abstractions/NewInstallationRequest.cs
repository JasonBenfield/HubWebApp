using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class NewInstallationRequest
{
    public NewInstallationRequest()
        : this(AppVersionName.Unknown, XTI_App.Abstractions.AppKey.Unknown, "", "", "")
    {
    }

    public NewInstallationRequest(AppVersionName versionName, AppKey appKey, string qualifiedMachineName, string domain, string siteName)
    {
        VersionName = versionName.DisplayText;
        AppKey = new AppKeyRequest(appKey);
        QualifiedMachineName = qualifiedMachineName;
        Domain = domain;
        SiteName = siteName;
    }

    public string VersionName { get; set; }
    public AppKeyRequest AppKey { get; set; }
    public string QualifiedMachineName { get; set; }
    public string Domain { get; set; }
    public string SiteName { get; set; }

    public AppVersionName ToAppVersionName() => new(VersionName);
}