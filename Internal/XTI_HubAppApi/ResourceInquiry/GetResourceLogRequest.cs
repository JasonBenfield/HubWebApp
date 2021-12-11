namespace XTI_HubAppApi.ResourceInquiry
{
    public sealed class GetResourceLogRequest
    {
        public string VersionKey { get; set; }
        public int ResourceID { get; set; }
        public int HowMany { get; set; }
    }
}
