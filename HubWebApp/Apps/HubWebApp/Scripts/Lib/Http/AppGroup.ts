// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'App');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetAppAction = this.createAction<IEmptyRequest,IAppModel>('GetApp', 'Get App');
		this.GetResourceGroupsAction = this.createAction<IEmptyRequest,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
		this.GetRolesAction = this.createAction<IEmptyRequest,IAppRoleModel[]>('GetRoles', 'Get Roles');
		this.GetMostRecentRequestsAction = this.createAction<number,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<number,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetModifierCategoriesAction = this.createAction<IEmptyRequest,IModifierCategoryModel[]>('GetModifierCategories', 'Get Modifier Categories');
		this.GetDefaultModifierAction = this.createAction<IEmptyRequest,IModifierModel>('GetDefaultModifier', 'Get Default Modifier');
		this.GetDefaultOptionsAction = this.createAction<IEmptyRequest,string>('GetDefaultOptions', 'Get Default Options');
		this.GetDefaultAppOptionsAction = this.createAction<IEmptyRequest,string>('GetDefaultAppOptions', 'Get Default App Options');
	}
	
	readonly Index: AppClientView<IEmptyRequest>;
	readonly GetAppAction: AppClientAction<IEmptyRequest,IAppModel>;
	readonly GetResourceGroupsAction: AppClientAction<IEmptyRequest,IResourceGroupModel[]>;
	readonly GetRolesAction: AppClientAction<IEmptyRequest,IAppRoleModel[]>;
	readonly GetMostRecentRequestsAction: AppClientAction<number,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppClientAction<number,IAppLogEntryModel[]>;
	readonly GetModifierCategoriesAction: AppClientAction<IEmptyRequest,IModifierCategoryModel[]>;
	readonly GetDefaultModifierAction: AppClientAction<IEmptyRequest,IModifierModel>;
	readonly GetDefaultOptionsAction: AppClientAction<IEmptyRequest,string>;
	readonly GetDefaultAppOptionsAction: AppClientAction<IEmptyRequest,string>;
	
	GetApp(errorOptions?: IActionErrorOptions) {
		return this.GetAppAction.execute({}, errorOptions || {});
	}
	GetResourceGroups(errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupsAction.execute({}, errorOptions || {});
	}
	GetRoles(errorOptions?: IActionErrorOptions) {
		return this.GetRolesAction.execute({}, errorOptions || {});
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
	GetDefaultModifier(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultModifierAction.execute({}, errorOptions || {});
	}
	GetDefaultOptions(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultOptionsAction.execute({}, errorOptions || {});
	}
	GetDefaultAppOptions(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultAppOptionsAction.execute({}, errorOptions || {});
	}
}