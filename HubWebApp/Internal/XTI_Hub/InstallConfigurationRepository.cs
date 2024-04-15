using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallConfigurationRepository
{
    private readonly HubFactory hubFactory;

    internal InstallConfigurationRepository(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<InstallConfiguration[]> Configurations(string repoOwner, string repoName, string configurationName)
    {
        var configs = await hubFactory.DB.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && (configurationName == "" || c.ConfigurationName == configurationName))
            .ToArrayAsync();
        return configs.Select(c => new InstallConfiguration(hubFactory, c)).ToArray();
    }

    public async Task<InstallConfiguration> AddOrUpdateConfiguration
    (
        string repoOwner,
        string repoName,
        string configurationName,
        AppKey appKey,
        InstallConfigurationTemplate template,
        int installSequence
    )
    {
        if (installSequence == 0)
        {
            installSequence = await hubFactory.DB.InstallConfigurations.Retrieve()
                .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.ConfigurationName == configurationName)
                .OrderByDescending(c => c.InstallSequence)
                .Select(c => c.InstallSequence)
                .FirstOrDefaultAsync();
            installSequence++;
        }
        InstallConfiguration installConfiguration;
        var config = await hubFactory.DB.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.ConfigurationName == configurationName && c.AppName == appKey.Name && c.AppType == appKey.Type.Value)
            .FirstOrDefaultAsync();
        if (config == null)
        {
            config = new InstallConfigurationEntity
            {
                RepoOwner = repoOwner,
                RepoName = repoName,
                ConfigurationName = configurationName,
                AppName = appKey.Name.DisplayText,
                AppType = appKey.Type.Value,
                TemplateID = template.ID,
                InstallSequence = installSequence
            };
            await hubFactory.DB.InstallConfigurations.Create(config);
            installConfiguration = new InstallConfiguration(hubFactory, config);
        }
        else
        {
            installConfiguration = new InstallConfiguration(hubFactory, config);
            await installConfiguration.Update(template, installSequence);
        }
        return installConfiguration;
    }

    internal async Task<InstallConfiguration> ConfigurationOrDefault(string repoOwner, string repoName, string configurationName, AppKey appKey)
    {
        var config = await hubFactory.DB.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.ConfigurationName == configurationName && c.AppName == appKey.Name && c.AppType == appKey.Type.Value)
            .FirstOrDefaultAsync();
        return new InstallConfiguration(hubFactory, config ?? new());
    }
}
