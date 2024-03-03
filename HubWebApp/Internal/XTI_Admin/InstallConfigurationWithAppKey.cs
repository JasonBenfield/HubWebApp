using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed record InstallConfigurationWithAppKey(InstallConfigurationModel Config, AppKey AppKey);