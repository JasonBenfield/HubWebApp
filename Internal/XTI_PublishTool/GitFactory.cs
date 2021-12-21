using XTI_GitHub;

namespace XTI_PublishTool;

public interface GitFactory
{
    Task<XtiGitHubRepository> CreateGitHubRepo(string ownerName, string repoName);
}