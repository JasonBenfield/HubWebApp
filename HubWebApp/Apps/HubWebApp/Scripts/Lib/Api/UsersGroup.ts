// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";
import { AddUserForm } from "./AddUserForm";

export class UsersGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Users');
		this.Index = this.createView<IGetUserRequest>('Index');
		this.GetUserGroupAction = this.createAction<IEmptyRequest,IAppUserGroupModel>('GetUserGroup', 'Get User Group');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
		this.AddOrUpdateUserAction = this.createAction<IAddOrUpdateUserModel,number>('AddOrUpdateUser', 'Add Or Update User');
		this.AddUserAction = this.createAction<AddUserForm,IAppUserModel>('AddUser', 'Add User');
	}
	
	readonly Index: AppApiView<IGetUserRequest>;
	readonly GetUserGroupAction: AppApiAction<IEmptyRequest,IAppUserGroupModel>;
	readonly GetUsersAction: AppApiAction<IEmptyRequest,IAppUserModel[]>;
	readonly AddOrUpdateUserAction: AppApiAction<IAddOrUpdateUserModel,number>;
	readonly AddUserAction: AppApiAction<AddUserForm,IAppUserModel>;
	
	GetUserGroup(errorOptions?: IActionErrorOptions) {
		return this.GetUserGroupAction.execute({}, errorOptions || {});
	}
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
	AddOrUpdateUser(model: IAddOrUpdateUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateUserAction.execute(model, errorOptions || {});
	}
	AddUser(model: AddUserForm, errorOptions?: IActionErrorOptions) {
		return this.AddUserAction.execute(model, errorOptions || {});
	}
}