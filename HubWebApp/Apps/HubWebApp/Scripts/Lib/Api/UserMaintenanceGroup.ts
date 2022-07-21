// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";
import { EditUserForm } from "./EditUserForm";
import { ChangePasswordForm } from "./ChangePasswordForm";

export class UserMaintenanceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserMaintenance');
		this.EditUserAction = this.createAction<EditUserForm,IEmptyActionResult>('EditUser', 'Edit User');
		this.ChangePasswordAction = this.createAction<ChangePasswordForm,IEmptyActionResult>('ChangePassword', 'Change Password');
		this.GetUserForEditAction = this.createAction<number,Record<string,object>>('GetUserForEdit', 'Get User For Edit');
	}
	
	readonly EditUserAction: AppApiAction<EditUserForm,IEmptyActionResult>;
	readonly ChangePasswordAction: AppApiAction<ChangePasswordForm,IEmptyActionResult>;
	readonly GetUserForEditAction: AppApiAction<number,Record<string,object>>;
	
	EditUser(model: EditUserForm, errorOptions?: IActionErrorOptions) {
		return this.EditUserAction.execute(model, errorOptions || {});
	}
	ChangePassword(model: ChangePasswordForm, errorOptions?: IActionErrorOptions) {
		return this.ChangePasswordAction.execute(model, errorOptions || {});
	}
	GetUserForEdit(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserForEditAction.execute(model, errorOptions || {});
	}
}