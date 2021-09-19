using System.Threading.Tasks;
using XTI_Git;
using XTI_GitHub;

namespace XTI_PublishTool
{
    public interface GitFactory
    {
        Task<XtiGitRepository> CreateGitRepo();
        Task<XtiGitHubRepository> CreateGitHubRepo(string ownerName, string repoName);
    }
}
