namespace XTI_HubDB.Entities
{
    public sealed class AppUserRoleEntity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ModifierID { get; set; }
        public int RoleID { get; set; }
    }
}
