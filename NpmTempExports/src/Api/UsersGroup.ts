// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class UsersGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Users');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
		this.GetSystemUsersAction = this.createAction<IAppKey,IAppUserModel[]>('GetSystemUsers', 'Get System Users');
		this.AddOrUpdateUserAction = this.createAction<IAddUserModel,number>('AddOrUpdateUser', 'Add Or Update User');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetUsersAction: AppApiAction<IEmptyRequest,IAppUserModel[]>;
	readonly GetSystemUsersAction: AppApiAction<IAppKey,IAppUserModel[]>;
	readonly AddOrUpdateUserAction: AppApiAction<IAddUserModel,number>;
	
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
	GetSystemUsers(model: IAppKey, errorOptions?: IActionErrorOptions) {
		return this.GetSystemUsersAction.execute(model, errorOptions || {});
	}
	AddOrUpdateUser(model: IAddUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateUserAction.execute(model, errorOptions || {});
	}
}