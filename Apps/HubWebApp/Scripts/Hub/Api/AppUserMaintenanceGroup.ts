// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class AppUserMaintenanceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUserMaintenance');
		this.AssignRoleAction = this.createAction<IAssignRoleRequest,number>('AssignRole', 'Assign Role');
		this.UnassignRoleAction = this.createAction<number,IEmptyActionResult>('UnassignRole', 'Unassign Role');
	}
	
	readonly AssignRoleAction: AppApiAction<IAssignRoleRequest,number>;
	readonly UnassignRoleAction: AppApiAction<number,IEmptyActionResult>;
	
	AssignRole(model: IAssignRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.AssignRoleAction.execute(model, errorOptions || {});
	}
	UnassignRole(model: number, errorOptions?: IActionErrorOptions) {
		return this.UnassignRoleAction.execute(model, errorOptions || {});
	}
}