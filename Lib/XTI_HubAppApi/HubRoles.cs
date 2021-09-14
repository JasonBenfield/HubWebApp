﻿using XTI_App.Abstractions;

namespace XTI_HubAppApi
{
    public sealed class HubRoles
    {
        public static HubRoles Instance = new HubRoles();

        public AppRoleName Admin { get; } = AppRoleName.Admin;
        public AppRoleName RegisterApp { get; } = new AppRoleName(nameof(RegisterApp));
        public AppRoleName ViewApp { get; } = new AppRoleName(nameof(ViewApp));
        public AppRoleName AddUser { get; } = new AppRoleName(nameof(AddUser));
        public AppRoleName EditUser { get; } = new AppRoleName(nameof(EditUser));
        public AppRoleName ViewUser { get; } = new AppRoleName(nameof(ViewUser));
    }
}