namespace XTI_HubDB.Entities
{
    public sealed class ResourceRoleEntity
    {
        public int ID { get; set; }
        public int ResourceID { get; set; }
        public int RoleID { get; set; }
        public bool IsAllowed { get; set; }
    }
}
