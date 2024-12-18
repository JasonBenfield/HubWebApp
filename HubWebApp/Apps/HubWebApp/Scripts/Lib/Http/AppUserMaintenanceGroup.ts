// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppUserMaintenanceGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUserMaintenance');
		this.AllowAccessAction = this.createAction<IUserModifierKey,IEmptyActionResult>('AllowAccess', 'Allow Access');
		this.AssignRoleAction = this.createAction<IUserRoleRequest,number>('AssignRole', 'Assign Role');
		this.DenyAccessAction = this.createAction<IUserModifierKey,IEmptyActionResult>('DenyAccess', 'Deny Access');
		this.UnassignRoleAction = this.createAction<IUserRoleRequest,IEmptyActionResult>('UnassignRole', 'Unassign Role');
	}
	
	readonly AllowAccessAction: AppClientAction<IUserModifierKey,IEmptyActionResult>;
	readonly AssignRoleAction: AppClientAction<IUserRoleRequest,number>;
	readonly DenyAccessAction: AppClientAction<IUserModifierKey,IEmptyActionResult>;
	readonly UnassignRoleAction: AppClientAction<IUserRoleRequest,IEmptyActionResult>;
	
	AllowAccess(requestData: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.AllowAccessAction.execute(requestData, errorOptions || {});
	}
	AssignRole(requestData: IUserRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.AssignRoleAction.execute(requestData, errorOptions || {});
	}
	DenyAccess(requestData: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.DenyAccessAction.execute(requestData, errorOptions || {});
	}
	UnassignRole(requestData: IUserRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.UnassignRoleAction.execute(requestData, errorOptions || {});
	}
}