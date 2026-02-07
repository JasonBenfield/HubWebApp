using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class AddAppInstallCommandRequest
{
    public static AddAppInstallCommandRequest Deserialize(string serializedRequest) =>
        XtiSerializer.Deserialize<AddAppInstallCommandRequest>(serializedRequest);

    public AddAppInstallCommandRequest()
        : this(XTI_App.Abstractions.AppKey.Unknown, AppVersionKey.None, 0, false, false)
    {
    }

    public AddAppInstallCommandRequest(AppKey appKey, AppVersionKey versionKey, int installConfigurationID, bool installAsCurrent, bool isAutoStartEnabled)
    {
        AppKey = new AppKeyRequest(appKey);
        VersionKey = versionKey.DisplayText;
        InstallConfigurationID = installConfigurationID;
        InstallAsCurrent = installAsCurrent;
        IsAutoStartEnabled = isAutoStartEnabled;
    }

    public AppKeyRequest AppKey { get; set; }
    public string VersionKey { get; set; }
    public int InstallConfigurationID { get; set; }
    public bool InstallAsCurrent { get; set; }
    public bool IsAutoStartEnabled { get; set; }

    public AppVersionKey ToAppVersionKey() => AppVersionKey.Parse(VersionKey);

    public string Serialize() => XtiSerializer.Serialize(this);
}
