// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class UsersGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Users');
		this.Index = this.createView<IGetUserRequest>('Index');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
		this.AddOrUpdateUserAction = this.createAction<IAddUserModel,number>('AddOrUpdateUser', 'Add Or Update User');
	}
	
	readonly Index: AppApiView<IGetUserRequest>;
	readonly GetUsersAction: AppApiAction<IEmptyRequest,IAppUserModel[]>;
	readonly AddOrUpdateUserAction: AppApiAction<IAddUserModel,number>;
	
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
	AddOrUpdateUser(model: IAddUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateUserAction.execute(model, errorOptions || {});
	}
}