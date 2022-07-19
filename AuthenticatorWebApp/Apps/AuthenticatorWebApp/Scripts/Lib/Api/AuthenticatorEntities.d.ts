// Generated code

interface IResourcePath {
	Group: string;
	Action: string;
	ModKey: string;
}
interface IResourcePathAccess {
	Path: IResourcePath;
	HasAccess: boolean;
}
interface IEmptyRequest {
}
interface ILogoutRequest {
	ReturnUrl: string;
}
interface IEmptyActionResult {
}