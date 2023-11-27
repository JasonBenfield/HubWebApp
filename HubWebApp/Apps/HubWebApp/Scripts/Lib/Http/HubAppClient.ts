// Generated code

import { AppClient } from "@jasonbenfield/sharedwebapp/Http/AppClient";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppClientQuery } from "@jasonbenfield/sharedwebapp/Http/AppClientQuery";
import { CurrentUserGroup } from "./CurrentUserGroup";
import { HomeGroup } from "./HomeGroup";
import { InstallationsGroup } from "./InstallationsGroup";
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
import { UserGroupsGroup } from "./UserGroupsGroup";
import { PeriodicGroup } from "./PeriodicGroup";
import { LogsGroup } from "./LogsGroup";
import { UserRolesGroup } from "./UserRolesGroup";


export class HubAppClient extends AppClient {
	constructor(events: AppClientEvents) {
		super(
			events, 
			'Hub', 
			pageContext.EnvironmentName === 'Production' || pageContext.EnvironmentName === 'Staging' ? 'V1408' : 'Current'
		);
		this.CurrentUser = this.addGroup((evts, resourceUrl) => new CurrentUserGroup(evts, resourceUrl));
		this.Home = this.addGroup((evts, resourceUrl) => new HomeGroup(evts, resourceUrl));
		this.Installations = this.addGroup((evts, resourceUrl) => new InstallationsGroup(evts, resourceUrl));
		this.InstallationQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IInstallationQueryRequest, IExpandedInstallation>(evts, resourceUrl.odata('InstallationQuery'), 'InstallationQuery'));
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
		this.UserGroups = this.addGroup((evts, resourceUrl) => new UserGroupsGroup(evts, resourceUrl));
		this.UserQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IUserGroupKey, IExpandedUser>(evts, resourceUrl.odata('UserQuery'), 'UserQuery'));
		this.Periodic = this.addGroup((evts, resourceUrl) => new PeriodicGroup(evts, resourceUrl));
		this.Logs = this.addGroup((evts, resourceUrl) => new LogsGroup(evts, resourceUrl));
		this.SessionQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IEmptyRequest, IExpandedSession>(evts, resourceUrl.odata('SessionQuery'), 'SessionQuery'));
		this.RequestQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IAppRequestQueryRequest, IExpandedRequest>(evts, resourceUrl.odata('RequestQuery'), 'RequestQuery'));
		this.LogEntryQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<ILogEntryQueryRequest, IExpandedLogEntry>(evts, resourceUrl.odata('LogEntryQuery'), 'LogEntryQuery'));
		this.UserRoles = this.addGroup((evts, resourceUrl) => new UserRolesGroup(evts, resourceUrl));
		this.UserRoleQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IUserRoleQueryRequest, IExpandedUserRole>(evts, resourceUrl.odata('UserRoleQuery'), 'UserRoleQuery'));
	}
	
	readonly CurrentUser: CurrentUserGroup;
	readonly Home: HomeGroup;
	readonly Installations: InstallationsGroup;
	readonly InstallationQuery: AppClientQuery<IInstallationQueryRequest, IExpandedInstallation>;
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
	readonly UserGroups: UserGroupsGroup;
	readonly UserQuery: AppClientQuery<IUserGroupKey, IExpandedUser>;
	readonly Periodic: PeriodicGroup;
	readonly Logs: LogsGroup;
	readonly SessionQuery: AppClientQuery<IEmptyRequest, IExpandedSession>;
	readonly RequestQuery: AppClientQuery<IAppRequestQueryRequest, IExpandedRequest>;
	readonly LogEntryQuery: AppClientQuery<ILogEntryQueryRequest, IExpandedLogEntry>;
	readonly UserRoles: UserRolesGroup;
	readonly UserRoleQuery: AppClientQuery<IUserRoleQueryRequest, IExpandedUserRole>;
}