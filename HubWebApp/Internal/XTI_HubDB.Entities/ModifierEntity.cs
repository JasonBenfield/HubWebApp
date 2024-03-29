﻿namespace XTI_HubDB.Entities;

public sealed class ModifierEntity
{
    public int ID { get; set; }
    public int CategoryID { get; set; }
    public string ModKey { get; set; } = "xti_notfound";
    public string ModKeyDisplayText { get; set; } = "";
    public string TargetKey { get; set; } = "xti_notfound";
    public string DisplayText { get; set; } = "";
}