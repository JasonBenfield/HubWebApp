// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class PermanentLogGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'PermanentLog');
		this.LogBatchAction = this.createAction<ILogBatchModel,IEmptyActionResult>('LogBatch', 'Log Batch');
		this.EndExpiredSessionsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('EndExpiredSessions', 'End Expired Sessions');
	}
	
	readonly LogBatchAction: AppClientAction<ILogBatchModel,IEmptyActionResult>;
	readonly EndExpiredSessionsAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	
	LogBatch(model: ILogBatchModel, errorOptions?: IActionErrorOptions) {
		return this.LogBatchAction.execute(model, errorOptions || {});
	}
	EndExpiredSessions(errorOptions?: IActionErrorOptions) {
		return this.EndExpiredSessionsAction.execute({}, errorOptions || {});
	}
}