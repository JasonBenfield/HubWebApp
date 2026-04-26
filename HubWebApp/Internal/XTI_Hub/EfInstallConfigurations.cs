using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallConfigurations
{
    private readonly EfHubDB db;

    internal EfInstallConfigurations(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<EfInstallConfiguration> Configuration(int configurationID, CancellationToken ct)
    {
        var config = await db.Context.InstallConfigurations.Retrieve()
            .Where(c => c.ID == configurationID)
            .FirstOrDefaultAsync(ct);
        return new EfInstallConfiguration(db, config ?? throw new Exception($"Install Configuration {configurationID} not found."));
    }

    public async Task<EfInstallConfiguration[]> Configurations(string repoOwner, string repoName, string configurationName, CancellationToken ct)
    {
        var configs = await db.Context.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && (configurationName == "" || c.ConfigurationName == configurationName))
            .OrderBy(c => c.InstallSequence)
            .ToArrayAsync(ct);
        return configs.Select(c => new EfInstallConfiguration(db, c)).ToArray();
    }

    internal async Task<EfInstallConfiguration[]> Configurations(string repoOwner, string repoName, AppKey appKey, CancellationToken ct)
    {
        var appName = appKey.Name.DisplayText;
        var appType = appKey.Type.Value;
        var configs = await db.Context.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.AppName == appName && c.AppType == appType)
            .OrderBy(c => c.ConfigurationName)
            .ThenBy(c => c.InstallSequence)
            .ToArrayAsync(ct);
        return configs.Select(c => new EfInstallConfiguration(db, c)).ToArray();
    }

    public async Task<EfInstallConfiguration> AddOrUpdateConfiguration
    (
        string repoOwner,
        string repoName,
        string configurationName,
        AppKey appKey,
        EfInstallConfigurationTemplate template,
        int installSequence,
        CancellationToken ct
    )
    {
        if (installSequence == 0)
        {
            installSequence = await db.Context.InstallConfigurations.Retrieve()
                .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.ConfigurationName == configurationName)
                .OrderByDescending(c => c.InstallSequence)
                .Select(c => c.InstallSequence)
                .FirstOrDefaultAsync(ct);
            installSequence++;
        }
        EfInstallConfiguration installConfiguration;
        var config = await db.Context.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.ConfigurationName == configurationName && (c.AppName == appKey.Name.DisplayText || c.AppName == appKey.Name.Value) && c.AppType == appKey.Type.Value)
            .FirstOrDefaultAsync(ct);
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
            await db.Context.InstallConfigurations.Create(config, ct);
            installConfiguration = new EfInstallConfiguration(db, config);
        }
        else
        {
            installConfiguration = new EfInstallConfiguration(db, config);
            await installConfiguration.Update(template, installSequence, ct);
        }
        return installConfiguration;
    }

    internal async Task<EfInstallConfiguration> ConfigurationOrDefault(string repoOwner, string repoName, string configurationName, AppKey appKey, CancellationToken ct)
    {
        var config = await db.Context.InstallConfigurations.Retrieve()
            .Where(c => c.RepoOwner == repoOwner && c.RepoName == repoName && c.ConfigurationName == configurationName && (c.AppName == appKey.Name.Value || c.AppName == appKey.Name.DisplayText) && c.AppType == appKey.Type.Value)
            .FirstOrDefaultAsync(ct);
        return new EfInstallConfiguration(db, config ?? new());
    }
}
