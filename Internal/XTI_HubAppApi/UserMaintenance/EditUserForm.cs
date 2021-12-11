using XTI_Forms;

namespace XTI_HubAppApi.UserMaintenance
{
    public sealed class EditUserForm : Form
    {
        public EditUserForm() : base(nameof(EditUserForm))
        {
            UserID = AddInt32Hidden(nameof(UserID));
            UserID.MustNotBeNull();
            PersonName = AddTextInput(nameof(PersonName));
            Email = AddTextInput(nameof(Email));
        }

        public HiddenField<int?> UserID { get; }
        public InputField<string> PersonName { get; }
        public InputField<string> Email { get; }
    }
}
