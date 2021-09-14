// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";
import { EditUserForm } from "./EditUserForm";

export class UserMaintenanceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserMaintenance');
		this.EditUserAction = this.createAction<EditUserForm,IEmptyActionResult>('EditUser', 'Edit User');
		this.GetUserForEditAction = this.createAction<number,Record<string,object>>('GetUserForEdit', 'Get User For Edit');
	}
	
	readonly EditUserAction: AppApiAction<EditUserForm,IEmptyActionResult>;
	readonly GetUserForEditAction: AppApiAction<number,Record<string,object>>;
	
	EditUser(model: EditUserForm, errorOptions?: IActionErrorOptions) {
		return this.EditUserAction.execute(model, errorOptions || {});
	}
	GetUserForEdit(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserForEditAction.execute(model, errorOptions || {});
	}
}