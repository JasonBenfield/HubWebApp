// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class ResourceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Resource');
		this.GetResourceAction = this.createAction<IGetResourceRequest,IResourceModel>('GetResource', 'Get Resource');
		this.GetRoleAccessAction = this.createAction<IGetResourceRoleAccessRequest,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceLogRequest,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
	}
	
	readonly GetResourceAction: AppApiAction<IGetResourceRequest,IResourceModel>;
	readonly GetRoleAccessAction: AppApiAction<IGetResourceRoleAccessRequest,IAppRoleModel[]>;
	readonly GetMostRecentRequestsAction: AppApiAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<IGetResourceLogRequest,IAppLogEntryModel[]>;
	
	GetResource(model: IGetResourceRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourceAction.execute(model, errorOptions || {});
	}
	GetRoleAccess(model: IGetResourceRoleAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAccessAction.execute(model, errorOptions || {});
	}
	GetMostRecentRequests(model: IGetResourceLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
	}
	GetMostRecentErrorEvents(model: IGetResourceLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
	}
}