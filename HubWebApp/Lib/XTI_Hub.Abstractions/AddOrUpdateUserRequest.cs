using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateUserRequest
{
    public AddOrUpdateUserRequest()
        :this(new AppUserName(), "", new PersonName(""), "")
    {    
    }

    public AddOrUpdateUserRequest(AppUserName userName, string password)
        :this(userName, password, new PersonName(""), "")
    {
    }

    public AddOrUpdateUserRequest(AppUserName userName, string password, PersonName personName, string email = "")
    {
        UserName = userName.DisplayText;
        Password = password;
        PersonName = personName.DisplayText;
        Email = email;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string PersonName { get; set; }
    public string Email { get; set; }
}