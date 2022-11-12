﻿namespace XTI_HubWebAppApi.UserGroups;

public sealed class ExpandedUser
{
    public int UserID { get; set; }
    public string UserName { get; set; } = "";
    public string PersonName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTimeOffset TimeUserAdded { get; set; }
    public int UserGroupID { get; set; }
    public string UserGroupName { get; set; } = "";
}
