// Generated code

interface ILinkModel {
	LinkName: string;
	DisplayText: string;
	Url: string;
	IsAuthenticationRequired: boolean;
}
interface IAppUserModel {
	ID: number;
	UserName: IAppUserName;
	Name: IPersonName;
	Email: string;
	TimeDeactivated: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
}
interface IAppUserName {
	Value: string;
	DisplayText: string;
}
interface IPersonName {
	Value: string;
	DisplayText: string;
}
interface IInstallationQueryRequest {
	QueryType: number;
}
interface IInstallationViewRequest {
	InstallationID: number;
}
interface IInstallationDetailModel {
	InstallLocation: IInstallLocationModel;
	Installation: IInstallationModel;
	Version: IXtiVersionModel;
	App: IAppModel;
	MostRecentRequest: IAppRequestModel;
}
interface IInstallLocationModel {
	ID: number;
	QualifiedMachineName: string;
}
interface IInstallationModel {
	ID: number;
	Status: IInstallStatus;
	IsCurrent: boolean;
	Domain: string;
	SiteName: string;
}
interface IXtiVersionModel {
	ID: number;
	VersionName: IAppVersionName;
	VersionKey: IAppVersionKey;
	VersionNumber: IAppVersionNumber;
	VersionType: IAppVersionType;
	Status: IAppVersionStatus;
	TimeAdded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
}
interface IAppVersionName {
	Value: string;
	DisplayText: string;
}
interface IAppVersionKey {
	Value: string;
	DisplayText: string;
}
interface IAppVersionNumber {
	Major: number;
	Minor: number;
	Patch: number;
}
interface IAppModel {
	ID: number;
	AppKey: IAppKey;
	VersionName: IAppVersionName;
	PublicKey: IModifierKey;
}
interface IAppKey {
	Name: IAppName;
	Type: IAppType;
}
interface IAppName {
	Value: string;
	DisplayText: string;
}
interface IModifierKey {
	Value: string;
	DisplayText: string;
}
interface IAppRequestModel {
	ID: number;
	SessionID: number;
	Path: string;
	ResourceID: number;
	ModifierID: number;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
}
interface IGetPendingDeletesRequest {
	MachineNames: string[];
}
interface IAppVersionInstallationModel {
	App: IAppModel;
	Version: IXtiVersionModel;
	Installation: IInstallationModel;
}
interface IGetInstallationRequest {
	InstallationID: number;
}
interface IExpandedInstallation {
	InstallationID: number;
	IsCurrent: boolean;
	InstallationStatus: string;
	TimeInstallationAdded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	QualifiedMachineName: string;
	Domain: string;
	AppID: number;
	AppKey: string;
	AppName: string;
	AppType: string;
	VersionName: string;
	VersionRelease: string;
	VersionKey: string;
	VersionStatus: string;
	VersionType: string;
	LastRequestTime: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	LastRequestDaysAgo: number;
	RequestCount: number;
}
interface IAuthenticatedLoginResult {
	AuthKey: string;
	AuthID: string;
}
interface IAuthenticatedLoginRequest {
	AuthKey: string;
	AuthID: string;
	ReturnKey: string;
}
interface ILoginReturnModel {
	ReturnUrl: string;
}
interface ILoginCredentials {
	UserName: string;
	Password: string;
}
interface ILoginResult {
	Token: string;
}
interface IExternalAuthKeyModel {
	AuthenticatorKey: string;
	ExternalUserKey: string;
}
interface IMoveAuthenticatorRequest {
	AuthenticatorKey: string;
	ExternalUserKey: string;
	TargetUserID: number;
}
interface IRegisterAuthenticatorRequest {
	AuthenticatorName: string;
}
interface IAuthenticatorModel {
	ID: number;
	AuthenticatorKey: IAuthenticatorKey;
}
interface IAuthenticatorKey {
	Value: string;
	DisplayText: string;
}
interface IRegisterUserAuthenticatorRequest {
	AuthenticatorKey: string;
	UserID: number;
	ExternalUserKey: string;
}
interface IUserOrAnonByAuthenticatorRequest {
	AuthenticatorKey: string;
	ExternalUserKey: string;
}
interface ILogBatchModel {
	StartSessions: IStartSessionModel[];
	StartRequests: IStartRequestModel[];
	LogEntries: ILogEntryModel[];
	EndRequests: IEndRequestModel[];
	AuthenticateSessions: IAuthenticateSessionModel[];
	EndSessions: IEndSessionModel[];
}
interface IStartSessionModel {
	SessionKey: string;
	UserName: string;
	RequesterKey: string;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RemoteAddress: string;
	UserAgent: string;
}
interface IStartRequestModel {
	RequestKey: string;
	SessionKey: string;
	SourceRequestKey: string;
	Path: string;
	InstallationID: number;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	ActualCount: number;
}
interface ILogEntryModel {
	EventKey: string;
	RequestKey: string;
	Severity: number;
	TimeOccurred: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	Caption: string;
	Message: string;
	Detail: string;
	ActualCount: number;
	ParentEventKey: string;
	Category: string;
}
interface IEndRequestModel {
	RequestKey: string;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
}
interface IAuthenticateSessionModel {
	SessionKey: string;
	UserName: string;
}
interface IEndSessionModel {
	SessionKey: string;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
}
interface IAppDomainModel {
	AppKey: IAppKey;
	VersionKey: IAppVersionKey;
	Domain: string;
}
interface IResourceGroupModel {
	ID: number;
	ModCategoryID: number;
	Name: IResourceGroupName;
	IsAnonymousAllowed: boolean;
}
interface IResourceGroupName {
	Value: string;
	DisplayText: string;
}
interface IAppRoleModel {
	ID: number;
	Name: IAppRoleName;
}
interface IAppRoleName {
	Value: string;
	DisplayText: string;
}
interface IAppRequestExpandedModel {
	ID: number;
	UserName: string;
	GroupName: string;
	ActionName: string;
	ResultType: IResourceResultType;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
}
interface IAppLogEntryModel {
	ID: number;
	RequestID: number;
	TimeOccurred: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	Severity: IAppEventSeverity;
	Caption: string;
	Message: string;
	Detail: string;
	Category: string;
}
interface IModifierCategoryModel {
	ID: number;
	Name: IModifierCategoryName;
}
interface IModifierCategoryName {
	Value: string;
	DisplayText: string;
}
interface IModifierModel {
	ID: number;
	CategoryID: number;
	ModKey: IModifierKey;
	TargetKey: string;
	DisplayText: string;
}
interface IRegisterAppRequest {
	VersionKey: IAppVersionKey;
	AppTemplate: IAppApiTemplateModel;
}
interface IAppApiTemplateModel {
	AppKey: IAppKey;
	GroupTemplates: IAppApiGroupTemplateModel[];
}
interface IAppApiGroupTemplateModel {
	Name: IResourceGroupName;
	ModCategory: IModifierCategoryName;
	IsAnonymousAllowed: boolean;
	Roles: IAppRoleName[];
	ActionTemplates: IAppApiActionTemplateModel[];
}
interface IAppApiActionTemplateModel {
	Name: IResourceName;
	IsAnonymousAllowed: boolean;
	Roles: IAppRoleName[];
	ResultType: IResourceResultType;
}
interface IResourceName {
	Value: string;
	DisplayText: string;
}
interface IAddOrUpdateAppsRequest {
	VersionName: string;
	AppKeys: IAppKeyRequest[];
}
interface IAppKeyRequest {
	AppName: string;
	AppType: number;
}
interface IAddOrUpdateVersionsRequest {
	AppKeys: IAppKeyRequest[];
	Versions: IAddVersionRequest[];
}
interface IAddVersionRequest {
	VersionName: string;
	VersionKey: string;
	VersionType: number;
	VersionStatus: number;
	VersionNumber: IAppVersionNumberRequest;
}
interface IAppVersionNumberRequest {
	Major: number;
	Minor: number;
	Patch: number;
}
interface IGetVersionRequest {
	VersionName: string;
	VersionKey: string;
}
interface IGetVersionsRequest {
	VersionName: string;
}
interface IAddSystemUserRequest {
	AppKey: IAppKeyRequest;
	MachineName: string;
	Password: string;
}
interface IAddAdminUserRequest {
	AppKey: IAppKeyRequest;
	UserName: string;
	Password: string;
}
interface IAddInstallationUserRequest {
	MachineName: string;
	Password: string;
}
interface INewInstallationRequest {
	VersionName: string;
	AppKey: IAppKeyRequest;
	QualifiedMachineName: string;
	Domain: string;
	SiteName: string;
}
interface INewInstallationResult {
	CurrentInstallationID: number;
	VersionInstallationID: number;
}
interface ISetUserAccessRequest {
	UserName: string;
	RoleAssignments: ISetUserAccessRoleRequest[];
}
interface ISetUserAccessRoleRequest {
	AppKey: IAppKeyRequest;
	ModCategoryName: string;
	ModKey: string;
	RoleNames: string[];
}
interface INewVersionRequest {
	VersionName: string;
	VersionType: number;
}
interface IPublishVersionRequest {
	VersionName: string;
	VersionKey: string;
}
interface IGetResourceGroupRequest {
	VersionKey: string;
	GroupID: number;
}
interface IGetResourcesRequest {
	VersionKey: string;
	GroupID: number;
}
interface IResourceModel {
	ID: number;
	Name: IResourceName;
	IsAnonymousAllowed: boolean;
	ResultType: IResourceResultType;
}
interface IGetResourceGroupRoleAccessRequest {
	VersionKey: string;
	GroupID: number;
}
interface IGetResourceGroupModCategoryRequest {
	VersionKey: string;
	GroupID: number;
}
interface IGetResourceGroupLogRequest {
	VersionKey: string;
	GroupID: number;
	HowMany: number;
}
interface IGetResourceRequest {
	VersionKey: string;
	ResourceID: number;
}
interface IGetResourceRoleAccessRequest {
	VersionKey: string;
	ResourceID: number;
}
interface IGetResourceLogRequest {
	VersionKey: string;
	ResourceID: number;
	HowMany: number;
}
interface IUsersIndexRequest {
	UserID: number;
	ReturnTo: string;
}
interface IAppUserGroupModel {
	ID: number;
	GroupName: IAppUserGroupName;
	PublicKey: IModifierKey;
}
interface IAppUserGroupName {
	Value: string;
	DisplayText: string;
}
interface IAddOrUpdateUserRequest {
	UserName: string;
	Password: string;
	PersonName: string;
	Email: string;
}
interface IAppUserIDRequest {
	UserID: number;
}
interface IAppUserNameRequest {
	UserName: string;
}
interface IUserAuthenticatorModel {
	Authenticator: IAuthenticatorModel;
	ExternalUserID: string;
}
interface IGetAppUserRequest {
	App: string;
	UserID: number;
}
interface IUserModifierKey {
	UserID: number;
	ModifierID: number;
}
interface IUserAccessModel {
	HasAccess: boolean;
	AssignedRoles: IAppRoleModel[];
}
interface IUserRoleRequest {
	UserID: number;
	ModifierID: number;
	RoleID: number;
}
interface IStoreObjectRequest {
	StorageName: string;
	Data: string;
	ExpireAfter: import('@jasonbenfield/sharedwebapp/Common').TimeSpan;
	GenerateKey: IGenerateKeyModel;
	IsSingleUse: boolean;
}
interface IGenerateKeyModel {
	KeyType: IGeneratedKeyType;
	Value: string;
}
interface IGetStoredObjectRequest {
	StorageName: string;
	StorageKey: string;
}
interface IGetAppContextRequest {
	InstallationID: number;
}
interface IAppContextModel {
	App: IAppModel;
	Version: IXtiVersionModel;
	Roles: IAppRoleModel[];
	ModifierCategories: IAppContextModifierCategoryModel[];
	ResourceGroups: IAppContextResourceGroupModel[];
}
interface IAppContextModifierCategoryModel {
	ModifierCategory: IModifierCategoryModel;
	Modifiers: IModifierModel[];
}
interface IAppContextResourceGroupModel {
	ResourceGroup: IResourceGroupModel;
	Resources: IAppContextResourceModel[];
	AllowedRoles: IAppRoleModel[];
}
interface IAppContextResourceModel {
	Resource: IResourceModel;
	AllowedRoles: IAppRoleModel[];
}
interface IGetUserContextRequest {
	InstallationID: number;
	UserName: string;
}
interface IUserContextModel {
	User: IAppUserModel;
	ModifiedRoles: IUserContextRoleModel[];
}
interface IUserContextRoleModel {
	ModifierCategory: IModifierCategoryModel;
	Modifier: IModifierModel;
	Roles: IAppRoleModel[];
}
interface ISystemAddOrUpdateModifierByTargetKeyRequest {
	InstallationID: number;
	ModCategoryName: string;
	GenerateModKey: IGenerateKeyModel;
	TargetKey: string;
	TargetDisplayText: string;
}
interface ISystemAddOrUpdateModifierByModKeyRequest {
	InstallationID: number;
	ModCategoryName: string;
	ModKey: string;
	TargetKey: string;
	TargetDisplayText: string;
}
interface ISystemGetUsersWithAnyRoleRequest {
	InstallationID: number;
	ModCategoryName: string;
	ModKey: string;
	RoleNames: string[];
}
interface ISystemSetUserAccessRequest {
	InstallationID: number;
	UserName: string;
	RoleAssignments: ISystemSetUserAccessRoleRequest[];
}
interface ISystemSetUserAccessRoleRequest {
	ModCategoryName: string;
	ModKey: string;
	RoleNames: string[];
}
interface IUserGroupKey {
	UserGroupName: string;
}
interface IAddUserGroupIfNotExistsRequest {
	GroupName: string;
}
interface IAppUserDetailModel {
	User: IAppUserModel;
	UserGroup: IAppUserGroupModel;
}
interface IExpandedUser {
	UserID: number;
	UserName: string;
	PersonName: string;
	Email: string;
	TimeUserAdded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	UserGroupID: number;
	UserGroupName: string;
	TimeUserDeactivated: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	IsActive: boolean;
}
interface IAppLogEntryDetailModel {
	LogEntry: IAppLogEntryModel;
	Request: IAppRequestModel;
	ResourceGroup: IResourceGroupModel;
	Resource: IResourceModel;
	ModCategory: IModifierCategoryModel;
	Modifier: IModifierModel;
	InstallLocation: IInstallLocationModel;
	Installation: IInstallationModel;
	Version: IXtiVersionModel;
	App: IAppModel;
	Session: IAppSessionModel;
	UserGroup: IAppUserGroupModel;
	User: IAppUserModel;
	SourceLogEntryID: number;
	TargetLogEntryID: number;
}
interface IAppSessionModel {
	ID: number;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RemoteAddress: string;
	UserAgent: string;
}
interface IAppRequestDetailModel {
	Request: IAppRequestModel;
	ResourceGroup: IResourceGroupModel;
	Resource: IResourceModel;
	ModCategory: IModifierCategoryModel;
	Modifier: IModifierModel;
	InstallLocation: IInstallLocationModel;
	Installation: IInstallationModel;
	Version: IXtiVersionModel;
	App: IAppModel;
	Session: IAppSessionModel;
	UserGroup: IAppUserGroupModel;
	User: IAppUserModel;
	SourceRequestID: number;
	TargetRequestIDs: number[];
}
interface IAppSessionDetailModel {
	Session: IAppSessionModel;
	UserGroup: IAppUserGroupModel;
	User: IAppUserModel;
}
interface ISessionViewRequest {
	SessionID: number;
}
interface IAppRequestQueryRequest {
	SessionID: number;
	InstallationID: number;
	SourceRequestID: number;
}
interface IAppRequestRequest {
	RequestID: number;
}
interface ILogEntryRequest {
	LogEntryID: number;
}
interface ILogEntryQueryRequest {
	RequestID: number;
	InstallationID: number;
}
interface IExpandedSession {
	SessionID: number;
	UserID: number;
	UserName: string;
	UserGroupID: number;
	UserGroupName: string;
	UserGroupDisplayText: string;
	RemoteAddress: string;
	UserAgent: string;
	TimeSessionStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeSessionEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeElapsed: string;
	LastRequestTime: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RequestCount: number;
}
interface IExpandedRequest {
	RequestID: number;
	Path: string;
	AppID: number;
	AppKey: string;
	AppName: string;
	AppTypeText: string;
	ResourceGroupName: string;
	ResourceName: string;
	ModCategoryName: string;
	ModKey: string;
	ModTargetKey: string;
	ModDisplayText: string;
	ActualCount: number;
	SessionID: number;
	UserName: string;
	UserGroupID: number;
	UserGroupName: string;
	UserGroupDisplayText: string;
	RequestTimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RequestTimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RequestTimeElapsed: string;
	Succeeded: boolean;
	CriticalErrorCount: number;
	ValidationFailedCount: number;
	AppErrorCount: number;
	TotalErrorCount: number;
	InformationMessageCount: number;
	VersionName: string;
	VersionKey: string;
	VersionRelease: string;
	VersionStatus: string;
	VersionType: string;
	InstallationID: number;
	InstallLocation: string;
	IsCurrentInstallation: boolean;
	SourceRequestID: number;
}
interface IExpandedLogEntry {
	EventID: number;
	TimeOccurred: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	Severity: string;
	Caption: string;
	Message: string;
	Detail: string;
	Category: string;
	Path: string;
	ActualCount: number;
	AppID: number;
	AppKey: string;
	AppName: string;
	AppType: string;
	ResourceGroupName: string;
	ResourceName: string;
	ModCategoryName: string;
	ModKey: string;
	ModTargetKey: string;
	ModDisplayText: string;
	UserName: string;
	UserGroupID: number;
	UserGroupName: string;
	UserGroupDisplayText: string;
	RequestID: number;
	RequestTimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RequestTimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RequestTimeElapsed: string;
	VersionName: string;
	VersionKey: string;
	VersionRelease: string;
	VersionStatus: string;
	VersionType: string;
	InstallationID: number;
	InstallLocation: string;
	IsCurrentInstallation: boolean;
	SourceID: number;
}
interface IUserRoleIDRequest {
	UserRoleID: number;
}
interface IUserRoleQueryRequest {
	AppID: number;
}
interface IUserRoleDetailModel {
	ID: number;
	UserGroup: IAppUserGroupModel;
	User: IAppUserModel;
	App: IAppModel;
	Role: IAppRoleModel;
	ModCategory: IModifierCategoryModel;
	Modifier: IModifierModel;
}
interface IExpandedUserRole {
	UserRoleID: number;
	UserGroupDisplayText: string;
	UserName: string;
	ModCategoryName: string;
	ModDisplayText: string;
	RoleDisplayText: string;
	AppKey: string;
	AppID: number;
	UserGroupID: number;
	UserID: number;
	RoleID: number;
	ModCategoryID: number;
	ModifierID: number;
}
interface IInstallationQueryType {
	Value: number;
	DisplayText: string;
}
interface IInstallStatus {
	Value: number;
	DisplayText: string;
}
interface IAppVersionType {
	Value: number;
	DisplayText: string;
}
interface IAppVersionStatus {
	Value: number;
	DisplayText: string;
}
interface IAppType {
	Value: number;
	DisplayText: string;
}
interface IResourceResultType {
	Value: number;
	DisplayText: string;
}
interface IAppEventSeverity {
	Value: number;
	DisplayText: string;
}
interface IGeneratedKeyType {
	Value: number;
	DisplayText: string;
}