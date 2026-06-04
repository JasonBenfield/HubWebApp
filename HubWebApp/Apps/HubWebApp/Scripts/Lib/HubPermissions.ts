import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/Common";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { XtiUrl } from "@jasonbenfield/sharedwebapp/Http/XtiUrl";
import { HubAppClient } from "./Http/HubAppClient";

export interface IHubPermissions {
    readonly canAddUserGroup: boolean,
    readonly canConfigureInstallTemplate: boolean,
    readonly canViewUserRoles: boolean,
    readonly canViewLogs: boolean,
    readonly canManageInstallations: boolean
}

export interface IUserPermissions {
    readonly canEditUser: boolean,
    readonly canAddUser: boolean
}

export interface IModifiedUserPermissions {
    [name: string]: IUserPermissions;
}

interface IDeserializedPermissions {
    permissions: IHubPermissions;
    timeCacheExpires: string;
}

interface IDeserializedUserPermissions {
    permissions: IUserPermissions;
    timeCacheExpires: string;
}

export class HubPermissions {
    private static permissions: IHubPermissions | null = null;
    private static modifiedUserPermissions: IModifiedUserPermissions = {};
    private static isInProgress = false;
    private static isUserInProgress = false;
    private static readonly cacheKey = `hub_permissions_${pageContext.UserName}`;

    constructor(private readonly hubClient: HubAppClient) {
    }

    resetCache() {
        const localStorageKeys = Object.keys(localStorage).filter(k => k.startsWith(HubPermissions.cacheKey));
        for (const key of localStorageKeys) {
            localStorage.removeItem(key);
        }
        HubPermissions.permissions = null;
        for (const key of Object.keys(HubPermissions.modifiedUserPermissions)) {
            delete HubPermissions.modifiedUserPermissions[key];
        }
    }

    async value() {
        const maxWaitTime = DateTimeOffset.now().addMinutes(1);
        while (HubPermissions.isInProgress && !HubPermissions.permissions && DateTimeOffset.now() < maxWaitTime) {
            await DelayedAction.delay(100);
        }
        if (!HubPermissions.permissions) {
            const serialized = localStorage.getItem(HubPermissions.cacheKey);
            if (serialized) {
                try {
                    const deserialized: IDeserializedPermissions = JSON.parse(serialized);
                    const timeCacheExpires = DateTimeOffset.parse(deserialized.timeCacheExpires) || DateTimeOffset.max();
                    if (timeCacheExpires > DateTimeOffset.now()) {
                        HubPermissions.permissions = deserialized.permissions;
                    }
                }
                catch {
                    localStorage.setItem(HubPermissions.cacheKey, "");
                }
            }
        }
        if (!HubPermissions.permissions) {
            HubPermissions.isInProgress = true;
            try {
                HubPermissions.permissions = await this.getPermissions();
            }
            catch { }
            HubPermissions.isInProgress = false;
            const timeCacheExpires = DateTimeOffset.now().addDays(1);
            const serialized = JSON.stringify({
                timeCacheExpires: timeCacheExpires.toJSON(),
                permissions: HubPermissions.permissions
            });
            localStorage.setItem(HubPermissions.cacheKey, serialized);
        }
        return HubPermissions.permissions;
    }

    private getPermissions() {
        return this.hubClient.getUserAccess({
            canAddUserGroup: this.hubClient.getAccessRequest(c => c.UserGroups.AddUserGroupIfNotExistsAction, ""),
            canConfigureInstallTemplate: this.hubClient.getAccessRequest(c => c.Install.ConfigureInstallTemplateAction, ""),
            canViewUserRoles: this.hubClient.getAccessRequest(c => c.UserRoles.Index, ""),
            canViewLogs: this.hubClient.getAccessRequest(c => c.Logs.Sessions, ""),
            canManageInstallations: this.hubClient.getAccessRequest(c => c.Installations.Index, "")
        });
    }

    async userPermissions(modKey?: string) {
        const maxWaitTime = DateTimeOffset.now().addMinutes(1);
        if (modKey === undefined) {
            modKey = XtiUrl.current().path.modifier;
        }
        while (HubPermissions.isUserInProgress && !HubPermissions.modifiedUserPermissions[modKey] && DateTimeOffset.now() < maxWaitTime) {
            await DelayedAction.delay(100);
        }
        const cacheKey = `${HubPermissions.cacheKey}_${modKey}_userPermissions`;
        let userPermissions: IUserPermissions | null = null;
        const permissionKey = modKey || "default";
        if (!HubPermissions.modifiedUserPermissions[modKey]) {
            const serialized = localStorage.getItem(cacheKey);
            if (serialized) {
                try {
                    const deserialized: IDeserializedUserPermissions = JSON.parse(serialized);
                    const timeCacheExpires = DateTimeOffset.parse(deserialized.timeCacheExpires) || DateTimeOffset.max();
                    if (timeCacheExpires > DateTimeOffset.now()) {
                        userPermissions = deserialized.permissions;
                    }
                }
                catch {
                    localStorage.setItem(cacheKey, "");
                }
            }
        }
        if (!userPermissions) {
            HubPermissions.isUserInProgress = true;
            try {
                userPermissions = await this.getUserPermissions(modKey);
                HubPermissions.modifiedUserPermissions[permissionKey] = userPermissions;
            }
            catch { }
            HubPermissions.isUserInProgress = false;
            const timeCacheExpires = DateTimeOffset.now().addDays(1);
            const serialized = JSON.stringify({
                timeCacheExpires: timeCacheExpires.toJSON(),
                permissions: userPermissions
            });
            localStorage.setItem(cacheKey, serialized);
        }
        return userPermissions;
    }

    private getUserPermissions(modKey: string) {
        return this.hubClient.getUserAccess({
            canEditUser: this.hubClient.getAccessRequest(c => c.UserMaintenance.EditUserAction, modKey),
            canAddUser: this.hubClient.getAccessRequest(c => c.Users.AddUserAction, modKey)
        });
    }
}