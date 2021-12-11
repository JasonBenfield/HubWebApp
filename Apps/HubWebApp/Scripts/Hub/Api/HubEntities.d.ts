// Generated code

interface IUserStartRequest {
	ReturnUrl: string;
}
interface IEmptyActionResult {
}
interface IEmptyRequest {
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
interface IAppModel {
	ID: number;
	Type: IAppType;
	AppName: string;
	Title: string;
}
interface IAppKey {
	Name: string;
	Type: IAppType;
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
	Versions: IAppVersionModel[];
	VersionKey: string;
	AppTemplate: IAppApiTemplateModel;
}
interface IAppVersionModel {
	ID: number;
	VersionKey: string;
	Major: number;
	Minor: number;
	Patch: number;
	VersionType: IAppVersionType;
	Status: IAppVersionStatus;
	TimeAdded: Date;
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
	AppKey: IAppKey;
	VersionKey: string;
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
interface INewInstallationRequest {
	VersionID: number;
	QualifiedMachineName: string;
}
interface INewInstallationResult {
	CurrentInstallationID: number;
	VersionInstallationID: number;
}
interface IBeginInstallationRequest {
	QualifiedMachineName: string;
	AppKey: IAppKey;
	VersionKey: string;
}
interface IInstalledRequest {
	InstallationID: number;
}
interface INewVersionRequest {
	AppKey: IAppKey;
	VersionType: IAppVersionType;
}
interface IPublishVersionRequest {
	AppKey: IAppKey;
	VersionKey: string;
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
}
interface IRedirectToAppUserRequest {
	AppID: number;
	UserID: number;
}
interface IGetUserRolesRequest {
	UserID: number;
	ModifierID: number;
}
interface IGetUserRoleAccessRequest {
	UserID: number;
	ModifierID: number;
}
interface IUserRoleAccessModel {
	UnassignedRoles: IAppRoleModel[];
	AssignedRoles: IAppRoleModel[];
}
interface IUserModifierCategoryModel {
	ModCategory: IModifierCategoryModel;
	Modifiers: IModifierModel[];
}
interface IUserRoleRequest {
	UserID: number;
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