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
		this.GetExplicitUserAccessAction = this.createAction<IUserModifierKey,IUserAccessModel>('GetExplicitUserAccess', 'Get Explicit User Access');
		this.GetAssignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetAssignedRoles', 'Get Assigned Roles');
		this.GetExplicitlyUnassignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetExplicitlyUnassignedRoles', 'Get Explicitly Unassigned Roles');
	}
	
	readonly Index: AppClientView<IGetAppUserRequest>;
	readonly GetExplicitUserAccessAction: AppClientAction<IUserModifierKey,IUserAccessModel>;
	readonly GetAssignedRolesAction: AppClientAction<IUserModifierKey,IAppRoleModel[]>;
	readonly GetExplicitlyUnassignedRolesAction: AppClientAction<IUserModifierKey,IAppRoleModel[]>;
	
	GetExplicitUserAccess(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetExplicitUserAccessAction.execute(model, errorOptions || {});
	}
	GetAssignedRoles(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetAssignedRolesAction.execute(model, errorOptions || {});
	}
	GetExplicitlyUnassignedRoles(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetExplicitlyUnassignedRolesAction.execute(model, errorOptions || {});
	}
}