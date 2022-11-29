// Generated code

interface ILinkModel {
	LinkName: string;
	DisplayText: string;
	Url: string;
}
interface IAppUserModel {
	ID: number;
	UserName: IAppUserName;
	Name: IPersonName;
	Email: string;
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
interface IExpandedInstallation {
	InstallationID: number;
	IsCurrent: boolean;
	InstallationStatusDisplayText: string;
	TimeInstallationAdded: Date;
	QualifiedMachineName: string;
	Domain: string;
	AppID: number;
	AppKey: string;
	AppName: string;
	AppTypeText: string;
	VersionName: string;
	VersionRelease: string;
	VersionKey: string;
	VersionStatusText: string;
	VersionTypeText: string;
	LastRequestTime: Date;
	LastRequestDaysAgo: number;
	RequestCount: number;
}
interface IWebFileResult {
	FileStream: IStream;
	ContentType: string;
	DownloadName: string;
}
interface IStream {
	CanRead: boolean;
	CanWrite: boolean;
	CanSeek: boolean;
	CanTimeout: boolean;
	Length: number;
	Position: number;
	ReadTimeout: number;
	WriteTimeout: number;
}
interface ILoginModel {
	AuthKey: string;
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
	TimeStarted: Date;
	RemoteAddress: string;
	UserAgent: string;
}
interface IStartRequestModel {
	RequestKey: string;
	SessionKey: string;
	Path: string;
	InstallationID: number;
	TimeStarted: Date;
	ActualCount: number;
}
interface ILogEntryModel {
	EventKey: string;
	RequestKey: string;
	Severity: number;
	TimeOccurred: Date;
	Caption: string;
	Message: string;
	Detail: string;
	ActualCount: number;
}
interface IEndRequestModel {
	RequestKey: string;
	TimeEnded: Date;
}
interface IAuthenticateSessionModel {
	SessionKey: string;
	UserName: string;
}
interface IEndSessionModel {
	SessionKey: string;
	TimeEnded: Date;
}
interface IAppModel {
	ID: number;
	AppKey: IAppKey;
	VersionName: IAppVersionName;
	Title: string;
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
interface IAppDomainModel {
	AppKey: IAppKey;
	VersionKey: IAppVersionKey;
	Domain: string;
}
interface IAppVersionKey {
	Value: string;
	DisplayText: string;
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
interface IAppRequestExpandedModel {
	ID: number;
	UserName: string;
	GroupName: string;
	ActionName: string;
	ResultType: IResourceResultType;
	TimeStarted: Date;
	TimeEnded: Date;
}
interface IAppLogEntryModel {
	ID: number;
	RequestID: number;
	TimeOccurred: Date;
	Severity: IAppEventSeverity;
	Caption: string;
	Message: string;
	Detail: string;
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
	Name: string;
	ModCategory: string;
	IsAnonymousAllowed: boolean;
	Roles: string[];
	ActionTemplates: IAppApiActionTemplateModel[];
}
interface IAppApiActionTemplateModel {
	Name: string;
	IsAnonymousAllowed: boolean;
	Roles: string[];
	ResultType: IResourceResultType;
}
interface IAddOrUpdateAppsRequest {
	VersionName: IAppVersionName;
	Apps: IAppDefinitionModel[];
}
interface IAppDefinitionModel {
	AppKey: IAppKey;
}
interface IAddOrUpdateVersionsRequest {
	Apps: IAppKey[];
	Versions: IXtiVersionModel[];
}
interface IXtiVersionModel {
	ID: number;
	VersionName: IAppVersionName;
	VersionKey: IAppVersionKey;
	VersionNumber: IAppVersionNumber;
	VersionType: IAppVersionType;
	Status: IAppVersionStatus;
	TimeAdded: Date;
}
interface IAppVersionNumber {
	Major: number;
	Minor: number;
	Patch: number;
}
interface IGetVersionRequest {
	VersionName: IAppVersionName;
	VersionKey: IAppVersionKey;
}
interface IGetVersionsRequest {
	VersionName: IAppVersionName;
}
interface IAddSystemUserRequest {
	AppKey: IAppKey;
	MachineName: string;
	Password: string;
}
interface IAddAdminUserRequest {
	AppKey: IAppKey;
	UserName: string;
	Password: string;
}
interface IAddInstallationUserRequest {
	MachineName: string;
	Password: string;
}
interface INewInstallationRequest {
	VersionName: IAppVersionName;
	AppKey: IAppKey;
	QualifiedMachineName: string;
	Domain: string;
}
interface INewInstallationResult {
	CurrentInstallationID: number;
	VersionInstallationID: number;
}
interface IInstallationRequest {
	InstallationID: number;
}
interface ISetUserAccessRequest {
	UserName: string;
	RoleAssignments: ISetUserAccessRoleRequest[];
}
interface ISetUserAccessRoleRequest {
	AppKey: IAppKey;
	ModCategoryName: string;
	ModKey: string;
	RoleNames: string[];
}
interface INewVersionRequest {
	VersionName: IAppVersionName;
	VersionType: IAppVersionType;
}
interface IPublishVersionRequest {
	VersionName: IAppVersionName;
	VersionKey: IAppVersionKey;
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
interface IResourceName {
	Value: string;
	DisplayText: string;
}
interface IGetResourceGroupRoleAccessRequest {
	VersionKey: string;
	GroupID: number;
}
interface IAppRoleModel {
	ID: number;
	Name: IAppRoleName;
}
interface IAppRoleName {
	Value: string;
	DisplayText: string;
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
interface IGetUserRequest {
	UserID: number;
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
	ExpireAfter: string;
	GenerateKey: IGenerateKeyModel;
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
	VersionKey: string;
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
	VersionKey: string;
	UserName: string;
}
interface IUserContextModel {
	User: IAppUserModel;
	ModifiedRoles: IUserContextRoleModel[];
}
interface IUserContextRoleModel {
	ModifierCategoryID: number;
	ModifierKey: IModifierKey;
	Roles: IAppRoleModel[];
}
interface IAddOrUpdateModifierByTargetKeyRequest {
	ModCategoryName: string;
	GenerateModKey: IGenerateKeyModel;
	TargetKey: string;
	TargetDisplayText: string;
}
interface IUserGroupKey {
	UserGroupName: string;
}
interface IAddUserGroupIfNotExistsRequest {
	GroupName: string;
}
interface IExpandedUser {
	UserID: number;
	UserName: string;
	PersonName: string;
	Email: string;
	TimeUserAdded: Date;
	UserGroupID: number;
	UserGroupName: string;
}
interface IRequestQueryRequest {
	SessionID: number;
}
interface ILogEntryQueryRequest {
	RequestID: number;
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
	TimeStarted: Date;
	TimeEnded: Date;
	TimeElapsed: string;
	LastRequestTime: Date;
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
	RequestTimeStarted: Date;
	RequestTimeEnded: Date;
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
	VersionStatusText: string;
	VersionTypeText: string;
	InstallLocation: string;
	IsCurrentInstallation: boolean;
}
interface IExpandedLogEntry {
	EventID: number;
	TimeOccurred: Date;
	SeverityText: string;
	Caption: string;
	Message: string;
	Detail: string;
	Path: string;
	ActualCount: number;
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
	UserName: string;
	UserGroupID: number;
	UserGroupName: string;
	UserGroupDisplayText: string;
	RequestID: number;
	RequestTimeStarted: Date;
	RequestTimeEnded: Date;
	RequestTimeElapsed: string;
	VersionName: string;
	VersionKey: string;
	VersionRelease: string;
	VersionStatusText: string;
	VersionTypeText: string;
	InstallLocation: string;
	IsCurrentInstallation: boolean;
}
interface IInstallationQueryType {
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
interface IAppVersionType {
	Value: number;
	DisplayText: string;
}
interface IAppVersionStatus {
	Value: number;
	DisplayText: string;
}
interface IGeneratedKeyType {
	Value: number;
	DisplayText: string;
}