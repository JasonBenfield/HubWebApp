// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExpandedUser
{
    public int UserID { get; set; }
    public string UserName { get; set; } = "";
    public string PersonName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTimeOffset TimeUserAdded { get; set; }
    public int UserGroupID { get; set; }
    public string UserGroupName { get; set; } = "";
    public DateTimeOffset TimeUserDeactivated { get; set; }
    public bool IsActive { get; set; }
}