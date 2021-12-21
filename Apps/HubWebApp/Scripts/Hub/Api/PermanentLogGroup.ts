// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class PermanentLogGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'PermanentLog');
		this.LogBatchAction = this.createAction<ILogBatchModel,IEmptyActionResult>('LogBatch', 'Log Batch');
		this.StartSessionAction = this.createAction<IStartSessionModel,IEmptyActionResult>('StartSession', 'Start Session');
		this.StartRequestAction = this.createAction<IStartRequestModel,IEmptyActionResult>('StartRequest', 'Start Request');
		this.EndRequestAction = this.createAction<IEndRequestModel,IEmptyActionResult>('EndRequest', 'End Request');
		this.EndSessionAction = this.createAction<IEndSessionModel,IEmptyActionResult>('EndSession', 'End Session');
		this.LogEventAction = this.createAction<ILogEventModel,IEmptyActionResult>('LogEvent', 'Log Event');
		this.AuthenticateSessionAction = this.createAction<IAuthenticateSessionModel,IEmptyActionResult>('AuthenticateSession', 'Authenticate Session');
		this.EndExpiredSessionsAction = this.createAction<IEmptyRequest,IEmptyActionResult>('EndExpiredSessions', 'End Expired Sessions');
	}
	
	readonly LogBatchAction: AppApiAction<ILogBatchModel,IEmptyActionResult>;
	readonly StartSessionAction: AppApiAction<IStartSessionModel,IEmptyActionResult>;
	readonly StartRequestAction: AppApiAction<IStartRequestModel,IEmptyActionResult>;
	readonly EndRequestAction: AppApiAction<IEndRequestModel,IEmptyActionResult>;
	readonly EndSessionAction: AppApiAction<IEndSessionModel,IEmptyActionResult>;
	readonly LogEventAction: AppApiAction<ILogEventModel,IEmptyActionResult>;
	readonly AuthenticateSessionAction: AppApiAction<IAuthenticateSessionModel,IEmptyActionResult>;
	readonly EndExpiredSessionsAction: AppApiAction<IEmptyRequest,IEmptyActionResult>;
	
	LogBatch(model: ILogBatchModel, errorOptions?: IActionErrorOptions) {
		return this.LogBatchAction.execute(model, errorOptions || {});
	}
	StartSession(model: IStartSessionModel, errorOptions?: IActionErrorOptions) {
		return this.StartSessionAction.execute(model, errorOptions || {});
	}
	StartRequest(model: IStartRequestModel, errorOptions?: IActionErrorOptions) {
		return this.StartRequestAction.execute(model, errorOptions || {});
	}
	EndRequest(model: IEndRequestModel, errorOptions?: IActionErrorOptions) {
		return this.EndRequestAction.execute(model, errorOptions || {});
	}
	EndSession(model: IEndSessionModel, errorOptions?: IActionErrorOptions) {
		return this.EndSessionAction.execute(model, errorOptions || {});
	}
	LogEvent(model: ILogEventModel, errorOptions?: IActionErrorOptions) {
		return this.LogEventAction.execute(model, errorOptions || {});
	}
	AuthenticateSession(model: IAuthenticateSessionModel, errorOptions?: IActionErrorOptions) {
		return this.AuthenticateSessionAction.execute(model, errorOptions || {});
	}
	EndExpiredSessions(errorOptions?: IActionErrorOptions) {
		return this.EndExpiredSessionsAction.execute({}, errorOptions || {});
	}
}