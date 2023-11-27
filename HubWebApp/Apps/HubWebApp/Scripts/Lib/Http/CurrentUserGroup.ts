// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";
import { ChangeCurrentUserPasswordForm } from "./ChangeCurrentUserPasswordForm";
import { EditCurrentUserForm } from "./EditCurrentUserForm";

export class CurrentUserGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'CurrentUser');
		this.ChangePasswordAction = this.createAction<ChangeCurrentUserPasswordForm,IEmptyActionResult>('ChangePassword', 'Change Password');
		this.EditUserAction = this.createAction<EditCurrentUserForm,IAppUserModel>('EditUser', 'Edit User');
		this.GetUserAction = this.createAction<IEmptyRequest,IAppUserModel>('GetUser', 'Get User');
		this.Index = this.createView<IEmptyRequest>('Index');
	}
	
	readonly ChangePasswordAction: AppClientAction<ChangeCurrentUserPasswordForm,IEmptyActionResult>;
	readonly EditUserAction: AppClientAction<EditCurrentUserForm,IAppUserModel>;
	readonly GetUserAction: AppClientAction<IEmptyRequest,IAppUserModel>;
	readonly Index: AppClientView<IEmptyRequest>;
	
	ChangePassword(model: ChangeCurrentUserPasswordForm, errorOptions?: IActionErrorOptions) {
		return this.ChangePasswordAction.execute(model, errorOptions || {});
	}
	EditUser(model: EditCurrentUserForm, errorOptions?: IActionErrorOptions) {
		return this.EditUserAction.execute(model, errorOptions || {});
	}
	GetUser(errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute({}, errorOptions || {});
	}
}