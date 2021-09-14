namespace XTI_HubDB.Entities
{
    public sealed class ResourceGroupRecord
    {
        public int ID { get; set; }
        public int VersionID { get; set; }
        public string Name { get; set; } = "";
        public int ModCategoryID { get; set; }
        public bool IsAnonymousAllowed { get; set; }
    }
}
