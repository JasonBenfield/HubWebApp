// Generated code

import { AppApi } from "XtiShared/AppApi";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { UserCacheGroup } from "./UserCacheGroup";
import { AppsGroup } from "./AppsGroup";
import { AppGroup } from "./AppGroup";
import { ResourceGroupGroup } from "./ResourceGroupGroup";
import { ResourceGroup } from "./ResourceGroup";
import { ModCategoryGroup } from "./ModCategoryGroup";
import { UsersGroup } from "./UsersGroup";
import { UserInquiryGroup } from "./UserInquiryGroup";
import { UserMaintenanceGroup } from "./UserMaintenanceGroup";
import { AppUserGroup } from "./AppUserGroup";
import { AppUserMaintenanceGroup } from "./AppUserMaintenanceGroup";


export class HubAppApi extends AppApi {
	public static readonly DefaultVersion = 'V21';
	
	constructor(events: AppApiEvents, baseUrl: string, version: string = '') {
		super(events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion);
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserCache = this.addGroup((evts, resourceUrl) => new UserCacheGroup(evts, resourceUrl));
		this.Apps = this.addGroup((evts, resourceUrl) => new AppsGroup(evts, resourceUrl));
		this.App = this.addGroup((evts, resourceUrl) => new AppGroup(evts, resourceUrl));
		this.ResourceGroup = this.addGroup((evts, resourceUrl) => new ResourceGroupGroup(evts, resourceUrl));
		this.Resource = this.addGroup((evts, resourceUrl) => new ResourceGroup(evts, resourceUrl));
		this.ModCategory = this.addGroup((evts, resourceUrl) => new ModCategoryGroup(evts, resourceUrl));
		this.Users = this.addGroup((evts, resourceUrl) => new UsersGroup(evts, resourceUrl));
		this.UserInquiry = this.addGroup((evts, resourceUrl) => new UserInquiryGroup(evts, resourceUrl));
		this.UserMaintenance = this.addGroup((evts, resourceUrl) => new UserMaintenanceGroup(evts, resourceUrl));
		this.AppUser = this.addGroup((evts, resourceUrl) => new AppUserGroup(evts, resourceUrl));
		this.AppUserMaintenance = this.addGroup((evts, resourceUrl) => new AppUserMaintenanceGroup(evts, resourceUrl));
	}
	
	readonly User: UserGroup;
	readonly UserCache: UserCacheGroup;
	readonly Apps: AppsGroup;
	readonly App: AppGroup;
	readonly ResourceGroup: ResourceGroupGroup;
	readonly Resource: ResourceGroup;
	readonly ModCategory: ModCategoryGroup;
	readonly Users: UsersGroup;
	readonly UserInquiry: UserInquiryGroup;
	readonly UserMaintenance: UserMaintenanceGroup;
	readonly AppUser: AppUserGroup;
	readonly AppUserMaintenance: AppUserMaintenanceGroup;
}