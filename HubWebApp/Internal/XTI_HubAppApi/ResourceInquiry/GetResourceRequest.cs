namespace XTI_HubAppApi.ResourceInquiry;

public sealed class GetResourceRequest
{
    public string VersionKey { get; set; } = "";
    public int ResourceID { get; set; }
}
