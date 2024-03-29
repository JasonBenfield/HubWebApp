﻿namespace XTI_HubDB.Entities;

public sealed class AppRoleEntity
{
    public int ID { get; set; }
    public int AppID { get; set; }
    public string Name { get; set; } = "xti_notfound";
    public string DisplayText { get; set; } = "";
    public DateTimeOffset TimeDeactivated { get; set; } = DateTimeOffset.MaxValue;
}