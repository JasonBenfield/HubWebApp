// Generated code

interface IUserStartRequest {
	ReturnUrl: string;
}
interface IEmptyRequest {
}
interface ILogoutRequest {
	ReturnUrl: string;
}
interface IEmptyActionResult {
}
interface ILoginModel {
	Credentials: ILoginCredentials;
	ReturnUrl: string;
}
interface ILoginCredentials {
	UserName: string;
	Password: string;
}
interface ILoginResult {
	Token: string;
}
interface IExternalLoginRequest {
	ExternalUserKey: string;
	ReturnUrl: string;
}
interface IRegisterUserAuthenticatorRequest {
	UserID: number;
	ExternalUserKey: string;
}
interface ILogBatchModel {
	StartSessions: IStartSessionModel[];
	StartRequests: IStartRequestModel[];
	LogEvents: ILogEventModel[];
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
	AppType: string;
	Path: string;
	TimeStarted: Date;
	ActualCount: number;
}
interface ILogEventModel {
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
interface IAppWithModKeyModel {
	App: IAppModel;
	ModKey: string;
}
interface IAppModel {
	ID: number;
	AppKey: IAppKey;
	VersionName: IAppVersionName;
	Title: string;
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
interface IGetAppByIDRequest {
	AppID: number;
}
interface IGetAppByAppKeyRequest {
	AppKey: IAppKey;
}
interface IAppDomainModel {
	AppKey: IAppKey;
	Domain: string;
}
interface IAppRoleModel {
	ID: number;
	Name: string;
}
interface IResourceGroupModel {
	ID: number;
	Name: string;
	IsAnonymousAllowed: boolean;
	ModCategoryID: number;
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
interface IAppEventModel {
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
	Name: string;
}
interface IModifierModel {
	ID: number;
	CategoryID: number;
	ModKey: string;
	TargetKey: string;
	DisplayText: string;
}
interface IRegisterAppRequest {
	VersionKey: IAppVersionKey;
	AppTemplate: IAppApiTemplateModel;
}
interface IAppVersionKey {
	Value: string;
	DisplayText: string;
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
	UserName: string;
	Name: string;
	Email: string;
}
interface IAddInstallationUserRequest {
	MachineName: string;
	Password: string;
}
interface INewInstallationRequest {
	VersionName: IAppVersionName;
	AppKey: IAppKey;
	QualifiedMachineName: string;
}
interface INewInstallationResult {
	CurrentInstallationID: number;
	VersionInstallationID: number;
}
interface IBeginInstallationRequest {
	QualifiedMachineName: string;
	AppKey: IAppKey;
	VersionKey: IAppVersionKey;
	Domain: string;
}
interface IInstalledRequest {
	InstallationID: number;
}
interface ISetUserAccessRequest {
	UserID: number;
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
interface IGetVersionResourceGroupRequest {
	VersionKey: string;
	GroupName: string;
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
	Name: string;
	IsAnonymousAllowed: boolean;
	ResultType: IResourceResultType;
}
interface IGetResourceGroupResourceRequest {
	VersionKey: string;
	GroupID: number;
	ResourceName: string;
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
interface IGetModCategoryModifierRequest {
	CategoryID: number;
	ModifierKey: string;
}
interface IAddUserModel {
	UserName: string;
	Password: string;
	PersonName: string;
	Email: string;
}
interface IRedirectToAppUserRequest {
	AppID: number;
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
interface IUserModifierCategoryModel {
	ModCategory: IModifierCategoryModel;
	Modifiers: IModifierModel[];
}
interface IUserRoleRequest {
	UserID: number;
	ModifierID: number;
	RoleID: number;
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