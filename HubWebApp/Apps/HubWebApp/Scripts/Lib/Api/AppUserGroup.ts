// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AppUserGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUser');
		this.Index = this.createView<IGetAppUserRequest>('Index');
		this.GetUserAccessAction = this.createAction<IUserModifierKey,IUserAccessModel>('GetUserAccess', 'Get User Access');
		this.GetUnassignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetUnassignedRoles', 'Get Unassigned Roles');
	}
	
	readonly Index: AppApiView<IGetAppUserRequest>;
	readonly GetUserAccessAction: AppApiAction<IUserModifierKey,IUserAccessModel>;
	readonly GetUnassignedRolesAction: AppApiAction<IUserModifierKey,IAppRoleModel[]>;
	
	GetUserAccess(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetUserAccessAction.execute(model, errorOptions || {});
	}
	GetUnassignedRoles(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetUnassignedRolesAction.execute(model, errorOptions || {});
	}
}