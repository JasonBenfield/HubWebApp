using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class AddInstallCommandRequest
{
    public static AddInstallCommandRequest Deserialize(string serializedRequest) =>
        XtiSerializer.Deserialize<AddInstallCommandRequest>(serializedRequest);

    public AddInstallCommandRequest()
        : this(AppVersionKey.None, 0, false, false)
    {
    }

    public AddInstallCommandRequest(AppVersionKey versionKey, int installConfigurationID, bool installAsCurrent, bool isAutoStartEnabled)
    {
        VersionKey = versionKey.DisplayText;
        InstallConfigurationID = installConfigurationID;
        InstallAsCurrent = installAsCurrent;
        IsAutoStartEnabled = isAutoStartEnabled;
    }

    public string VersionKey { get; set; }
    public int InstallConfigurationID { get; set; }
    public bool InstallAsCurrent { get; set; }
    public bool IsAutoStartEnabled { get; set; }

    public AppVersionKey ToAppVersionKey() => AppVersionKey.Parse(VersionKey);

    public string Serialize() => XtiSerializer.Serialize(this);
}
