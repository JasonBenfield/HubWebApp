namespace XTI_Hub.Abstractions;

public sealed class AddInstallationUserRequest
{
    public AddInstallationUserRequest()
        : this("", "")
    {
    }

    public AddInstallationUserRequest(string machineName, string password)
    {
        MachineName = machineName;
        Password = password;
    }

    public string MachineName { get; set; }
    public string Password { get; set; }
}