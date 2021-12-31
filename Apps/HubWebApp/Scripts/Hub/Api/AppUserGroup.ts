// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class AppUserGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUser');
		this.Index = this.createView<number>('Index');
		this.GetUserRolesAction = this.createAction<IGetUserRolesRequest,IAppRoleModel[]>('GetUserRoles', 'Get User Roles');
		this.GetUnassignedRolesAction = this.createAction<IGetUnassignedRolesRequest,IAppRoleModel[]>('GetUnassignedRoles', 'Get Unassigned Roles');
		this.GetUserModCategoriesAction = this.createAction<number,IUserModifierCategoryModel[]>('GetUserModCategories', 'Get User Mod Categories');
	}
	
	readonly Index: AppApiView<number>;
	readonly GetUserRolesAction: AppApiAction<IGetUserRolesRequest,IAppRoleModel[]>;
	readonly GetUnassignedRolesAction: AppApiAction<IGetUnassignedRolesRequest,IAppRoleModel[]>;
	readonly GetUserModCategoriesAction: AppApiAction<number,IUserModifierCategoryModel[]>;
	
	GetUserRoles(model: IGetUserRolesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRolesAction.execute(model, errorOptions || {});
	}
	GetUnassignedRoles(model: IGetUnassignedRolesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUnassignedRolesAction.execute(model, errorOptions || {});
	}
	GetUserModCategories(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserModCategoriesAction.execute(model, errorOptions || {});
	}
}