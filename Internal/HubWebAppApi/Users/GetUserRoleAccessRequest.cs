namespace HubWebAppApi.Users
{
    public sealed class GetUserRoleAccessRequest
    {
        public int UserID { get; set; }
        public int ModifierID { get; set; }
    }
}
