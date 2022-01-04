// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AppGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'App');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetAppAction = this.createAction<IEmptyRequest,IAppModel>('GetApp', 'Get App');
		this.GetRolesAction = this.createAction<IEmptyRequest,IAppRoleModel[]>('GetRoles', 'Get Roles');
		this.GetRoleAction = this.createAction<string,IAppRoleModel>('GetRole', 'Get Role');
		this.GetResourceGroupsAction = this.createAction<IEmptyRequest,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
		this.GetMostRecentRequestsAction = this.createAction<number,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<number,IAppEventModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetModifierCategoriesAction = this.createAction<IEmptyRequest,IModifierCategoryModel[]>('GetModifierCategories', 'Get Modifier Categories');
		this.GetModifierCategoryAction = this.createAction<string,IModifierCategoryModel>('GetModifierCategory', 'Get Modifier Category');
		this.GetDefaultModiiferAction = this.createAction<IEmptyRequest,IModifierModel>('GetDefaultModiifer', 'Get Default Modiifer');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetAppAction: AppApiAction<IEmptyRequest,IAppModel>;
	readonly GetRolesAction: AppApiAction<IEmptyRequest,IAppRoleModel[]>;
	readonly GetRoleAction: AppApiAction<string,IAppRoleModel>;
	readonly GetResourceGroupsAction: AppApiAction<IEmptyRequest,IResourceGroupModel[]>;
	readonly GetMostRecentRequestsAction: AppApiAction<number,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<number,IAppEventModel[]>;
	readonly GetModifierCategoriesAction: AppApiAction<IEmptyRequest,IModifierCategoryModel[]>;
	readonly GetModifierCategoryAction: AppApiAction<string,IModifierCategoryModel>;
	readonly GetDefaultModiiferAction: AppApiAction<IEmptyRequest,IModifierModel>;
	
	GetApp(errorOptions?: IActionErrorOptions) {
		return this.GetAppAction.execute({}, errorOptions || {});
	}
	GetRoles(errorOptions?: IActionErrorOptions) {
		return this.GetRolesAction.execute({}, errorOptions || {});
	}
	GetRole(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAction.execute(model, errorOptions || {});
	}
	GetResourceGroups(errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupsAction.execute({}, errorOptions || {});
	}
	GetMostRecentRequests(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
	}
	GetMostRecentErrorEvents(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
	}
	GetModifierCategories(errorOptions?: IActionErrorOptions) {
		return this.GetModifierCategoriesAction.execute({}, errorOptions || {});
	}
	GetModifierCategory(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetModifierCategoryAction.execute(model, errorOptions || {});
	}
	GetDefaultModiifer(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultModiiferAction.execute({}, errorOptions || {});
	}
}