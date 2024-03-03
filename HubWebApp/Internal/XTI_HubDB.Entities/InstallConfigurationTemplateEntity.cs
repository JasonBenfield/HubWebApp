namespace XTI_HubDB.Entities;

public sealed class InstallConfigurationTemplateEntity
{
    public int ID { get; set; }
    public string TemplateName { get; set; } = "";
    public string DestinationMachineName { get; set; } = "";
    public string Domain { get; set; } = "";
    public string SiteName { get; set; } = "";
}
