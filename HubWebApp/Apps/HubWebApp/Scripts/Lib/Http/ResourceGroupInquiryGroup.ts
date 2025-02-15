// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class ResourceGroupInquiryGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ResourceGroupInquiry');
		this.GetModCategoryAction = this.createAction<IGetResourceGroupModCategoryRequest,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceGroupLogRequest,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetResourceGroupAction = this.createAction<IGetResourceGroupRequest,IResourceGroupModel>('GetResourceGroup', 'Get Resource Group');
		this.GetResourcesAction = this.createAction<IGetResourcesRequest,IResourceModel[]>('GetResources', 'Get Resources');
		this.GetRoleAccessAction = this.createAction<IGetResourceGroupRoleAccessRequest,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
	}
	
	readonly GetModCategoryAction: AppClientAction<IGetResourceGroupModCategoryRequest,IModifierCategoryModel>;
	readonly GetMostRecentErrorEventsAction: AppClientAction<IGetResourceGroupLogRequest,IAppLogEntryModel[]>;
	readonly GetMostRecentRequestsAction: AppClientAction<IGetResourceGroupLogRequest,IAppRequestExpandedModel[]>;
	readonly GetResourceGroupAction: AppClientAction<IGetResourceGroupRequest,IResourceGroupModel>;
	readonly GetResourcesAction: AppClientAction<IGetResourcesRequest,IResourceModel[]>;
	readonly GetRoleAccessAction: AppClientAction<IGetResourceGroupRoleAccessRequest,IAppRoleModel[]>;
	
	GetModCategory(requestData: IGetResourceGroupModCategoryRequest, errorOptions?: IActionErrorOptions) {
		return this.GetModCategoryAction.execute(requestData, errorOptions || {});
	}
	GetMostRecentErrorEvents(requestData: IGetResourceGroupLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(requestData, errorOptions || {});
	}
	GetMostRecentRequests(requestData: IGetResourceGroupLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(requestData, errorOptions || {});
	}
	GetResourceGroup(requestData: IGetResourceGroupRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupAction.execute(requestData, errorOptions || {});
	}
	GetResources(requestData: IGetResourcesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourcesAction.execute(requestData, errorOptions || {});
	}
	GetRoleAccess(requestData: IGetResourceGroupRoleAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAccessAction.execute(requestData, errorOptions || {});
	}
}