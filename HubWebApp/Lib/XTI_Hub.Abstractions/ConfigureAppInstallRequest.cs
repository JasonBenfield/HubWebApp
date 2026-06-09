namespace XTI_Hub.Abstractions;

public sealed class ConfigureAppInstallRequest
{
    public ConfigureAppInstallRequest()
        : this("", 0, 0)
    {
    }

    public ConfigureAppInstallRequest(string configurationName, int templateID, int installSequence)
    {
        ConfigurationName = configurationName;
        TemplateID = templateID;
        InstallSequence = installSequence;
    }

    public string ConfigurationName { get; set; }
    public int TemplateID { get; set; }
    public int InstallSequence { get; set; }
}
