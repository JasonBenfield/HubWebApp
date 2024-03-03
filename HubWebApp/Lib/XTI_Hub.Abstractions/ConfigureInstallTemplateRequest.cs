namespace XTI_Hub.Abstractions;

public sealed class ConfigureInstallTemplateRequest
{
    public ConfigureInstallTemplateRequest()
        :this("", "", "", "")
    {    
    }

    public ConfigureInstallTemplateRequest(string templateName, string destinationMachineName, string domain, string siteName)
    {
        TemplateName = templateName;
        DestinationMachineName = destinationMachineName;
        Domain = domain;
        SiteName = siteName;
    }

    public string TemplateName { get; set; }
    public string DestinationMachineName { get; set; }
    public string Domain { get; set; }
    public string SiteName { get; set; }
}
