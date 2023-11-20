// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class LogsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Logs');
		this.GetLogEntryOrDefaultByKeyAction = this.createAction<string,IAppLogEntryModel>('GetLogEntryOrDefaultByKey', 'Get Log Entry Or Default By Key');
		this.GetLogEntryDetailAction = this.createAction<number,IAppLogEntryDetailModel>('GetLogEntryDetail', 'Get Log Entry Detail');
		this.GetRequestDetailAction = this.createAction<number,IAppRequestDetailModel>('GetRequestDetail', 'Get Request Detail');
		this.GetSessionDetailAction = this.createAction<number,IAppSessionDetailModel>('GetSessionDetail', 'Get Session Detail');
		this.Sessions = this.createView<IEmptyRequest>('Sessions');
		this.Session = this.createView<ISessionViewRequest>('Session');
		this.AppRequests = this.createView<IAppRequestQueryRequest>('AppRequests');
		this.AppRequest = this.createView<IAppRequestRequest>('AppRequest');
		this.LogEntry = this.createView<ILogEntryRequest>('LogEntry');
		this.LogEntries = this.createView<ILogEntryQueryRequest>('LogEntries');
	}
	
	readonly GetLogEntryOrDefaultByKeyAction: AppClientAction<string,IAppLogEntryModel>;
	readonly GetLogEntryDetailAction: AppClientAction<number,IAppLogEntryDetailModel>;
	readonly GetRequestDetailAction: AppClientAction<number,IAppRequestDetailModel>;
	readonly GetSessionDetailAction: AppClientAction<number,IAppSessionDetailModel>;
	readonly Sessions: AppClientView<IEmptyRequest>;
	readonly Session: AppClientView<ISessionViewRequest>;
	readonly AppRequests: AppClientView<IAppRequestQueryRequest>;
	readonly AppRequest: AppClientView<IAppRequestRequest>;
	readonly LogEntry: AppClientView<ILogEntryRequest>;
	readonly LogEntries: AppClientView<ILogEntryQueryRequest>;
	
	GetLogEntryOrDefaultByKey(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryOrDefaultByKeyAction.execute(model, errorOptions || {});
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