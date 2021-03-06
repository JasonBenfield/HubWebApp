// Generated code

interface IUserStartRequest {
	ReturnUrl: string;
}
interface IEmptyRequest {
}
interface IAddUserModel {
	UserName: string;
	Password: string;
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
interface IRoleAccessModel {
	AllowedRoles: IAppRoleModel[];
	DeniedRoles: IAppRoleModel[];
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