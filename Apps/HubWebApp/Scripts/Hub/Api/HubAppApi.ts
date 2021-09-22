// Generated code

import { AppApi } from "XtiShared/AppApi";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { UserCacheGroup } from "./UserCacheGroup";
import { AuthGroup } from "./AuthGroup";
import { AuthApiGroup } from "./AuthApiGroup";
import { PermanentLogGroup } from "./PermanentLogGroup";
import { AppsGroup } from "./AppsGroup";
import { AppGroup } from "./AppGroup";
import { AppRegistrationGroup } from "./AppRegistrationGroup";
import { VersionGroup } from "./VersionGroup";
import { ResourceGroupGroup } from "./ResourceGroupGroup";
import { ResourceGroup } from "./ResourceGroup";
import { ModCategoryGroup } from "./ModCategoryGroup";
import { UsersGroup } from "./UsersGroup";
import { UserInquiryGroup } from "./UserInquiryGroup";
import { AppUserGroup } from "./AppUserGroup";
import { AppUserMaintenanceGroup } from "./AppUserMaintenanceGroup";
import { UserMaintenanceGroup } from "./UserMaintenanceGroup";


export class HubAppApi extends AppApi {
	public static readonly DefaultVersion = 'V1169';
	
	constructor(events: AppApiEvents, baseUrl: string, version: string = '') {
		super(events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion);
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserCache = this.addGroup((evts, resourceUrl) => new UserCacheGroup(evts, resourceUrl));
		this.Auth = this.addGroup((evts, resourceUrl) => new AuthGroup(evts, resourceUrl));
		this.AuthApi = this.addGroup((evts, resourceUrl) => new AuthApiGroup(evts, resourceUrl));
		this.PermanentLog = this.addGroup((evts, resourceUrl) => new PermanentLogGroup(evts, resourceUrl));
		this.Apps = this.addGroup((evts, resourceUrl) => new AppsGroup(evts, resourceUrl));
		this.App = this.addGroup((evts, resourceUrl) => new AppGroup(evts, resourceUrl));
		this.AppRegistration = this.addGroup((evts, resourceUrl) => new AppRegistrationGroup(evts, resourceUrl));
		this.Version = this.addGroup((evts, resourceUrl) => new VersionGroup(evts, resourceUrl));
		this.ResourceGroup = this.addGroup((evts, resourceUrl) => new ResourceGroupGroup(evts, resourceUrl));
		this.Resource = this.addGroup((evts, resourceUrl) => new ResourceGroup(evts, resourceUrl));
		this.ModCategory = this.addGroup((evts, resourceUrl) => new ModCategoryGroup(evts, resourceUrl));
		this.Users = this.addGroup((evts, resourceUrl) => new UsersGroup(evts, resourceUrl));
		this.UserInquiry = this.addGroup((evts, resourceUrl) => new UserInquiryGroup(evts, resourceUrl));
		this.AppUser = this.addGroup((evts, resourceUrl) => new AppUserGroup(evts, resourceUrl));
		this.AppUserMaintenance = this.addGroup((evts, resourceUrl) => new AppUserMaintenanceGroup(evts, resourceUrl));
		this.UserMaintenance = this.addGroup((evts, resourceUrl) => new UserMaintenanceGroup(evts, resourceUrl));
	}
	
	readonly User: UserGroup;
	readonly UserCache: UserCacheGroup;
	readonly Auth: AuthGroup;
	readonly AuthApi: AuthApiGroup;
	readonly PermanentLog: PermanentLogGroup;
	readonly Apps: AppsGroup;
	readonly App: AppGroup;
	readonly AppRegistration: AppRegistrationGroup;
	readonly Version: VersionGroup;
	readonly ResourceGroup: ResourceGroupGroup;
	readonly Resource: ResourceGroup;
	readonly ModCategory: ModCategoryGroup;
	readonly Users: UsersGroup;
	readonly UserInquiry: UserInquiryGroup;
	readonly AppUser: AppUserGroup;
	readonly AppUserMaintenance: AppUserMaintenanceGroup;
	readonly UserMaintenance: UserMaintenanceGroup;
}