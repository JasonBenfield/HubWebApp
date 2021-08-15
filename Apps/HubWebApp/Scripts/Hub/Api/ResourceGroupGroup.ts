// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class ResourceGroupGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ResourceGroup');
		this.GetResourceGroupAction = this.createAction<number,IResourceGroupModel>('GetResourceGroup', 'Get Resource Group');
		this.GetResourcesAction = this.createAction<number,IResourceModel[]>('GetResources', 'Get Resources');
		this.GetRoleAccessAction = this.createAction<number,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
		this.GetModCategoryAction = this.createAction<number,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceGroupLogRequest,IAppEventModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
	}
	
	readonly GetResourceGroupAction: AppApiAction<number,IResourceGroupModel>;
	readonly GetResourcesAction: AppApiAction<number,IResourceModel[]>;
	readonly GetRoleAccessAction: AppApiAction<number,IAppRoleModel[]>;
	readonly GetModCategoryAction: AppApiAction<number,IModifierCategoryModel>;
	readonly GetMostRecentRequestsAction: AppApiAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<IGetResourceGroupLogRequest,IAppEventModel[]>;
	
	GetResourceGroup(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupAction.execute(model, errorOptions || {});
	}
	GetResources(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetResourcesAction.execute(model, errorOptions || {});
	}
	GetRoleAccess(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAccessAction.execute(model, errorOptions || {});
	}
	GetModCategory(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetModCategoryAction.execute(model, errorOptions || {});
	}
	GetMostRecentRequests(model: IGetResourceGroupLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
	}
	GetMostRecentErrorEvents(model: IGetResourceGroupLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
	}
}