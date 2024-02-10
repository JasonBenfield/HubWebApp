using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddAdminUserRequest
{

    public AddAdminUserRequest()
        : this(XTI_App.Abstractions.AppKey.Unknown, new AppUserName(""), "")
    {
    }

    public AddAdminUserRequest(AppKey appKey, AppUserName userName, string password)
    {
        AppKey = new AppKeyRequest(appKey);
        UserName = userName.DisplayText;
        Password = password;
    }

    public AppKeyRequest AppKey { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public AppUserName ToAppUserName() => new(UserName);
}
