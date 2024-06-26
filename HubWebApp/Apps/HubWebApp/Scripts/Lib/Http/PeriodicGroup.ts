// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class PeriodicGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Periodic');
		this.DeactivateUsersAction = this.createAction<IEmptyRequest,IEmptyActionResult>('DeactivateUsers', 'Deactivate Users');
		this.DeleteExpiredStoredObjectsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('DeleteExpiredStoredObjects', 'Delete Expired Stored Objects');
		this.EndExpiredSessionsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('EndExpiredSessions', 'End Expired Sessions');
		this.PurgeLogsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('PurgeLogs', 'Purge Logs');
	}
	
	readonly DeactivateUsersAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	readonly DeleteExpiredStoredObjectsAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	readonly EndExpiredSessionsAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	readonly PurgeLogsAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	
	DeactivateUsers(errorOptions?: IActionErrorOptions) {
		return this.DeactivateUsersAction.execute({}, errorOptions || {});
	}
	DeleteExpiredStoredObjects(errorOptions?: IActionErrorOptions) {
		return this.DeleteExpiredStoredObjectsAction.execute({}, errorOptions || {});
	}
	EndExpiredSessions(errorOptions?: IActionErrorOptions) {
		return this.EndExpiredSessionsAction.execute({}, errorOptions || {});
	}
	PurgeLogs(errorOptions?: IActionErrorOptions) {
		return this.PurgeLogsAction.execute({}, errorOptions || {});
	}
}