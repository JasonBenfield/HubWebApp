// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class LogsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Logs');
		this.AppRequest = this.createView<IAppRequestRequest>('AppRequest');
		this.AppRequests = this.createView<IAppRequestQueryRequest>('AppRequests');
		this.GetLogEntryDetailAction = this.createAction<number,IAppLogEntryDetailModel>('GetLogEntryDetail', 'Get Log Entry Detail');
		this.GetLogEntryOrDefaultByKeyAction = this.createAction<string,IAppLogEntryModel>('GetLogEntryOrDefaultByKey', 'Get Log Entry Or Default By Key');
		this.GetRequestDetailAction = this.createAction<number,IAppRequestDetailModel>('GetRequestDetail', 'Get Request Detail');
		this.GetSessionDetailAction = this.createAction<number,IAppSessionDetailModel>('GetSessionDetail', 'Get Session Detail');
		this.LogEntries = this.createView<ILogEntryQueryRequest>('LogEntries');
		this.LogEntry = this.createView<ILogEntryRequest>('LogEntry');
		this.Session = this.createView<ISessionViewRequest>('Session');
		this.Sessions = this.createView<IEmptyRequest>('Sessions');
	}
	
	readonly AppRequest: AppClientView<IAppRequestRequest>;
	readonly AppRequests: AppClientView<IAppRequestQueryRequest>;
	readonly GetLogEntryDetailAction: AppClientAction<number,IAppLogEntryDetailModel>;
	readonly GetLogEntryOrDefaultByKeyAction: AppClientAction<string,IAppLogEntryModel>;
	readonly GetRequestDetailAction: AppClientAction<number,IAppRequestDetailModel>;
	readonly GetSessionDetailAction: AppClientAction<number,IAppSessionDetailModel>;
	readonly LogEntries: AppClientView<ILogEntryQueryRequest>;
	readonly LogEntry: AppClientView<ILogEntryRequest>;
	readonly Session: AppClientView<ISessionViewRequest>;
	readonly Sessions: AppClientView<IEmptyRequest>;
	
	GetLogEntryDetail(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryDetailAction.execute(requestData, errorOptions || {});
	}
	GetLogEntryOrDefaultByKey(requestData: string, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryOrDefaultByKeyAction.execute(requestData, errorOptions || {});
	}
	GetRequestDetail(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetRequestDetailAction.execute(requestData, errorOptions || {});
	}
	GetSessionDetail(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetSessionDetailAction.execute(requestData, errorOptions || {});
	}
}