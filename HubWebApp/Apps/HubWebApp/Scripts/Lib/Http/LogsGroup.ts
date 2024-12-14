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
		this.AppRequestsView = this.createView<IAppRequestQueryRequest>('AppRequestsView');
		this.AppRequestView = this.createView<IAppRequestRequest>('AppRequestView');
		this.GetLogEntryDetailAction = this.createAction<number,IAppLogEntryDetailModel>('GetLogEntryDetail', 'Get Log Entry Detail');
		this.GetLogEntryOrDefaultByKeyAction = this.createAction<string,IAppLogEntryModel>('GetLogEntryOrDefaultByKey', 'Get Log Entry Or Default By Key');
		this.GetRequestDetailAction = this.createAction<number,IAppRequestDetailModel>('GetRequestDetail', 'Get Request Detail');
		this.GetSessionDetailAction = this.createAction<number,IAppSessionDetailModel>('GetSessionDetail', 'Get Session Detail');
		this.LogEntriesView = this.createView<ILogEntryQueryRequest>('LogEntriesView');
		this.LogEntryView = this.createView<ILogEntryRequest>('LogEntryView');
		this.SessionsView = this.createView<IEmptyRequest>('SessionsView');
		this.SessionView = this.createView<ISessionViewRequest>('SessionView');
	}
	
	readonly AppRequestsView: AppClientView<IAppRequestQueryRequest>;
	readonly AppRequestView: AppClientView<IAppRequestRequest>;
	readonly GetLogEntryDetailAction: AppClientAction<number,IAppLogEntryDetailModel>;
	readonly GetLogEntryOrDefaultByKeyAction: AppClientAction<string,IAppLogEntryModel>;
	readonly GetRequestDetailAction: AppClientAction<number,IAppRequestDetailModel>;
	readonly GetSessionDetailAction: AppClientAction<number,IAppSessionDetailModel>;
	readonly LogEntriesView: AppClientView<ILogEntryQueryRequest>;
	readonly LogEntryView: AppClientView<ILogEntryRequest>;
	readonly SessionsView: AppClientView<IEmptyRequest>;
	readonly SessionView: AppClientView<ISessionViewRequest>;
	
	GetLogEntryDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryDetailAction.execute(model, errorOptions || {});
	}
	GetLogEntryOrDefaultByKey(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetLogEntryOrDefaultByKeyAction.execute(model, errorOptions || {});
	}
	GetRequestDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetRequestDetailAction.execute(model, errorOptions || {});
	}
	GetSessionDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetSessionDetailAction.execute(model, errorOptions || {});
	}
}