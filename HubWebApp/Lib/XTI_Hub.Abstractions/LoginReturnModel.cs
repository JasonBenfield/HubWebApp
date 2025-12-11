namespace XTI_Hub.Abstractions;

public sealed class LoginReturnModel
{
    public LoginReturnModel()
        : this("", "")
    {
    }

    public LoginReturnModel(string requesterKey, string returnUrl)
    {
        RequesterKey = requesterKey;
        ReturnUrl = returnUrl;
    }

    public string RequesterKey { get; set; }
    public string ReturnUrl { get; set; }
}
