using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record InstallConfigurationModel
(
    int ID,
    string ConfigurationName,
    AppKey AppKey,
    InstallConfigurationTemplateModel Template,
    int InstallSequence
)
{
    public InstallConfigurationModel()
        : this(0, "", new AppKey("", AppType.Values.GetDefault()), new(), 0)
    {
    }

    public bool IsFound() => ID > 0;

    public bool IsMatch(AppKey appKey, string configurationName) =>
        AppKey.Equals(appKey) &&
        IsConfigrationNameMatch(configurationName);

    private bool IsConfigrationNameMatch(string configurationName) =>
        string.IsNullOrWhiteSpace(configurationName) ||
        ConfigurationName.Equals(configurationName, StringComparison.OrdinalIgnoreCase);
}
