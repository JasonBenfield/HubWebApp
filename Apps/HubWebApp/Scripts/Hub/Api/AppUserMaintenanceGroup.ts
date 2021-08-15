// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

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