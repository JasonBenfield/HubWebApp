// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class LogsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Logs');
		this.GetLogEntryByKeyAction = this.createAction<string,IAppLogEntryModel>('GetLogEntryByKey', 'Get Log Entry By Key');
		this.GetLogEntryDetailAction = this.createAction<number,IAppLogEntryDetailModel>('GetLogEntryDetail', 'Get Log Entry Detail');
		this.GetRequestDetailAction = this.createAction<number,IAppRequestDetailModel>('GetRequestDetail', 'Get Request Detail');
		this.GetSessionDetailAction = this.createAction<number,IAppSessionDetailModel>('GetSessionDetail', 'Get Session Detail');
		this.Sessions = this.createView<IEmptyRequest>('Sessions');
		this.Session = this.createView<ISessionViewRequest>('Session');
		this.Requests = this.createView<IRequestQueryRequest>('Requests');
		this.Request = this.createView<IRequestRequest>('Request');
		this.LogEntry = this.createView<ILogEntryRequest>('LogEntry');
		this.LogEntries = this.createView<ILogEntryQueryRequest>('LogEntries');
	}
	
	readonly GetLogEntryByKeyAction: AppApiAction<string,IAppLogEntryModel>;
	readonly GetLogEntryDetailAction: AppApiAction<number,IAppLogEntryDetailModel>;
	readonly GetRequestDetailAction: AppApiAction<number,IAppRequestDetailModel>;
	readonly GetSessionDetailAction: AppApiAction<number,IAppSessionDetailModel>;
	readonly Sessions: AppApiView<IEmptyRequest>;
	readonly Session: AppApiView<ISessionViewRequest>;
	readonly Requests: AppApiView<IRequestQueryRequest>;
	readonly Request: AppApiView<IRequestRequest>;
	readonly LogEntry: AppApiView<ILogEntryRequest>;
	readonly LogEntries: AppApiView<ILogEntryQueryRequest>;
	
	GetLogEntryByKey(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryByKeyAction.execute(model, errorOptions || {});
	}
	GetLogEntryDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryDetailAction.execute(model, errorOptions || {});
	}
	GetRequestDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetRequestDetailAction.execute(model, errorOptions || {});
	}
	GetSessionDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetSessionDetailAction.execute(model, errorOptions || {});
	}
}