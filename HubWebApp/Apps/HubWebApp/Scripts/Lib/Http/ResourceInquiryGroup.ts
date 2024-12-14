// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class ResourceInquiryGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ResourceInquiry');
		this.GetMostRecentErrorEventsAction = this.createAction<IGetResourceLogRequest,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetMostRecentRequestsAction = this.createAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetResourceAction = this.createAction<IGetResourceRequest,IResourceModel>('GetResource', 'Get Resource');
		this.GetRoleAccessAction = this.createAction<IGetResourceRoleAccessRequest,IAppRoleModel[]>('GetRoleAccess', 'Get Role Access');
	}
	
	readonly GetMostRecentErrorEventsAction: AppClientAction<IGetResourceLogRequest,IAppLogEntryModel[]>;
	readonly GetMostRecentRequestsAction: AppClientAction<IGetResourceLogRequest,IAppRequestExpandedModel[]>;
	readonly GetResourceAction: AppClientAction<IGetResourceRequest,IResourceModel>;
	readonly GetRoleAccessAction: AppClientAction<IGetResourceRoleAccessRequest,IAppRoleModel[]>;
	
	GetMostRecentErrorEvents(model: IGetResourceLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
	}
	GetMostRecentRequests(model: IGetResourceLogRequest, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
	}
	GetResource(model: IGetResourceRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourceAction.execute(model, errorOptions || {});
	}
	GetRoleAccess(model: IGetResourceRoleAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.GetRoleAccessAction.execute(model, errorOptions || {});
	}
}