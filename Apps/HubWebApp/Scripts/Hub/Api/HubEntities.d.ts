// Generated code

interface IUserStartRequest {
	ReturnUrl: string;
}
interface IClearUserCacheRequest {
	UserID: number;
	UserName: string;
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
}
interface ILogEventModel {
	EventKey: string;
	RequestKey: string;
	Severity: number;
	TimeOccurred: Date;
	Caption: string;
	Message: string;
	Detail: string;
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
interface IResourceModel {
	ID: number;
	Name: string;
	IsAnonymousAllowed: boolean;
	ResultType: IResourceResultType;
}
interface IAppRoleModel {
	ID: number;
	Name: string;
}
interface IGetResourceGroupLogRequest {
	GroupID: number;
	HowMany: number;
}
interface IGetResourceLogRequest {
	ResourceID: number;
	HowMany: number;
}
interface IModifierModel {
	ID: number;
	CategoryID: number;
	ModKey: string;
	TargetKey: string;
	DisplayText: string;
}
interface IAppUserModel {
	ID: number;
	UserName: string;
	Name: string;
	Email: string;
}
interface IAddUserModel {
	UserName: string;
	Password: string;
}
interface IRedirectToAppUserRequest {
	AppID: number;
	UserID: number;
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
interface IAppVersionType {
	Value: number;
	DisplayText: string;
}
interface IAppVersionStatus {
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