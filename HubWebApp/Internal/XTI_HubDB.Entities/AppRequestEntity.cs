﻿namespace XTI_HubDB.Entities;

public sealed class AppRequestEntity
{
    public int ID { get; set; }
    public string RequestKey { get; set; } = "";
    public int SessionID { get; set; }
    public string Path { get; set; } = "";
    public int InstallationID { get; set; }
    public int ResourceID { get; set; }
    public int ModifierID { get; set; }
    public DateTimeOffset TimeStarted { get; set; } = DateTimeOffset.MinValue;
    public DateTimeOffset TimeEnded { get; set; } = DateTimeOffset.MaxValue;
    public int ActualCount { get; set; } = 1;
    public string RequestData { get; set; } = "";
    public string ResultData { get; set; } = "";
}