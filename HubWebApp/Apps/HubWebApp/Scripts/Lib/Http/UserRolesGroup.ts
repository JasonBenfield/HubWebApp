// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class UserRolesGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserRoles');
		this.Index = this.createView<IUserRoleQueryRequest>('Index');
		this.GetUserRoleDetailAction = this.createAction<number,IUserRoleDetailModel>('GetUserRoleDetail', 'Get User Role Detail');
	}
	
	readonly Index: AppClientView<IUserRoleQueryRequest>;
	readonly GetUserRoleDetailAction: AppClientAction<number,IUserRoleDetailModel>;
	
	GetUserRoleDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserRoleDetailAction.execute(model, errorOptions || {});
	}
}