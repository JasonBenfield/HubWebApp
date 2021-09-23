using System.Threading.Tasks;
using XTI_GitHub;
using XTI_GitHub.Web;
using XTI_Secrets;

namespace LocalInstallApp
{
    sealed class GitFactory
    {
        private readonly ISecretCredentialsFactory credentialsFactory;

        public GitFactory(ISecretCredentialsFactory credentialsFactory)
        {
            this.credentialsFactory = credentialsFactory;
        }

        public async Task<XtiGitHubRepository> CreateGitHubRepo(string ownerName, string repoName)
        {
            var gitHubRepo = new WebXtiGitHubRepository(ownerName, repoName);
            var credentials = credentialsFactory.Create("GitHub");
            if (credentials.Exist())
            {
                var credentialsValue = await credentials.Value();
                gitHubRepo.UseCredentials(credentialsValue.UserName, credentialsValue.Password);
            }
            return gitHubRepo;
        }
    }
}
