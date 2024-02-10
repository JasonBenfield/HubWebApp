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
		this.Index = this.createView<IUserRoleQueryRequest>('Index');
		this.GetUserRoleDetailAction = this.createAction<IUserRoleIDRequest,IUserRoleDetailModel>('GetUserRoleDetail', 'Get User Role Detail');
		this.UserRole = this.createView<IUserRoleIDRequest>('UserRole');
	}
	
	readonly DeleteUserRoleAction: AppClientAction<IUserRoleIDRequest,IEmptyActionResult>;
	readonly Index: AppClientView<IUserRoleQueryRequest>;
	readonly GetUserRoleDetailAction: AppClientAction<IUserRoleIDRequest,IUserRoleDetailModel>;
	readonly UserRole: AppClientView<IUserRoleIDRequest>;
	
	DeleteUserRole(model: IUserRoleIDRequest, errorOptions?: IActionErrorOptions) {
		return this.DeleteUserRoleAction.execute(model, errorOptions || {});
	}
	GetUserRoleDetail(model: IUserRoleIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRoleDetailAction.execute(model, errorOptions || {});
	}
}