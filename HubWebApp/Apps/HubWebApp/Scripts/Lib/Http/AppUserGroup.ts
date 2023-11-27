// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppUserGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUser');
		this.Index = this.createView<IGetAppUserRequest>('Index');
		this.GetUserAccessAction = this.createAction<IUserModifierKey,IUserAccessModel>('GetUserAccess', 'Get User Access');
		this.GetUnassignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetUnassignedRoles', 'Get Unassigned Roles');
	}
	
	readonly Index: AppClientView<IGetAppUserRequest>;
	readonly GetUserAccessAction: AppClientAction<IUserModifierKey,IUserAccessModel>;
	readonly GetUnassignedRolesAction: AppClientAction<IUserModifierKey,IAppRoleModel[]>;
	
	GetUserAccess(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetUserAccessAction.execute(model, errorOptions || {});
	}
	GetUnassignedRoles(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetUnassignedRolesAction.execute(model, errorOptions || {});
	}
}