using XTI_Git;
using XTI_GitHub;

namespace XTI_Version;

public interface GitFactory
{
    Task<IXtiGitRepository> CreateGitRepo();
    Task<XtiGitHubRepository> CreateGitHubRepo(string ownerName, string repoName);
}