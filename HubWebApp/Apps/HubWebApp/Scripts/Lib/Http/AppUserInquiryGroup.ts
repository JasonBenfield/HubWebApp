// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppUserInquiryGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUserInquiry');
		this.GetAssignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetAssignedRoles', 'Get Assigned Roles');
		this.GetExplicitlyUnassignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetExplicitlyUnassignedRoles', 'Get Explicitly Unassigned Roles');
		this.GetExplicitUserAccessAction = this.createAction<IUserModifierKey,IUserAccessModel>('GetExplicitUserAccess', 'Get Explicit User Access');
		this.Index = this.createView<IAppUserIndexRequest>('Index');
	}
	
	readonly GetAssignedRolesAction: AppClientAction<IUserModifierKey,IAppRoleModel[]>;
	readonly GetExplicitlyUnassignedRolesAction: AppClientAction<IUserModifierKey,IAppRoleModel[]>;
	readonly GetExplicitUserAccessAction: AppClientAction<IUserModifierKey,IUserAccessModel>;
	readonly Index: AppClientView<IAppUserIndexRequest>;
	
	GetAssignedRoles(requestData: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetAssignedRolesAction.execute(requestData, errorOptions || {});
	}
	GetExplicitlyUnassignedRoles(requestData: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetExplicitlyUnassignedRolesAction.execute(requestData, errorOptions || {});
	}
	GetExplicitUserAccess(requestData: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetExplicitUserAccessAction.execute(requestData, errorOptions || {});
	}
}