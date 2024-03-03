using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class ConfigureInstallRequest
{
    public ConfigureInstallRequest()
        : this("", "", "", XTI_App.Abstractions.AppKey.Unknown, "", 0)
    {
    }

    public ConfigureInstallRequest(string repoOwner, string repoName, string configurationName, AppKey appKey, string templateName, int installSequence)
    {
        RepoOwner = repoOwner;
        RepoName = repoName;
        ConfigurationName = configurationName;
        AppKey = new AppKeyRequest(appKey);
        TemplateName = templateName;
        InstallSequence = installSequence;
    }

    public string RepoOwner { get; set; }
    public string RepoName { get; set; }
    public string ConfigurationName { get; set; }
    public AppKeyRequest AppKey { get; set; }
    public string TemplateName { get; set; }
    public int InstallSequence { get; set; }
}
