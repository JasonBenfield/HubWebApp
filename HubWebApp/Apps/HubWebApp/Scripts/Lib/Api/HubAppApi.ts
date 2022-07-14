// Generated code

import { AppApi } from "@jasonbenfield/sharedwebapp/Api/AppApi";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppApiQuery } from "@jasonbenfield/sharedwebapp/Api/AppApiQuery";
import { UserGroup } from "./UserGroup";
import { UserCacheGroup } from "./UserCacheGroup";
import { HomeGroup } from "./HomeGroup";
import { AuthGroup } from "./AuthGroup";
import { AuthApiGroup } from "./AuthApiGroup";
import { ExternalAuthGroup } from "./ExternalAuthGroup";
import { AuthenticatorsGroup } from "./AuthenticatorsGroup";
import { PermanentLogGroup } from "./PermanentLogGroup";
import { AppsGroup } from "./AppsGroup";
import { AppGroup } from "./AppGroup";
import { InstallGroup } from "./InstallGroup";
import { PublishGroup } from "./PublishGroup";
import { VersionGroup } from "./VersionGroup";
import { ResourceGroupGroup } from "./ResourceGroupGroup";
import { ResourceGroup } from "./ResourceGroup";
import { ModCategoryGroup } from "./ModCategoryGroup";
import { UsersGroup } from "./UsersGroup";
import { UserInquiryGroup } from "./UserInquiryGroup";
import { AppUserGroup } from "./AppUserGroup";
import { AppUserMaintenanceGroup } from "./AppUserMaintenanceGroup";
import { UserMaintenanceGroup } from "./UserMaintenanceGroup";
import { StorageGroup } from "./StorageGroup";
import { SystemGroup } from "./SystemGroup";


export class HubAppApi extends AppApi {
	constructor(events: AppApiEvents) {
		super(events, 'Hub');
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserCache = this.addGroup((evts, resourceUrl) => new UserCacheGroup(evts, resourceUrl));
		this.Home = this.addGroup((evts, resourceUrl) => new HomeGroup(evts, resourceUrl));
		this.Auth = this.addGroup((evts, resourceUrl) => new AuthGroup(evts, resourceUrl));
		this.AuthApi = this.addGroup((evts, resourceUrl) => new AuthApiGroup(evts, resourceUrl));
		this.ExternalAuth = this.addGroup((evts, resourceUrl) => new ExternalAuthGroup(evts, resourceUrl));
		this.Authenticators = this.addGroup((evts, resourceUrl) => new AuthenticatorsGroup(evts, resourceUrl));
		this.PermanentLog = this.addGroup((evts, resourceUrl) => new PermanentLogGroup(evts, resourceUrl));
		this.Apps = this.addGroup((evts, resourceUrl) => new AppsGroup(evts, resourceUrl));
		this.App = this.addGroup((evts, resourceUrl) => new AppGroup(evts, resourceUrl));
		this.Install = this.addGroup((evts, resourceUrl) => new InstallGroup(evts, resourceUrl));
		this.Publish = this.addGroup((evts, resourceUrl) => new PublishGroup(evts, resourceUrl));
		this.Version = this.addGroup((evts, resourceUrl) => new VersionGroup(evts, resourceUrl));
		this.ResourceGroup = this.addGroup((evts, resourceUrl) => new ResourceGroupGroup(evts, resourceUrl));
		this.Resource = this.addGroup((evts, resourceUrl) => new ResourceGroup(evts, resourceUrl));
		this.ModCategory = this.addGroup((evts, resourceUrl) => new ModCategoryGroup(evts, resourceUrl));
		this.Users = this.addGroup((evts, resourceUrl) => new UsersGroup(evts, resourceUrl));
		this.UserInquiry = this.addGroup((evts, resourceUrl) => new UserInquiryGroup(evts, resourceUrl));
		this.AppUser = this.addGroup((evts, resourceUrl) => new AppUserGroup(evts, resourceUrl));
		this.AppUserMaintenance = this.addGroup((evts, resourceUrl) => new AppUserMaintenanceGroup(evts, resourceUrl));
		this.UserMaintenance = this.addGroup((evts, resourceUrl) => new UserMaintenanceGroup(evts, resourceUrl));
		this.Storage = this.addGroup((evts, resourceUrl) => new StorageGroup(evts, resourceUrl));
		this.System = this.addGroup((evts, resourceUrl) => new SystemGroup(evts, resourceUrl));
	}
	
	readonly User: UserGroup;
	readonly UserCache: UserCacheGroup;
	readonly Home: HomeGroup;
	readonly Auth: AuthGroup;
	readonly AuthApi: AuthApiGroup;
	readonly ExternalAuth: ExternalAuthGroup;
	readonly Authenticators: AuthenticatorsGroup;
	readonly PermanentLog: PermanentLogGroup;
	readonly Apps: AppsGroup;
	readonly App: AppGroup;
	readonly Install: InstallGroup;
	readonly Publish: PublishGroup;
	readonly Version: VersionGroup;
	readonly ResourceGroup: ResourceGroupGroup;
	readonly Resource: ResourceGroup;
	readonly ModCategory: ModCategoryGroup;
	readonly Users: UsersGroup;
	readonly UserInquiry: UserInquiryGroup;
	readonly AppUser: AppUserGroup;
	readonly AppUserMaintenance: AppUserMaintenanceGroup;
	readonly UserMaintenance: UserMaintenanceGroup;
	readonly Storage: StorageGroup;
	readonly System: SystemGroup;
}