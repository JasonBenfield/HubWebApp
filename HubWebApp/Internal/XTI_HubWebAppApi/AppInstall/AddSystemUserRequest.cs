﻿namespace XTI_HubWebAppApi.AppInstall;

public sealed class AddSystemUserRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public string MachineName { get; set; } = "";
    public string Password { get; set; } = "";
}