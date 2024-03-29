﻿namespace XTI_HubWebAppApi.AppInstall;

public sealed class RegisterAppRequest
{
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;

    public AppApiTemplateModel AppTemplate { get; set; } = new AppApiTemplateModel();
}