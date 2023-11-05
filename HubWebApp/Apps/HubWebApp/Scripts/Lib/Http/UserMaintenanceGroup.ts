// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";
import { EditUserForm } from "./EditUserForm";
import { ChangePasswordForm } from "./ChangePasswordForm";

export class UserMaintenanceGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserMaintenance');
		this.DeactivateUserAction = this.createAction<number,IAppUserModel>('DeactivateUser', 'Deactivate User');
		this.ReactivateUserAction = this.createAction<number,IAppUserModel>('ReactivateUser', 'Reactivate User');
		this.EditUserAction = this.createAction<EditUserForm,IEmptyActionResult>('EditUser', 'Edit User');
		this.ChangePasswordAction = this.createAction<ChangePasswordForm,IEmptyActionResult>('ChangePassword', 'Change Password');
		this.GetUserForEditAction = this.createAction<number,Record<string,object>>('GetUserForEdit', 'Get User For Edit');
	}
	
	readonly DeactivateUserAction: AppClientAction<number,IAppUserModel>;
	readonly ReactivateUserAction: AppClientAction<number,IAppUserModel>;
	readonly EditUserAction: AppClientAction<EditUserForm,IEmptyActionResult>;
	readonly ChangePasswordAction: AppClientAction<ChangePasswordForm,IEmptyActionResult>;
	readonly GetUserForEditAction: AppClientAction<number,Record<string,object>>;
	
	DeactivateUser(model: number, errorOptions?: IActionErrorOptions) {
		return this.DeactivateUserAction.execute(model, errorOptions || {});
	}
	ReactivateUser(model: number, errorOptions?: IActionErrorOptions) {
		return this.ReactivateUserAction.execute(model, errorOptions || {});
	}
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