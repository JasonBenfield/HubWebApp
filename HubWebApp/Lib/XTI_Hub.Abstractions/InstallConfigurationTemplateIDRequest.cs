namespace XTI_Hub.Abstractions;

public class InstallConfigurationTemplateIDRequest
{
    public InstallConfigurationTemplateIDRequest()
        : this(0)
    {
    }

    public InstallConfigurationTemplateIDRequest(int templateID)
    {
        TemplateID = templateID;
    }

    public int TemplateID { get; set; }
}
