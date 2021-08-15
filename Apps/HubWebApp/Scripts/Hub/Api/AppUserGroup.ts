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
		this.GetUserRolesAction = this.createAction<number,IAppRoleModel[]>('GetUserRoles', 'Get User Roles');
		this.GetUserRoleAccessAction = this.createAction<IGetUserRoleAccessRequest,IUserRoleAccessModel>('GetUserRoleAccess', 'Get User Role Access');
		this.GetUserModCategoriesAction = this.createAction<number,IUserModifierCategoryModel[]>('GetUserModCategories', 'Get User Mod Categories');
	}
	
	readonly Index: AppApiView<number>;
	readonly GetUserRolesAction: AppApiAction<number,IAppRoleModel[]>;
	readonly GetUserRoleAccessAction: AppApiAction<IGetUserRoleAccessRequest,IUserRoleAccessModel>;
	readonly GetUserModCategoriesAction: AppApiAction<number,IUserModifierCategoryModel[]>;
	
	GetUserRoles(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserRolesAction.execute(model, errorOptions || {});
	}
	GetUserRoleAccess(model: IGetUserRoleAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRoleAccessAction.execute(model, errorOptions || {});
	}
	GetUserModCategories(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserModCategoriesAction.execute(model, errorOptions || {});
	}
}