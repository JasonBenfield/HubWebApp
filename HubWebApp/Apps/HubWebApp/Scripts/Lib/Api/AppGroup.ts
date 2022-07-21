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
		this.GetResourceGroupsAction = this.createAction<IEmptyRequest,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
		this.GetMostRecentRequestsAction = this.createAction<number,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<number,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetModifierCategoriesAction = this.createAction<IEmptyRequest,IModifierCategoryModel[]>('GetModifierCategories', 'Get Modifier Categories');
		this.GetDefaultModifierAction = this.createAction<IEmptyRequest,IModifierModel>('GetDefaultModifier', 'Get Default Modifier');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetAppAction: AppApiAction<IEmptyRequest,IAppModel>;
	readonly GetResourceGroupsAction: AppApiAction<IEmptyRequest,IResourceGroupModel[]>;
	readonly GetMostRecentRequestsAction: AppApiAction<number,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<number,IAppLogEntryModel[]>;
	readonly GetModifierCategoriesAction: AppApiAction<IEmptyRequest,IModifierCategoryModel[]>;
	readonly GetDefaultModifierAction: AppApiAction<IEmptyRequest,IModifierModel>;
	
	GetApp(errorOptions?: IActionErrorOptions) {
		return this.GetAppAction.execute({}, errorOptions || {});
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
	GetDefaultModifier(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultModifierAction.execute({}, errorOptions || {});
	}
}