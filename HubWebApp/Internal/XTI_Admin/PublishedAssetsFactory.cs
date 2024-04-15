using XTI_Core;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class PublishedAssetsFactory
{
    private readonly PublishedFolder publishedFolder;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly CurrentVersion currentVersionAccessor;

    public PublishedAssetsFactory(PublishedFolder publishedFolder, XtiGitHubRepository gitHubRepo, AppVersionNameAccessor versionNameAccessor, CurrentVersion currentVersionAccessor)
    {
        this.publishedFolder = publishedFolder;
        this.gitHubRepo = gitHubRepo;
        this.versionNameAccessor = versionNameAccessor;
        this.currentVersionAccessor = currentVersionAccessor;
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
            publishedAssets = new GitHubPublishedAssets
            (
                gitHubRepo,
                versionNameAccessor,
                currentVersionAccessor
            );
        }
        else
        {
            throw new NotSupportedException($"Installation Source {installationSource} is not supported");
        }
        return publishedAssets;
    }
}
