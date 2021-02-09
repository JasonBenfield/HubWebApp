// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class AppUserGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUser');
		this.Index = this.createView<number>('Index');
		this.GetUserRolesAction = this.createAction<number,IAppUserRoleModel[]>('GetUserRoles', 'Get User Roles');
		this.GetUserRoleAccessAction = this.createAction<number,IUserRoleAccessModel>('GetUserRoleAccess', 'Get User Role Access');
	}
	
	readonly Index: AppApiView<number>;
	readonly GetUserRolesAction: AppApiAction<number,IAppUserRoleModel[]>;
	readonly GetUserRoleAccessAction: AppApiAction<number,IUserRoleAccessModel>;
	
	GetUserRoles(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserRolesAction.execute(model, errorOptions || {});
	}
	GetUserRoleAccess(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserRoleAccessAction.execute(model, errorOptions || {});
	}
}