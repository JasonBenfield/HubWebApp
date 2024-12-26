// Generated code

import { AppClient } from "@jasonbenfield/sharedwebapp/Http/AppClient";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppClientQuery } from "@jasonbenfield/sharedwebapp/Http/AppClientQuery";
import { AppGroup } from "./AppGroup";
import { AppsGroup } from "./AppsGroup";
import { AppUserInquiryGroup } from "./AppUserInquiryGroup";
import { AppUserMaintenanceGroup } from "./AppUserMaintenanceGroup";
import { AuthGroup } from "./AuthGroup";
import { AuthApiGroup } from "./AuthApiGroup";
import { AuthenticatorsGroup } from "./AuthenticatorsGroup";
import { CurrentUserGroup } from "./CurrentUserGroup";
import { ExternalAuthGroup } from "./ExternalAuthGroup";
import { HomeGroup } from "./HomeGroup";
import { InstallGroup } from "./InstallGroup";
import { InstallationsGroup } from "./InstallationsGroup";
import { LogsGroup } from "./LogsGroup";
import { ModCategoryGroup } from "./ModCategoryGroup";
import { PeriodicGroup } from "./PeriodicGroup";
import { PermanentLogGroup } from "./PermanentLogGroup";
import { PublishGroup } from "./PublishGroup";
import { ResourceGroupInquiryGroup } from "./ResourceGroupInquiryGroup";
import { ResourceInquiryGroup } from "./ResourceInquiryGroup";
import { StorageGroup } from "./StorageGroup";
import { SystemGroup } from "./SystemGroup";
import { UserGroupsGroup } from "./UserGroupsGroup";
import { UserInquiryGroup } from "./UserInquiryGroup";
import { UserMaintenanceGroup } from "./UserMaintenanceGroup";
import { UserRolesGroup } from "./UserRolesGroup";
import { UsersGroup } from "./UsersGroup";
import { VersionGroup } from "./VersionGroup";


export class HubAppClient extends AppClient {
	constructor(events: AppClientEvents) {
		super(
			events, 
			'Hub', 
			pageContext.EnvironmentName === 'Production' || pageContext.EnvironmentName === 'Staging' ? 'V1433' : 'Current'
		);
		this.App = this.addGroup((evts, resourceUrl) => new AppGroup(evts, resourceUrl));
		this.Apps = this.addGroup((evts, resourceUrl) => new AppsGroup(evts, resourceUrl));
		this.AppUserInquiry = this.addGroup((evts, resourceUrl) => new AppUserInquiryGroup(evts, resourceUrl));
		this.AppUserMaintenance = this.addGroup((evts, resourceUrl) => new AppUserMaintenanceGroup(evts, resourceUrl));
		this.Auth = this.addGroup((evts, resourceUrl) => new AuthGroup(evts, resourceUrl));
		this.AuthApi = this.addGroup((evts, resourceUrl) => new AuthApiGroup(evts, resourceUrl));
		this.Authenticators = this.addGroup((evts, resourceUrl) => new AuthenticatorsGroup(evts, resourceUrl));
		this.CurrentUser = this.addGroup((evts, resourceUrl) => new CurrentUserGroup(evts, resourceUrl));
		this.ExternalAuth = this.addGroup((evts, resourceUrl) => new ExternalAuthGroup(evts, resourceUrl));
		this.Home = this.addGroup((evts, resourceUrl) => new HomeGroup(evts, resourceUrl));
		this.Install = this.addGroup((evts, resourceUrl) => new InstallGroup(evts, resourceUrl));
		this.Installations = this.addGroup((evts, resourceUrl) => new InstallationsGroup(evts, resourceUrl));
		this.Logs = this.addGroup((evts, resourceUrl) => new LogsGroup(evts, resourceUrl));
		this.ModCategory = this.addGroup((evts, resourceUrl) => new ModCategoryGroup(evts, resourceUrl));
		this.Periodic = this.addGroup((evts, resourceUrl) => new PeriodicGroup(evts, resourceUrl));
		this.PermanentLog = this.addGroup((evts, resourceUrl) => new PermanentLogGroup(evts, resourceUrl));
		this.Publish = this.addGroup((evts, resourceUrl) => new PublishGroup(evts, resourceUrl));
		this.ResourceGroupInquiry = this.addGroup((evts, resourceUrl) => new ResourceGroupInquiryGroup(evts, resourceUrl));
		this.ResourceInquiry = this.addGroup((evts, resourceUrl) => new ResourceInquiryGroup(evts, resourceUrl));
		this.Storage = this.addGroup((evts, resourceUrl) => new StorageGroup(evts, resourceUrl));
		this.System = this.addGroup((evts, resourceUrl) => new SystemGroup(evts, resourceUrl));
		this.UserGroups = this.addGroup((evts, resourceUrl) => new UserGroupsGroup(evts, resourceUrl));
		this.UserInquiry = this.addGroup((evts, resourceUrl) => new UserInquiryGroup(evts, resourceUrl));
		this.UserMaintenance = this.addGroup((evts, resourceUrl) => new UserMaintenanceGroup(evts, resourceUrl));
		this.UserRoles = this.addGroup((evts, resourceUrl) => new UserRolesGroup(evts, resourceUrl));
		this.Users = this.addGroup((evts, resourceUrl) => new UsersGroup(evts, resourceUrl));
		this.Version = this.addGroup((evts, resourceUrl) => new VersionGroup(evts, resourceUrl));
		this.InstallationQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IInstallationQueryRequest, IExpandedInstallation>(evts, resourceUrl.odata('InstallationQuery'), 'InstallationQuery'));
		this.AppRequestQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IAppRequestQueryRequest, IExpandedRequest>(evts, resourceUrl.odata('AppRequestQuery'), 'AppRequestQuery'));
		this.LogEntryQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<ILogEntryQueryRequest, IExpandedLogEntry>(evts, resourceUrl.odata('LogEntryQuery'), 'LogEntryQuery'));
		this.SessionQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IEmptyRequest, IExpandedSession>(evts, resourceUrl.odata('SessionQuery'), 'SessionQuery'));
		this.UserQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IUserGroupKey, IExpandedUser>(evts, resourceUrl.odata('UserQuery'), 'UserQuery'));
		this.UserRoleQuery = this.addODataGroup((evts, resourceUrl) => new AppClientQuery<IUserRoleQueryRequest, IExpandedUserRole>(evts, resourceUrl.odata('UserRoleQuery'), 'UserRoleQuery'));
	}
	
	readonly App: AppGroup;
	readonly Apps: AppsGroup;
	readonly AppUserInquiry: AppUserInquiryGroup;
	readonly AppUserMaintenance: AppUserMaintenanceGroup;
	readonly Auth: AuthGroup;
	readonly AuthApi: AuthApiGroup;
	readonly Authenticators: AuthenticatorsGroup;
	readonly CurrentUser: CurrentUserGroup;
	readonly ExternalAuth: ExternalAuthGroup;
	readonly Home: HomeGroup;
	readonly Install: InstallGroup;
	readonly Installations: InstallationsGroup;
	readonly Logs: LogsGroup;
	readonly ModCategory: ModCategoryGroup;
	readonly Periodic: PeriodicGroup;
	readonly PermanentLog: PermanentLogGroup;
	readonly Publish: PublishGroup;
	readonly ResourceGroupInquiry: ResourceGroupInquiryGroup;
	readonly ResourceInquiry: ResourceInquiryGroup;
	readonly Storage: StorageGroup;
	readonly System: SystemGroup;
	readonly UserGroups: UserGroupsGroup;
	readonly UserInquiry: UserInquiryGroup;
	readonly UserMaintenance: UserMaintenanceGroup;
	readonly UserRoles: UserRolesGroup;
	readonly Users: UsersGroup;
	readonly Version: VersionGroup;
	readonly InstallationQuery: AppClientQuery<IInstallationQueryRequest, IExpandedInstallation>;
	readonly AppRequestQuery: AppClientQuery<IAppRequestQueryRequest, IExpandedRequest>;
	readonly LogEntryQuery: AppClientQuery<ILogEntryQueryRequest, IExpandedLogEntry>;
	readonly SessionQuery: AppClientQuery<IEmptyRequest, IExpandedSession>;
	readonly UserQuery: AppClientQuery<IUserGroupKey, IExpandedUser>;
	readonly UserRoleQuery: AppClientQuery<IUserRoleQueryRequest, IExpandedUserRole>;
}