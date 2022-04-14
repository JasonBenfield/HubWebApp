// Generated code

interface IUserStartRequest {
	ReturnUrl: string;
}
interface IEmptyRequest {
}
interface IEmptyActionResult {
}
interface ILoginModel {
	Credentials: ILoginCredentials;
	StartUrl: string;
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
	StartUrl: string;
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
interface IGetAppDomainRequest {
	AppName: string;
	Version: string;
}
interface IAppWithModKeyModel {
	App: IAppModel;
	ModKey: string;
}
interface IAppModel {
	ID: number;
	Type: IAppType;
	AppName: string;
	Title: string;
	Domain: string;
}
interface IGetAppByIDRequest {
	AppID: number;
}
interface IGetAppByAppKeyRequest {
	AppKey: IAppKey;
}
interface IAppKey {
	Name: IAppName;
	Type: IAppType;
}
interface IAppName {
	Value: string;
	DisplayText: string;
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
	Versions: IXtiVersionModel[];
	Domain: string;
	VersionKey: IAppVersionKey;
	AppTemplate: IAppApiTemplateModel;
}
interface IXtiVersionModel {
	ID: number;
	GroupName: string;
	VersionKey: IAppVersionKey;
	VersionNumber: IAppVersionNumber;
	VersionType: IAppVersionType;
	Status: IAppVersionStatus;
	TimeAdded: Date;
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
interface IGetVersionRequest {
	GroupName: string;
	VersionKey: IAppVersionKey;
}
interface IAddSystemUserRequest {
	AppKey: IAppKey;
	Domain: string;
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
}
interface IInstalledRequest {
	InstallationID: number;
}
interface INewVersionRequest {
	GroupName: string;
	VersionType: IAppVersionType;
	AppDefinitions: IAppDefinitionModel[];
}
interface IAppDefinitionModel {
	AppKey: IAppKey;
	Domain: string;
}
interface IPublishVersionRequest {
	GroupName: string;
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