
namespace XTI_HubDB.Entities;

public sealed class InstallConfigurationEntity
{
    public int ID { get; set; }
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public string ConfigurationName { get; set; } = "";
    public string AppName { get; set; } = "";
    public int AppType { get; set; }
    public int TemplateID { get; set; }
    public int InstallSequence { get; set; }

}
