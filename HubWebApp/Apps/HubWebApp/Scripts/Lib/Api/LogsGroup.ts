// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class LogsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Logs');
		this.Sessions = this.createView<IEmptyRequest>('Sessions');
		this.Requests = this.createView<IRequestQueryRequest>('Requests');
		this.LogEntries = this.createView<ILogEntryQueryRequest>('LogEntries');
	}
	
	readonly Sessions: AppApiView<IEmptyRequest>;
	readonly Requests: AppApiView<IRequestQueryRequest>;
	readonly LogEntries: AppApiView<ILogEntryQueryRequest>;
	
}