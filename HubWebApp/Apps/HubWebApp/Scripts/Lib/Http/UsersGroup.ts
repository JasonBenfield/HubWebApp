// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";
import { AddUserForm } from "./AddUserForm";

export class UsersGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Users');
		this.Index = this.createView<IUsersIndexRequest>('Index');
		this.GetUserGroupAction = this.createAction<IEmptyRequest,IAppUserGroupModel>('GetUserGroup', 'Get User Group');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
		this.AddOrUpdateUserAction = this.createAction<IAddOrUpdateUserRequest,IAppUserModel>('AddOrUpdateUser', 'Add Or Update User');
		this.AddUserAction = this.createAction<AddUserForm,IAppUserModel>('AddUser', 'Add User');
	}
	
	readonly Index: AppClientView<IUsersIndexRequest>;
	readonly GetUserGroupAction: AppClientAction<IEmptyRequest,IAppUserGroupModel>;
	readonly GetUsersAction: AppClientAction<IEmptyRequest,IAppUserModel[]>;
	readonly AddOrUpdateUserAction: AppClientAction<IAddOrUpdateUserRequest,IAppUserModel>;
	readonly AddUserAction: AppClientAction<AddUserForm,IAppUserModel>;
	
	GetUserGroup(errorOptions?: IActionErrorOptions) {
		return this.GetUserGroupAction.execute({}, errorOptions || {});
	}
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
	AddOrUpdateUser(model: IAddOrUpdateUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateUserAction.execute(model, errorOptions || {});
	}
	AddUser(model: AddUserForm, errorOptions?: IActionErrorOptions) {
		return this.AddUserAction.execute(model, errorOptions || {});
	}
}