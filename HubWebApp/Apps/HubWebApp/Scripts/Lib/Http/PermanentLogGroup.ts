// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class PermanentLogGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'PermanentLog');
		this.LogBatchAction = this.createAction<ILogBatchModel,IEmptyActionResult>('LogBatch', 'Log Batch');
		this.LogSessionDetailsAction = this.createAction<ILogSessionDetailsRequest,IEmptyActionResult>('LogSessionDetails', 'Log Session Details');
	}
	
	readonly LogBatchAction: AppClientAction<ILogBatchModel,IEmptyActionResult>;
	readonly LogSessionDetailsAction: AppClientAction<ILogSessionDetailsRequest,IEmptyActionResult>;
	
	LogBatch(model: ILogBatchModel, errorOptions?: IActionErrorOptions) {
		return this.LogBatchAction.execute(model, errorOptions || {});
	}
	LogSessionDetails(model: ILogSessionDetailsRequest, errorOptions?: IActionErrorOptions) {
		return this.LogSessionDetailsAction.execute(model, errorOptions || {});
	}
}