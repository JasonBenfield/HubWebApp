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
		this.GetUserRoleAccessAction = this.createAction<IGetUserRoleAccessRequest,IUserRoleAccessModel>('GetUserRoleAccess', 'Get User Role Access');
		this.GetUserModCategoriesAction = this.createAction<number,IUserModifierCategoryModel[]>('GetUserModCategories', 'Get User Mod Categories');
	}
	
	readonly Index: AppApiView<number>;
	readonly GetUserRolesAction: AppApiAction<IGetUserRolesRequest,IAppRoleModel[]>;
	readonly GetUserRoleAccessAction: AppApiAction<IGetUserRoleAccessRequest,IUserRoleAccessModel>;
	readonly GetUserModCategoriesAction: AppApiAction<number,IUserModifierCategoryModel[]>;
	
	GetUserRoles(model: IGetUserRolesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRolesAction.execute(model, errorOptions || {});
	}
	GetUserRoleAccess(model: IGetUserRoleAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRoleAccessAction.execute(model, errorOptions || {});
	}
	GetUserModCategories(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserModCategoriesAction.execute(model, errorOptions || {});
	}
}