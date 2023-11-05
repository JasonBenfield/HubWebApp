// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class PeriodicGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Periodic');
		this.PurgeLogsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('PurgeLogs', 'Purge Logs');
	}
	
	readonly PurgeLogsAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	
	PurgeLogs(errorOptions?: IActionErrorOptions) {
		return this.PurgeLogsAction.execute({}, errorOptions || {});
	}
}