namespace XTI_HubWebAppApi.AppUserMaintenance;

public sealed class UserRoleRequest
{
    public int UserID { get; set; }
    public int ModifierID { get; set; }
    public int RoleID { get; set; }
}