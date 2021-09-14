﻿using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.UserList
{
    public sealed class GetSystemUsersAction : AppAction<AppKey, AppUserModel[]>
    {
        private readonly AppFactory appFactory;

        public GetSystemUsersAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppUserModel[]> Execute(AppKey appKey)
        {
            var users = await appFactory.Users().SystemUsers(appKey);
            var userModels = users.Select(u => u.ToModel()).ToArray();
            return userModels;
        }
    }
}