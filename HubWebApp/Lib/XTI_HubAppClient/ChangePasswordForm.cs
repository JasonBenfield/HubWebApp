// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ChangePasswordForm : Form
{
    public ChangePasswordForm(string name) : base(name)
    {
        UserID = AddField(new HiddenField<int>(FieldName, nameof(UserID)));
        Password = AddField(new InputField<string>(FieldName, nameof(Password)));
        Confirm = AddField(new InputField<string>(FieldName, nameof(Confirm)));
    }

    public HiddenField<int> UserID { get; }

    public InputField<string> Password { get; }

    public InputField<string> Confirm { get; }
}