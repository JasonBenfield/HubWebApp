using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddSystemUserRequest
{
    public AddSystemUserRequest()
        : this(XTI_App.Abstractions.AppKey.Unknown, "", "")
    {
    }

    public AddSystemUserRequest(AppKey appKey, string machineName, string password)
    {
        AppKey = new AppKeyRequest(appKey);
        MachineName = machineName;
        Password = password;
    }

    public AppKeyRequest AppKey { get; set; }
    public string MachineName { get; set; }
    public string Password { get; set; }
}