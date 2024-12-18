// Generated code

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
interface IAppVersionName {
	Value: string;
	DisplayText: string;
}
interface IModifierKey {
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
interface IModifierCategoryModel {
	ID: number;
	Name: IModifierCategoryName;
}
interface IModifierCategoryName {
	Value: string;
	DisplayText: string;
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
	ActualCount: number;
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
interface IAppDomainModel {
	AppKey: IAppKey;
	VersionKey: IAppVersionKey;
	Domain: string;
}
interface IAppVersionKey {
	Value: string;
	DisplayText: string;
}
interface IUserModifierKey {
	UserID: number;
	ModifierID: number;
}
interface IUserAccessModel {
	HasAccess: boolean;
	AssignedRoles: IAppRoleModel[];
}
interface IAppUserIndexRequest {
	App: string;
	UserID: number;
	ReturnTo: string;
}
interface IUserRoleRequest {
	UserID: number;
	ModifierID: number;
	RoleID: number;
}
interface IAuthenticatedLoginRequest {
	AuthKey: string;
	AuthID: string;
	ReturnKey: string;
}
interface ILoginReturnModel {
	ReturnUrl: string;
}
interface IAuthenticatedLoginResult {
	AuthKey: string;
	AuthID: string;
}
interface IAuthenticateRequest {
	UserName: string;
	Password: string;
}
interface ILoginResult {
	Token: string;
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
interface IExternalAuthKeyModel {
	AuthenticatorKey: string;
	ExternalUserKey: string;
}
interface IAddAdminUserRequest {
	AppKey: IAppKeyRequest;
	UserName: string;
	Password: string;
}
interface IAppKeyRequest {
	AppName: string;
	AppType: number;
}
interface IAddInstallationUserRequest {
	MachineName: string;
	Password: string;
}
interface IAddOrUpdateAppsRequest {
	VersionName: string;
	AppKeys: IAppKeyRequest[];
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
interface IAddSystemUserRequest {
	AppKey: IAppKeyRequest;
	MachineName: string;
	Password: string;
}
interface IGetInstallationRequest {
	InstallationID: number;
}
interface IConfigureInstallRequest {
	RepoOwner: string;
	RepoName: string;
	ConfigurationName: string;
	AppKey: IAppKeyRequest;
	TemplateName: string;
	InstallSequence: number;
}
interface IInstallConfigurationModel {
	ID: number;
	ConfigurationName: string;
	AppKey: IAppKey;
	Template: IInstallConfigurationTemplateModel;
	InstallSequence: number;
}
interface IInstallConfigurationTemplateModel {
	ID: number;
	TemplateName: string;
	DestinationMachineName: string;
	Domain: string;
	SiteName: string;
}
interface IConfigureInstallTemplateRequest {
	TemplateName: string;
	DestinationMachineName: string;
	Domain: string;
	SiteName: string;
}
interface IDeleteInstallConfigurationRequest {
	RepoOwner: string;
	RepoName: string;
	ConfigurationName: string;
	AppKey: IAppKeyRequest;
}
interface IGetInstallConfigurationsRequest {
	RepoOwner: string;
	RepoName: string;
	ConfigurationName: string;
}
interface IGetVersionRequest {
	VersionName: string;
	VersionKey: string;
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
interface IAppVersionNumber {
	Major: number;
	Minor: number;
	Patch: number;
}
interface IGetVersionsRequest {
	VersionName: string;
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
interface IRegisterAppRequest {
	VersionKey: IAppVersionKey;
	AppTemplate: IAppApiTemplateModel;
}
interface IAppApiTemplateModel {
	AppKey: IAppKey;
	SerializedDefaultOptions: string;
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
interface IAppRequestModel {
	ID: number;
	Path: string;
	ResourceID: number;
	ModifierID: number;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	ActualCount: number;
}
interface IGetPendingDeletesRequest {
	MachineNames: string[];
}
interface IAppVersionInstallationModel {
	App: IAppModel;
	Version: IXtiVersionModel;
	Installation: IInstallationModel;
}
interface IInstallationQueryRequest {
	QueryType: number;
}
interface IInstallationViewRequest {
	InstallationID: number;
}
interface IAppRequestRequest {
	RequestID: number;
}
interface IAppRequestQueryRequest {
	SessionID: number;
	InstallationID: number;
	SourceRequestID: number;
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
interface IResourceModel {
	ID: number;
	Name: IResourceName;
	IsAnonymousAllowed: boolean;
	ResultType: IResourceResultType;
}
interface IAppSessionModel {
	ID: number;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	SessionKey: string;
	RequesterKey: string;
	RemoteAddress: string;
	UserAgent: string;
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
	RequestData: string;
	ResultData: string;
}
interface IAppSessionDetailModel {
	Session: IAppSessionModel;
	UserGroup: IAppUserGroupModel;
	User: IAppUserModel;
}
interface ILogEntryQueryRequest {
	RequestID: number;
	InstallationID: number;
}
interface ILogEntryRequest {
	LogEntryID: number;
}
interface ISessionViewRequest {
	SessionID: number;
}
interface ILogBatchModel {
	StartSessions: IStartSessionModel[];
	StartRequests: IStartRequestModel[];
	LogEntries: ILogEntryModelV1[];
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
interface ILogEntryModelV1 {
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
interface ILogSessionDetailsRequest {
	SessionDetails: ITempLogSessionDetailModel[];
}
interface ITempLogSessionDetailModel {
	Session: ITempLogSessionModel;
	RequestDetails: ITempLogRequestDetailModel[];
}
interface ITempLogSessionModel {
	SessionKey: ISessionKey;
	RequesterKey: string;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	RemoteAddress: string;
	UserAgent: string;
}
interface ISessionKey {
	ID: string;
	UserName: string;
}
interface ITempLogRequestDetailModel {
	Request: ITempLogRequestModel;
	LogEntries: ILogEntryModel[];
}
interface ITempLogRequestModel {
	RequestKey: string;
	SourceRequestKey: string;
	Path: string;
	InstallationID: number;
	TimeStarted: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	TimeEnded: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	ActualCount: number;
	RequestData: string;
	ResultData: string;
}
interface ILogEntryModel {
	EventKey: string;
	Severity: number;
	TimeOccurred: import('@jasonbenfield/sharedwebapp/Common').DateTimeOffset;
	Caption: string;
	Message: string;
	Detail: string;
	ActualCount: number;
	ParentEventKey: string;
	Category: string;
}
interface IPublishVersionRequest {
	VersionName: string;
	VersionKey: string;
}
interface INewVersionRequest {
	VersionName: string;
	VersionType: number;
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
interface IGetResourceGroupRequest {
	VersionKey: string;
	GroupID: number;
}
interface IGetResourcesRequest {
	VersionKey: string;
	GroupID: number;
}
interface IGetResourceGroupRoleAccessRequest {
	VersionKey: string;
	GroupID: number;
}
interface IGetResourceLogRequest {
	VersionKey: string;
	ResourceID: number;
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
interface IGetStoredObjectRequest {
	StorageName: string;
	StorageKey: string;
}
interface IStoreObjectRequest {
	StorageName: string;
	Data: string;
	ExpireAfter: import('@jasonbenfield/sharedwebapp/Common').TimeSpan;
	GenerateKey: IGenerateKeyModel;
	IsSingleUse: boolean;
	IsSlidingExpiration: boolean;
}
interface IGenerateKeyModel {
	KeyType: IGeneratedKeyType;
	Value: string;
}
interface ISystemAddOrUpdateModifierByModKeyRequest {
	InstallationID: number;
	ModCategoryName: string;
	ModKey: string;
	TargetKey: string;
	TargetDisplayText: string;
}
interface ISystemAddOrUpdateModifierByTargetKeyRequest {
	InstallationID: number;
	ModCategoryName: string;
	GenerateModKey: IGenerateKeyModel;
	TargetKey: string;
	TargetDisplayText: string;
}
interface IGetAppContextRequest {
	InstallationID: number;
}
interface IAppContextModel {
	App: IAppModel;
	Version: IXtiVersionModel;
	Roles: IAppRoleModel[];
	ModCategories: IModifierCategoryModel[];
	ResourceGroups: IAppContextResourceGroupModel[];
	DefaultModifier: IModifierModel;
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
interface IGetModifierRequest {
	CategoryID: number;
	ModKey: string;
}
interface IAppUserIDRequest {
	UserID: number;
}
interface IUserAuthenticatorModel {
	Authenticator: IAuthenticatorModel;
	ExternalUserID: string;
}
interface IAppUserNameRequest {
	UserName: string;
}
interface IGetUserRolesRequest {
	UserID: number;
	ModifierID: number;
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
interface IAddUserGroupIfNotExistsRequest {
	GroupName: string;
}
interface IAppUserDetailModel {
	User: IAppUserModel;
	UserGroup: IAppUserGroupModel;
}
interface IUserGroupKey {
	UserGroupName: string;
}
interface IUserRoleIDRequest {
	UserRoleID: number;
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
interface IUserRoleQueryRequest {
	AppID: number;
}
interface IAddOrUpdateUserRequest {
	UserName: string;
	Password: string;
	PersonName: string;
	Email: string;
}
interface IUsersIndexRequest {
	UserID: number;
	ReturnTo: string;
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
	RequestData: string;
	ResultData: string;
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
	RequestData: string;
	ResultData: string;
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
interface ILinkModel {
	LinkName: string;
	DisplayText: string;
	Url: string;
	IsAuthenticationRequired: boolean;
}
interface IAppType {
	Value: number;
	DisplayText: string;
}
interface IAppEventSeverity {
	Value: number;
	DisplayText: string;
}
interface IResourceResultType {
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
interface IInstallStatus {
	Value: number;
	DisplayText: string;
}
interface IInstallationQueryType {
	Value: number;
	DisplayText: string;
}
interface IGeneratedKeyType {
	Value: number;
	DisplayText: string;
}