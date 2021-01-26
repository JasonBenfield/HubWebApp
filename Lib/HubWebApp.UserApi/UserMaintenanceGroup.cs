using System.Collections.Generic;
using XTI_App.Api;

namespace HubWebApp.UserApi
{
    public sealed class UserMaintenanceGroup : AppApiGroupWrapper
    {
        public UserMaintenanceGroup(AppApiGroup source, UserMaintenanceGroupFactory factory)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            EditUser = source.AddAction(actions.Action(nameof(EditUser), factory.CreateEditUser));
            GetUserForEdit = source.AddAction(actions.Action(nameof(GetUserForEdit), factory.CreateGetUserForEdit));
        }

        public AppApiAction<EditUserForm, EmptyActionResult> EditUser { get; }
        public AppApiAction<int, IDictionary<string, object>> GetUserForEdit { get; }
    }
}
