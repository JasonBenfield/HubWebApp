using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetUserContextRequest
{
    public GetUserContextRequest()
        :this(0, AppUserName.Anon)
    {
    }

    public GetUserContextRequest(int installationID, AppUserName userName)
    {
        InstallationID = installationID;
        UserName = userName.DisplayText;
    }

    public int InstallationID { get; set; }
    public string UserName { get; set; }
}
