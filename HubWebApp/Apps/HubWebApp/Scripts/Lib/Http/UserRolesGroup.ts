// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class UserRolesGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserRoles');
		this.DeleteUserRoleAction = this.createAction<IUserRoleIDRequest,IEmptyActionResult>('DeleteUserRole', 'Delete User Role');
		this.GetUserRoleDetailAction = this.createAction<IUserRoleIDRequest,IUserRoleDetailModel>('GetUserRoleDetail', 'Get User Role Detail');
		this.Index = this.createView<IUserRoleQueryRequest>('Index');
		this.UserRole = this.createView<IUserRoleIDRequest>('UserRole');
	}
	
	readonly DeleteUserRoleAction: AppClientAction<IUserRoleIDRequest,IEmptyActionResult>;
	readonly GetUserRoleDetailAction: AppClientAction<IUserRoleIDRequest,IUserRoleDetailModel>;
	readonly Index: AppClientView<IUserRoleQueryRequest>;
	readonly UserRole: AppClientView<IUserRoleIDRequest>;
	
	DeleteUserRole(requestData: IUserRoleIDRequest, errorOptions?: IActionErrorOptions) {
		return this.DeleteUserRoleAction.execute(requestData, errorOptions || {});
	}
	GetUserRoleDetail(requestData: IUserRoleIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRoleDetailAction.execute(requestData, errorOptions || {});
	}
}