// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AppUserGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppUser');
		this.Index = this.createView<number>('Index');
		this.GetUserAccessAction = this.createAction<IUserModifierKey,IUserAccessModel>('GetUserAccess', 'Get User Access');
		this.GetUnassignedRolesAction = this.createAction<IUserModifierKey,IAppRoleModel[]>('GetUnassignedRoles', 'Get Unassigned Roles');
		this.GetUserModCategoriesAction = this.createAction<number,IUserModifierCategoryModel[]>('GetUserModCategories', 'Get User Mod Categories');
	}
	
	readonly Index: AppApiView<number>;
	readonly GetUserAccessAction: AppApiAction<IUserModifierKey,IUserAccessModel>;
	readonly GetUnassignedRolesAction: AppApiAction<IUserModifierKey,IAppRoleModel[]>;
	readonly GetUserModCategoriesAction: AppApiAction<number,IUserModifierCategoryModel[]>;
	
	GetUserAccess(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetUserAccessAction.execute(model, errorOptions || {});
	}
	GetUnassignedRoles(model: IUserModifierKey, errorOptions?: IActionErrorOptions) {
		return this.GetUnassignedRolesAction.execute(model, errorOptions || {});
	}
	GetUserModCategories(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserModCategoriesAction.execute(model, errorOptions || {});
	}
}