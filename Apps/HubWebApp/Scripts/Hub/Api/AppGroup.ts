// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class AppGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'App');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetAppAction = this.createAction<IEmptyRequest,IAppModel>('GetApp', 'Get App');
		this.GetCurrentVersionAction = this.createAction<IEmptyRequest,IAppVersionModel>('GetCurrentVersion', 'Get Current Version');
		this.GetResourceGroupsAction = this.createAction<IEmptyRequest,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
		this.GetMostRecentRequestsAction = this.createAction<number,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<number,IAppEventModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetModifierCategoriesAction = this.createAction<IEmptyRequest,IModifierCategoryModel[]>('GetModifierCategories', 'Get Modifier Categories');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetAppAction: AppApiAction<IEmptyRequest,IAppModel>;
	readonly GetCurrentVersionAction: AppApiAction<IEmptyRequest,IAppVersionModel>;
	readonly GetResourceGroupsAction: AppApiAction<IEmptyRequest,IResourceGroupModel[]>;
	readonly GetMostRecentRequestsAction: AppApiAction<number,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<number,IAppEventModel[]>;
	readonly GetModifierCategoriesAction: AppApiAction<IEmptyRequest,IModifierCategoryModel[]>;
	
	GetApp(errorOptions?: IActionErrorOptions) {
		return this.GetAppAction.execute({}, errorOptions || {});
	}
	GetCurrentVersion(errorOptions?: IActionErrorOptions) {
		return this.GetCurrentVersionAction.execute({}, errorOptions || {});
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
}