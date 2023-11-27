// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class UserGroupsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserGroups');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.UserQuery = this.createView<IUserGroupKey>('UserQuery');
		this.AddUserGroupIfNotExistsAction = this.createAction<IAddUserGroupIfNotExistsRequest,IAppUserGroupModel>('AddUserGroupIfNotExists', 'Add User Group If Not Exists');
		this.GetUserGroupsAction = this.createAction<IEmptyRequest,IAppUserGroupModel[]>('GetUserGroups', 'Get User Groups');
	}
	
	readonly Index: AppClientView<IEmptyRequest>;
	readonly UserQuery: AppClientView<IUserGroupKey>;
	readonly AddUserGroupIfNotExistsAction: AppClientAction<IAddUserGroupIfNotExistsRequest,IAppUserGroupModel>;
	readonly GetUserGroupsAction: AppClientAction<IEmptyRequest,IAppUserGroupModel[]>;
	
	AddUserGroupIfNotExists(model: IAddUserGroupIfNotExistsRequest, errorOptions?: IActionErrorOptions) {
		return this.AddUserGroupIfNotExistsAction.execute(model, errorOptions || {});
	}
	GetUserGroups(errorOptions?: IActionErrorOptions) {
		return this.GetUserGroupsAction.execute({}, errorOptions || {});
	}
}