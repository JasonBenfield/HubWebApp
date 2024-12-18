// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";
import { ChangePasswordForm } from "./ChangePasswordForm";
import { EditUserForm } from "./EditUserForm";

export class UserMaintenanceGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserMaintenance');
		this.ChangePasswordAction = this.createAction<ChangePasswordForm,IEmptyActionResult>('ChangePassword', 'Change Password');
		this.DeactivateUserAction = this.createAction<number,IAppUserModel>('DeactivateUser', 'Deactivate User');
		this.EditUserAction = this.createAction<EditUserForm,IEmptyActionResult>('EditUser', 'Edit User');
		this.GetUserForEditAction = this.createAction<number,Record<string,object>>('GetUserForEdit', 'Get User For Edit');
		this.ReactivateUserAction = this.createAction<number,IAppUserModel>('ReactivateUser', 'Reactivate User');
	}
	
	readonly ChangePasswordAction: AppClientAction<ChangePasswordForm,IEmptyActionResult>;
	readonly DeactivateUserAction: AppClientAction<number,IAppUserModel>;
	readonly EditUserAction: AppClientAction<EditUserForm,IEmptyActionResult>;
	readonly GetUserForEditAction: AppClientAction<number,Record<string,object>>;
	readonly ReactivateUserAction: AppClientAction<number,IAppUserModel>;
	
	ChangePassword(requestData: ChangePasswordForm, errorOptions?: IActionErrorOptions) {
		return this.ChangePasswordAction.execute(requestData, errorOptions || {});
	}
	DeactivateUser(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.DeactivateUserAction.execute(requestData, errorOptions || {});
	}
	EditUser(requestData: EditUserForm, errorOptions?: IActionErrorOptions) {
		return this.EditUserAction.execute(requestData, errorOptions || {});
	}
	GetUserForEdit(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserForEditAction.execute(requestData, errorOptions || {});
	}
	ReactivateUser(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.ReactivateUserAction.execute(requestData, errorOptions || {});
	}
}