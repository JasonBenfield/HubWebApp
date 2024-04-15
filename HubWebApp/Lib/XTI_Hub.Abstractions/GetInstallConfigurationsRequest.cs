namespace XTI_Hub.Abstractions;

public sealed class GetInstallConfigurationsRequest
{
    public GetInstallConfigurationsRequest()
        :this("", "", "")
    {    
    }

    public GetInstallConfigurationsRequest(string repoOwner, string repoName, string configurationName)
    {
        RepoOwner = repoOwner;
        RepoName = repoName;
        ConfigurationName = configurationName;
    }

    public string RepoOwner { get; set; }
    public string RepoName { get; set; }
    public string ConfigurationName { get; set; }
}
