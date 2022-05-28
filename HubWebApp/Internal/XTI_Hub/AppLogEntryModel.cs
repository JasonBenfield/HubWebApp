﻿using XTI_Core;

namespace XTI_Hub;

public sealed class AppLogEntryModel
{
    public int ID { get; set; }
    public int RequestID { get; set; }
    public DateTimeOffset TimeOccurred { get; set; }
    public AppEventSeverity Severity { get; set; } = AppEventSeverity.Values.NotSet;
    public string Caption { get; set; } = "";
    public string Message { get; set; } = "";
    public string Detail { get; set; } = "";
}