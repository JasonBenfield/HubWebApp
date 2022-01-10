using Microsoft.Extensions.Options;
using XTI_Git;
using XTI_GitHub;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class VersionGitFactory
{
    private readonly IXtiGitFactory gitFactory;
    private readonly IGitHubFactory gitHubFactory;
    private readonly VersionToolOptions options;
    private IXtiGitRepository? cachedGitRepo;
    private XtiGitHubRepository? cachedGitHubRepo;

    public VersionGitFactory(IXtiGitFactory gitFactory, IGitHubFactory gitHubFactory, IOptions<VersionToolOptions> options)
    {
        this.gitFactory = gitFactory;
        this.gitHubFactory = gitHubFactory;
        this.options = options.Value;
    }

    public IXtiGitRepository CreateGitRepo() =>
        cachedGitRepo ??= gitFactory.CreateRepository(Environment.CurrentDirectory);

    public XtiGitHubRepository CreateGitHubRepo()
    {
        if (string.IsNullOrWhiteSpace(options.RepoOwner)) { throw new ArgumentException("Repo Owner is required"); }
        if (string.IsNullOrWhiteSpace(options.RepoName)) { throw new ArgumentException("Repo Name is required"); }
        return cachedGitHubRepo ??= gitHubFactory.CreateGitHubRepository(options.RepoOwner, options.RepoName);
    }
}