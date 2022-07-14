namespace XTI_HubWebAppApi.ResourceInquiry;

public sealed class GetResourceRequest
{
    public string VersionKey { get; set; } = "";
    public int ResourceID { get; set; }
}
