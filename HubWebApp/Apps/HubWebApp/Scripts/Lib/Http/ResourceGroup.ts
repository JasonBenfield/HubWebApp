// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class ResourceGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Resource');
		this.GetResourceAction = this.createAction<IGetResourceRequest,IResourceModel>('GetResource', 'Get Resource');
		this.GetRoleAccessAction = this.createAction<IGetResourceRoleAccessRequest,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceLogRequest,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
	}
	
	readonly GetResourceAction: AppClientAction<IGetResourceRequest,IResourceModel>;
	readonly GetRoleAccessAction: AppClientAction<IGetResourceRoleAccessRequest,IAppRoleModel[]>;
	readonly GetMostRecentRequestsAction: AppClientAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>;
	readonly GetMostRecentErrorEventsAction: AppClientAction<IGetResourceLogRequest,IAppLogEntryModel[]>;
	
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