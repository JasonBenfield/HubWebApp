// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppUserMaintenanceGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUserMaintenance');
		this.AssignRoleAction = this.createAction<IUserRoleRequest,number>('AssignRole', 'Assign Role');
		this.UnassignRoleAction = this.createAction<IUserRoleRequest,IEmptyActionResult>('UnassignRole', 'Unassign Role');
		this.DenyAccessAction = this.createAction<IUserModifierKey,IEmptyActionResult>('DenyAccess', 'Deny Access');
		this.AllowAccessAction = this.createAction<IUserModifierKey,IEmptyActionResult>('AllowAccess', 'Allow Access');
	}
	
	readonly AssignRoleAction: AppClientAction<IUserRoleRequest,number>;
	readonly UnassignRoleAction: AppClientAction<IUserRoleRequest,IEmptyActionResult>;
	readonly DenyAccessAction: AppClientAction<IUserModifierKey,IEmptyActionResult>;
	readonly AllowAccessAction: AppClientAction<IUserModifierKey,IEmptyActionResult>;
	
	AssignRole(model: IUserRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.AssignRoleAction.execute(model, errorOptions || {});
	}
	UnassignRole(model: IUserRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.UnassignRoleAction.execute(model, errorOptions || {});
	}
	DenyAccess(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.DenyAccessAction.execute(model, errorOptions || {});
	}
	AllowAccess(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.AllowAccessAction.execute(model, errorOptions || {});
	}
}