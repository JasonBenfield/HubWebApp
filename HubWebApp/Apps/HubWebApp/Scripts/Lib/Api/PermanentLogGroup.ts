// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class PermanentLogGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'PermanentLog');
		this.LogBatchAction = this.createAction<ILogBatchModel,IEmptyActionResult>('LogBatch', 'Log Batch');
		this.EndExpiredSessionsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('EndExpiredSessions', 'End Expired Sessions');
	}
	
	readonly LogBatchAction: AppApiAction<ILogBatchModel,IEmptyActionResult>;
	readonly EndExpiredSessionsAction: AppApiAction<IEmptyRequest,IEmptyActionResult>;
	
	LogBatch(model: ILogBatchModel, errorOptions?: IActionErrorOptions) {
		return this.LogBatchAction.execute(model, errorOptions || {});
	}
	EndExpiredSessions(errorOptions?: IActionErrorOptions) {
		return this.EndExpiredSessionsAction.execute({}, errorOptions || {});
	}
}