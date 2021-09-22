namespace XTI_HubDB.Entities
{
    public sealed class ResourceGroupRoleRecord
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public int RoleID { get; set; }
        public bool IsAllowed { get; set; }
    }
}
