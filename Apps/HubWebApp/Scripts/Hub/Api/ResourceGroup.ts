// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class ResourceGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Resource');
		this.GetResourceAction = this.createAction<number,IResourceModel>('GetResource', 'Get Resource');
		this.GetRoleAccessAction = this.createAction<number,IRoleAccessModel>('GetRoleAccess', 'Get Role Access');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceLogRequest,IAppEventModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
	}
	
	readonly GetResourceAction: AppApiAction<number,IResourceModel>;
	readonly GetRoleAccessAction: AppApiAction<number,IRoleAccessModel>;
	readonly GetMostRecentRequestsAction: AppApiAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppApiAction<IGetResourceLogRequest,IAppEventModel[]>;
	
	GetResource(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetResourceAction.execute(model, errorOptions || {});
	}
	GetRoleAccess(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAccessAction.execute(model, errorOptions || {});
	}
	GetMostRecentRequests(model: IGetResourceLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
	}
	GetMostRecentErrorEvents(model: IGetResourceLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
	}
}