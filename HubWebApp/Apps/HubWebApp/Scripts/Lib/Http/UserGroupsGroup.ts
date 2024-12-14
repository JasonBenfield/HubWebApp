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
		this.AddUserGroupIfNotExistsAction = this.createAction<IAddUserGroupIfNotExistsRequest,IAppUserGroupModel>('AddUserGroupIfNotExists', 'Add User Group If Not Exists');
		this.GetUserDetailOrAnonAction = this.createAction<IAppUserNameRequest,IAppUserDetailModel>('GetUserDetailOrAnon', 'Get User Detail Or Anon');
		this.GetUserGroupForUserAction = this.createAction<IAppUserIDRequest,IAppUserGroupModel>('GetUserGroupForUser', 'Get User Group For User');
		this.GetUserGroupsAction = this.createAction<IEmptyRequest,IAppUserGroupModel[]>('GetUserGroups', 'Get User Groups');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.UserQuery = this.createView<IUserGroupKey>('UserQuery');
	}
	
	readonly AddUserGroupIfNotExistsAction: AppClientAction<IAddUserGroupIfNotExistsRequest,IAppUserGroupModel>;
	readonly GetUserDetailOrAnonAction: AppClientAction<IAppUserNameRequest,IAppUserDetailModel>;
	readonly GetUserGroupForUserAction: AppClientAction<IAppUserIDRequest,IAppUserGroupModel>;
	readonly GetUserGroupsAction: AppClientAction<IEmptyRequest,IAppUserGroupModel[]>;
	readonly Index: AppClientView<IEmptyRequest>;
	readonly UserQuery: AppClientView<IUserGroupKey>;
	
	AddUserGroupIfNotExists(model: IAddUserGroupIfNotExistsRequest, errorOptions?: IActionErrorOptions) {
		return this.AddUserGroupIfNotExistsAction.execute(model, errorOptions || {});
	}
	GetUserDetailOrAnon(model: IAppUserNameRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserDetailOrAnonAction.execute(model, errorOptions || {});
	}
	GetUserGroupForUser(model: IAppUserIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserGroupForUserAction.execute(model, errorOptions || {});
	}
	GetUserGroups(errorOptions?: IActionErrorOptions) {
		return this.GetUserGroupsAction.execute({}, errorOptions || {});
	}
}