﻿namespace XTI_HubAppApi.ResourceGroupInquiry;

public sealed class GetResourcesRequest
{
    public string VersionKey { get; set; } = "";
    public int GroupID { get; set; }
}
