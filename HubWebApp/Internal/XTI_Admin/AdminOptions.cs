using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Admin;

public sealed class AdminOptions
{
    public CommandNames Command { get; set; }
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string HubAppVersionKey { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string VersionNumber { get; set; } = "";
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public string InstallConfigurationName { get; set; } = "";
    public string InstallTemplateName { get; set; } = "";
    public int InstallSequence { get; set; } 
    public string Domain { get; set; } = "";
    public string SiteName { get; set; } = "";
    public string DestinationMachine { get; set; } = "";
    public string RemoteOptionsKey { get; set; } = "";
    public InstallationSources InstallationSource { get; set; } = InstallationSources.Default;
    public string VersionType { get; set; } = "";
    public string IssueTitle { get; set; } = "";
    public int IssueNumber { get; set; }
    public bool StartIssue { get; set; }
    public HubAdministrationTypes HubAdministrationType { get; set; } = HubAdministrationTypes.Default;
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string CredentialKey { get; set; } = "";

    public AppKey AppKey() =>
        string.IsNullOrWhiteSpace(AppName)
        ? XTI_App.Abstractions.AppKey.Unknown
        : new AppKey(new AppName(AppName), XTI_App.Abstractions.AppType.Values.Value(AppType));

    public InstallationSources GetInstallationSource(XtiEnvironment xtiEnv)
    {
        var installationSource = InstallationSource;
        if (installationSource == InstallationSources.Default)
        {
            if (xtiEnv.IsDevelopmentOrTest())
            {
                installationSource = InstallationSources.Folder;
            }
            else
            {
                installationSource = InstallationSources.GitHub;
            }
        }
        return installationSource;
    }

    public AdminOptions Copy() =>
        new AdminOptions
        {
            Command = Command,
            AppName = AppName,
            AppType = AppType,
            HubAdministrationType = HubAdministrationType,
            HubAppVersionKey = HubAppVersionKey,
            CredentialKey = CredentialKey,
            DestinationMachine = DestinationMachine,
            Domain = Domain,
            InstallationSource = InstallationSource,
            IssueNumber = IssueNumber,
            IssueTitle = IssueTitle,
            UserName = UserName,
            Password = Password,
            RemoteOptionsKey = RemoteOptionsKey,
            RepoName = RepoName,
            RepoOwner = RepoOwner,
            InstallConfigurationName = InstallConfigurationName,
            InstallTemplateName = InstallTemplateName,
            InstallSequence = InstallSequence,
            SiteName = SiteName,
            StartIssue = StartIssue,
            VersionKey = VersionKey,
            VersionNumber = VersionNumber,
            VersionType = VersionType
        };

    public void Load(AdminOptions options)
    {
        Command = options.Command;
        AppName = options.AppName;
        AppType = options.AppType;
        HubAdministrationType = options.HubAdministrationType;
        HubAppVersionKey = options.HubAppVersionKey;
        CredentialKey = options.CredentialKey;
        DestinationMachine = options.DestinationMachine;
        Domain = options.Domain;
        InstallationSource = options.InstallationSource;
        IssueNumber = options.IssueNumber;
        IssueTitle = options.IssueTitle;
        UserName = options.UserName;
        Password = options.Password;
        RemoteOptionsKey = options.RemoteOptionsKey;
        RepoOwner = options.RepoOwner;
        RepoName = options.RepoName;
        InstallConfigurationName = options.InstallConfigurationName;
        InstallTemplateName = options.InstallTemplateName;
        InstallSequence = options.InstallSequence;
        SiteName = options.SiteName;
        StartIssue = options.StartIssue;
        VersionKey = options.VersionKey;
        VersionNumber = options.VersionNumber;
        VersionType = options.VersionType;
    }
}