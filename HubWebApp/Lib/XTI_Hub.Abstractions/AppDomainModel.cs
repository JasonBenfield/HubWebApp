using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppDomainModel(AppKey AppKey, AppVersionKey VersionKey, string Domain);