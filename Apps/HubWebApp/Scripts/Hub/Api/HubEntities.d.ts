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
	AppKey: string;
	Title: string;
}
interface IAppType {
	Value: number;
	DisplayText: string;
}