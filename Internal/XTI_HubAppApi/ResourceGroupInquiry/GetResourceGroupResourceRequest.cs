﻿namespace XTI_HubAppApi.ResourceGroupInquiry;

public sealed class GetResourceGroupResourceRequest
{
    public string VersionKey { get; set; } = "";
    public int GroupID { get; set; }
    public string ResourceName { get; set; } = "";
}
