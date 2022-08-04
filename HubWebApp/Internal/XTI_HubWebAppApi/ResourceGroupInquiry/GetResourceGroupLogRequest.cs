namespace XTI_HubWebAppApi.ResourceGroupInquiry;

public sealed class GetResourceGroupLogRequest
{
    public string VersionKey { get; set; } = "";
    public int GroupID { get; set; }
    public int HowMany { get; set; }
}