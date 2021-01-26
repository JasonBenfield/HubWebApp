// Generated code
using XTI_WebAppClient.Forms;
using System;

namespace HubWebApp.Client
{
    public sealed partial class EditUserForm : Form
    {
        public EditUserForm(string name): base(name)
        {
            UserID = AddField(new HiddenField<int>(FieldName, nameof(UserID)));
            PersonName = AddField(new InputField<string>(FieldName, nameof(PersonName)));
            Email = AddField(new InputField<string>(FieldName, nameof(Email)));
        }

        public HiddenField<int> UserID
        {
            get;
        }

        public InputField<string> PersonName
        {
            get;
        }

        public InputField<string> Email
        {
            get;
        }
    }
}