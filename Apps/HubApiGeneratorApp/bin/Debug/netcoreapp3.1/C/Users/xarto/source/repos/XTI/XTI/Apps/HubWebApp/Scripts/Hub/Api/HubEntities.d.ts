// Generated code

interface IEmptyRequest {
}
interface IAppActionViewResult {
	ViewName: string;
}
interface IStartRequest {
	StartUrl: string;
	ReturnUrl: string;
}
interface IAppActionRedirectResult {
	Url: string;
}
interface ILoginModel {
	UserName: string;
	Password: string;
}
interface ILoginResult {
	Token: string;
}
interface IAddUserModel {
	UserName: string;
	Password: string;
}