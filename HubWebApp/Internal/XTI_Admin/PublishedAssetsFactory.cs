using XTI_GitHub;
using XTI_Internal.Implementations;

namespace XTI_Admin;

public sealed class PublishedAssetsFactory
{
    private readonly PublishedFolder publishedFolder;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public PublishedAssetsFactory(PublishedFolder publishedFolder, XtiGitHubRepository gitHubRepo, AppVersionNameAccessor versionNameAccessor)
    {
        this.publishedFolder = publishedFolder;
        this.gitHubRepo = gitHubRepo;
        this.versionNameAccessor = versionNameAccessor;
    }

    public IPublishedAssets Create(InstallationSources installationSource)
    {
        IPublishedAssets publishedAssets;
        if (installationSource == InstallationSources.Folder)
        {
            publishedAssets = new FolderPublishedAssets(publishedFolder);
        }
        else if (installationSource == InstallationSources.GitHub)
        {
            var versionName = versionNameAccessor.Value;
            publishedAssets = new GitHubPublishedAssets
            (
                gitHubRepo,
                $"xti_{versionName.Value}"
            );
        }
        else
        {
            throw new NotSupportedException($"Installation Source {installationSource} is not supported");
        }
        return publishedAssets;
    }
}
