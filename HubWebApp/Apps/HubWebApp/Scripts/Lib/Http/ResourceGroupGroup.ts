// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class ResourceGroupGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ResourceGroup');
		this.GetResourceGroupAction = this.createAction<IGetResourceGroupRequest,IResourceGroupModel>('GetResourceGroup', 'Get Resource Group');
		this.GetResourcesAction = this.createAction<IGetResourcesRequest,IResourceModel[]>('GetResources', 'Get Resources');
		this.GetRoleAccessAction = this.createAction<IGetResourceGroupRoleAccessRequest,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
		this.GetModCategoryAction = this.createAction<IGetResourceGroupModCategoryRequest,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceGroupLogRequest,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
	}
	
	readonly GetResourceGroupAction: AppClientAction<IGetResourceGroupRequest,IResourceGroupModel>;
	readonly GetResourcesAction: AppClientAction<IGetResourcesRequest,IResourceModel[]>;
	readonly GetRoleAccessAction: AppClientAction<IGetResourceGroupRoleAccessRequest,IAppRoleModel[]>;
	readonly GetModCategoryAction: AppClientAction<IGetResourceGroupModCategoryRequest,IModifierCategoryModel>;
	readonly GetMostRecentRequestsAction: AppClientAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppClientAction<IGetResourceGroupLogRequest,IAppLogEntryModel[]>;
	
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