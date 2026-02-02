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
		this.LogSessionDetailsAction = this.createAction<ILogSessionDetailsRequest,IEmptyActionResult>('LogSessionDetails', 'Log Session Details');
	}
	
	readonly LogSessionDetailsAction: AppClientAction<ILogSessionDetailsRequest,IEmptyActionResult>;
	
	LogSessionDetails(requestData: ILogSessionDetailsRequest, errorOptions?: IActionErrorOptions) {
		return this.LogSessionDetailsAction.execute(requestData, errorOptions || {});
	}
}