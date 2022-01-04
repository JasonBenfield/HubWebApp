// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AppUserMaintenanceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUserMaintenance');
		this.AssignRoleAction = this.createAction<IUserRoleRequest,number>('AssignRole', 'Assign Role');
		this.UnassignRoleAction = this.createAction<IUserRoleRequest,IEmptyActionResult>('UnassignRole', 'Unassign Role');
		this.DenyAccessAction = this.createAction<IUserModifierKey,IEmptyActionResult>('DenyAccess', 'Deny Access');
		this.AllowAccessAction = this.createAction<IUserModifierKey,IEmptyActionResult>('AllowAccess', 'Allow Access');
	}
	
	readonly AssignRoleAction: AppApiAction<IUserRoleRequest,number>;
	readonly UnassignRoleAction: AppApiAction<IUserRoleRequest,IEmptyActionResult>;
	readonly DenyAccessAction: AppApiAction<IUserModifierKey,IEmptyActionResult>;
	readonly AllowAccessAction: AppApiAction<IUserModifierKey,IEmptyActionResult>;
	
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