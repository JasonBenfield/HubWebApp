using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppRegistration
{
    public sealed class AddSystemUserRequest
    {
        public AppKey AppKey { get; set; }
        public string MachineName { get; set; }
        public string Password { get; set; }
    }
    public sealed class AddSystemUserAction : AppAction<AddSystemUserRequest, AppUserModel>
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;
        private readonly IHashedPasswordFactory hashedPasswordFactory;

        public AddSystemUserAction(AppFactory appFactory, Clock clock, IHashedPasswordFactory hashedPasswordFactory)
        {
            this.appFactory = appFactory;
            this.clock = clock;
            this.hashedPasswordFactory = hashedPasswordFactory;
        }

        public async Task<AppUserModel> Execute(AddSystemUserRequest model)
        {
            var systemUser = await appFactory.Users().SystemUser(model.AppKey, model.MachineName);
            var hashedPassword = hashedPasswordFactory.Create(model.Password);
            if (systemUser.UserName().Equals(AppUserName.SystemUser(model.AppKey, model.MachineName)))
            {
                await systemUser.ChangePassword(hashedPassword);
            }
            else
            {
                systemUser = await appFactory.Users().AddSystemUser
                (
                    model.AppKey,
                    model.MachineName,
                    hashedPassword,
                    clock.Now()
                );
            }
            var app = await appFactory.Apps().App(model.AppKey);
            var modifier = await app.DefaultModifier();
            var assignedRoles = await systemUser.AssignedRoles(modifier);
            if (!assignedRoles.Any(r => r.Name().Equals(AppRoleName.System)))
            {
                var systemRole = await app.Role(AppRoleName.System);
                await systemUser.AddRole(systemRole);
            }
            return systemUser.ToModel();
        }
    }
}
