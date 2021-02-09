// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class UsersGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Users');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
		this.AddUserAction = this.createAction<IAddUserModel,number>('AddUser', 'Add User');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetUsersAction: AppApiAction<IEmptyRequest,IAppUserModel[]>;
	readonly AddUserAction: AppApiAction<IAddUserModel,number>;
	
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
	AddUser(model: IAddUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddUserAction.execute(model, errorOptions || {});
	}
}