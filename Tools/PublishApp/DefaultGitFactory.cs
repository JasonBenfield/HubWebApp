using XTI_GitHub;
using XTI_GitHub.Web;
using XTI_PublishTool;
using XTI_Secrets;

namespace PublishApp;

internal sealed class DefaultGitFactory : GitFactory
{
    private readonly ISecretCredentialsFactory credentialsFactory;

    public DefaultGitFactory(ISecretCredentialsFactory credentialsFactory)
    {
        this.credentialsFactory = credentialsFactory;
    }

    public async Task<XtiGitHubRepository> CreateGitHubRepo(string ownerName, string repoName)
    {
        var gitHubRepo = new WebXtiGitHubRepository(ownerName, repoName);
        var credentials = credentialsFactory.Create("GitHub");
        var credentialsValue = await credentials.Value();
        gitHubRepo.UseCredentials(credentialsValue.UserName, credentialsValue.Password);
        return gitHubRepo;
    }
}