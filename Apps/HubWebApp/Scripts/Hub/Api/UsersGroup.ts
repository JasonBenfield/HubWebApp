// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class UsersGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Users');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
		this.GetSystemUsersAction = this.createAction<IAppKey,IAppUserModel[]>('GetSystemUsers', 'Get System Users');
		this.AddUserAction = this.createAction<IAddUserModel,number>('AddUser', 'Add User');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetUsersAction: AppApiAction<IEmptyRequest,IAppUserModel[]>;
	readonly GetSystemUsersAction: AppApiAction<IAppKey,IAppUserModel[]>;
	readonly AddUserAction: AppApiAction<IAddUserModel,number>;
	
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
	GetSystemUsers(model: IAppKey, errorOptions?: IActionErrorOptions) {
		return this.GetSystemUsersAction.execute(model, errorOptions || {});
	}
	AddUser(model: IAddUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddUserAction.execute(model, errorOptions || {});
	}
}