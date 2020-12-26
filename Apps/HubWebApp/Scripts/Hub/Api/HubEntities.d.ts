// Generated code

interface IUserStartRequest {
	ReturnUrl: string;
}
interface IAppActionViewResult {
	ViewName: string;
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
interface IAppActionRedirectResult {
	Url: string;
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