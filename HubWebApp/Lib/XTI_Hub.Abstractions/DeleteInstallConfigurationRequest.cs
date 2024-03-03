using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class DeleteInstallConfigurationRequest
{
    public DeleteInstallConfigurationRequest()
        :this("", "", "", XTI_App.Abstractions.AppKey.Unknown)
    {    
    }

    public DeleteInstallConfigurationRequest(string repoOwner, string repoName, string configurationName, AppKey appKey)
    {
        RepoOwner = repoOwner;
        RepoName = repoName;
        ConfigurationName = configurationName;
        AppKey = new AppKeyRequest(appKey);
    }

    public string RepoOwner { get; set; }
    public string RepoName { get; set; }
    public string ConfigurationName { get; set; }
    public AppKeyRequest AppKey { get; set; }
}
