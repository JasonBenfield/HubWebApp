// Generated code

interface ILogoutRequest {
	ReturnUrl: string;
}
interface IEmptyActionResult {
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
	AppKey: IAppKey;
	ExternalUserKey: string;
}
interface IAppKey {
	Name: IAppName;
	Type: IAppType;
}
interface IAppName {
	Value: string;
	DisplayText: string;
}
interface IRegisterUserAuthenticatorRequest {
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
	UserName: IAppUserName;
	RoleAssignments: ISetUserAccessRoleRequest[];
}
interface ISetUserAccessRoleRequest {
	AppKey: IAppKey;
	RoleNames: IAppRoleName[];
}
interface IAppRoleName {
	Value: string;
	DisplayText: string;
}
interface INewVersionRequest {
	VersionName: IAppVersionName;
	VersionType: IAppVersionType;
	AppKeys: IAppKey[];
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
interface IAddUserModel {
	UserName: string;
	Password: string;
	PersonName: string;
	Email: string;
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
interface IUserGroupKey {
	UserGroupName: string;
}
interface IAddUserGroupIfNotExistsRequest {
	GroupName: string;
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
interface IExpandedUser {
	UserID: number;
	UserName: string;
	PersonName: string;
	Email: string;
	TimeUserAdded: Date;
	UserGroupID: number;
	UserGroupName: string;
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