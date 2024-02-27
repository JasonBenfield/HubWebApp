// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class ResourceGroupGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ResourceGroup');
		this.GetResourceGroupAction = this.createAction<IGetResourceGroupRequest,IResourceGroupModel>('GetResourceGroup', 'Get Resource Group');
		this.GetResourcesAction = this.createAction<IGetResourcesRequest,IResourceModel[]>('GetResources', 'Get Resources');
		this.GetRoleAccessAction = this.createAction<IGetResourceGroupRoleAccessRequest,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
		this.GetModCategoryAction = this.createAction<IGetResourceGroupModCategoryRequest,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceGroupLogRequest,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
	}
	
	readonly GetResourceGroupAction: AppApiAction<IGetResourceGroupRequest,IResourceGroupModel>;
	readonly GetResourcesAction: AppApiAction<IGetResourcesRequest,IResourceModel[]>;
	readonly GetRoleAccessAction: AppApiAction<IGetResourceGroupRoleAccessRequest,IAppRoleModel[]>;
	readonly GetModCategoryAction: AppApiAction<IGetResourceGroupModCategoryRequest,IModifierCategoryModel>;
	readonly GetMostRecentRequestsAction: AppApiAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<IGetResourceGroupLogRequest,IAppLogEntryModel[]>;
	
	GetResourceGroup(model: IGetResourceGroupRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupAction.execute(model, errorOptions || {});
	}
	GetResources(model: IGetResourcesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourcesAction.execute(model, errorOptions || {});
	}
	GetRoleAccess(model: IGetResourceGroupRoleAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAccessAction.execute(model, errorOptions || {});
	}
	GetModCategory(model: IGetResourceGroupModCategoryRequest, errorOptions?: IActionErrorOptions) {
		return this.GetModCategoryAction.execute(model, errorOptions || {});
	}
	GetMostRecentRequests(model: IGetResourceGroupLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
	}
	GetMostRecentErrorEvents(model: IGetResourceGroupLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
	}
}