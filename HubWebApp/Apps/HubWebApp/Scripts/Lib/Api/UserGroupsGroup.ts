// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class UserGroupsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserGroups');
		this.Index = this.createView<IUserGroupKey>('Index');
		this.AddUserGroupIfNotExistsAction = this.createAction<IAddUserGroupIfNotExistsRequest,IAppUserGroupModel>('AddUserGroupIfNotExists', 'Add User Group If Not Exists');
		this.GetUserGroupsAction = this.createAction<IEmptyRequest,IAppUserGroupModel[]>('GetUserGroups', 'Get User Groups');
	}
	
	readonly Index: AppApiView<IUserGroupKey>;
	readonly AddUserGroupIfNotExistsAction: AppApiAction<IAddUserGroupIfNotExistsRequest,IAppUserGroupModel>;
	readonly GetUserGroupsAction: AppApiAction<IEmptyRequest,IAppUserGroupModel[]>;
	
	AddUserGroupIfNotExists(model: IAddUserGroupIfNotExistsRequest, errorOptions?: IActionErrorOptions) {
		return this.AddUserGroupIfNotExistsAction.execute(model, errorOptions || {});
	}
	GetUserGroups(errorOptions?: IActionErrorOptions) {
		return this.GetUserGroupsAction.execute({}, errorOptions || {});
	}
}