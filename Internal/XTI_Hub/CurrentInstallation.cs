using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_HubDB.Entities;

namespace XTI_Hub
{
    public sealed class CurrentInstallation : Installation
    {
        internal CurrentInstallation(AppFactory appFactory, InstallationEntity entity) 
            : base(appFactory, entity)
        {
        }

        public Task Start(AppVersion appVersion) => StartCurrent(appVersion);
    }
}
