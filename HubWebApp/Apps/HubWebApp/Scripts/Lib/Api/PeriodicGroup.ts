// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class PeriodicGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Periodic');
		this.PurgeLogsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('PurgeLogs', 'Purge Logs');
	}
	
	readonly PurgeLogsAction: AppApiAction<IEmptyRequest,IEmptyActionResult>;
	
	PurgeLogs(errorOptions?: IActionErrorOptions) {
		return this.PurgeLogsAction.execute({}, errorOptions || {});
	}
}