using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AppUserNameRequest
{
    public AppUserNameRequest()
        : this(new(""))
    {
    }

    public AppUserNameRequest(AppUserName userName)
        : this(userName.DisplayText)
    {
    }

    public AppUserNameRequest(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }

    public AppUserName ToAppUserName() => new(UserName);
}
