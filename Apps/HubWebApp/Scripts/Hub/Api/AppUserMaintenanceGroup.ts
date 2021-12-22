// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class AppUserMaintenanceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUserMaintenance');
		this.AssignRoleAction = this.createAction<IUserRoleRequest,number>('AssignRole', 'Assign Role');
		this.UnassignRoleAction = this.createAction<IUserRoleRequest,IEmptyActionResult>('UnassignRole', 'Unassign Role');
	}
	
	readonly AssignRoleAction: AppApiAction<IUserRoleRequest,number>;
	readonly UnassignRoleAction: AppApiAction<IUserRoleRequest,IEmptyActionResult>;
	
	AssignRole(model: IUserRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.AssignRoleAction.execute(model, errorOptions || {});
	}
	UnassignRole(model: IUserRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.UnassignRoleAction.execute(model, errorOptions || {});
	}
}