namespace XTI_Hub;

public sealed class ResourceGroupRoleModel
{
    public int ID { get; set; }
    public int RoleID { get; set; }
    public string RoleName { get; set; } = "";
    public bool IsAllowed { get; set; }
}