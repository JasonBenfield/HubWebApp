using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_HubAppApi.UserMaintenance;

namespace XTI_HubAppApi
{
    internal static class UserMaintenanceExtensions
    {
        public static void AddUserMaintenanceGroupServices(this IServiceCollection services)
        {
            services.AddScoped<EditUserAction>();
            services.AddScoped<GetUserForEditAction>();
        }
    }
}
