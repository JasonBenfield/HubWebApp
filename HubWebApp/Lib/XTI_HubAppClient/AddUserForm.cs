// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AddUserForm : Form
{
    public AddUserForm(string name) : base(name)
    {
        UserName = AddField(new InputField<string>(FieldName, nameof(UserName)));
        Password = AddField(new InputField<string>(FieldName, nameof(Password)));
        Confirm = AddField(new InputField<string>(FieldName, nameof(Confirm)));
        PersonName = AddField(new InputField<string>(FieldName, nameof(PersonName)));
        Email = AddField(new InputField<string>(FieldName, nameof(Email)));
    }

    public InputField<string> UserName { get; }

    public InputField<string> Password { get; }

    public InputField<string> Confirm { get; }

    public InputField<string> PersonName { get; }

    public InputField<string> Email { get; }
}