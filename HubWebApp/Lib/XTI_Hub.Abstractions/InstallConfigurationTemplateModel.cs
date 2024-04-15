namespace XTI_Hub.Abstractions;

public sealed record InstallConfigurationTemplateModel
(
    int ID,
    string TemplateName,
    string DestinationMachineName,
    string Domain,
    string SiteName
)
{
    public InstallConfigurationTemplateModel()
        : this(0, "", "", "", "")
    {
    }

    public bool IsFound() => ID > 0;
}
