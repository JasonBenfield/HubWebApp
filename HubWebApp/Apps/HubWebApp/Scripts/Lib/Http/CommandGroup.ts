// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class CommandGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Command');
		this.GetCommandAction = this.createAction<IAppCommandIDRequest,IAppCommandModel>('GetCommand', 'Get Command');
		this.GetDeleteCommandDetailAction = this.createAction<IAppCommandIDRequest,IAppDeleteCommandDetailModel>('GetDeleteCommandDetail', 'Get Delete Command Detail');
		this.GetInstallationCommandDetailAction = this.createAction<IAppCommandIDRequest,IAppInstallCommandDetailModel>('GetInstallationCommandDetail', 'Get Installation Command Detail');
		this.Index = this.createView<IAppCommandIDRequest>('Index');
	}
	
	readonly GetCommandAction: AppClientAction<IAppCommandIDRequest,IAppCommandModel>;
	readonly GetDeleteCommandDetailAction: AppClientAction<IAppCommandIDRequest,IAppDeleteCommandDetailModel>;
	readonly GetInstallationCommandDetailAction: AppClientAction<IAppCommandIDRequest,IAppInstallCommandDetailModel>;
	readonly Index: AppClientView<IAppCommandIDRequest>;
	
	GetCommand(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetCommandAction.execute(requestData, errorOptions || {});
	}
	GetDeleteCommandDetail(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetDeleteCommandDetailAction.execute(requestData, errorOptions || {});
	}
	GetInstallationCommandDetail(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationCommandDetailAction.execute(requestData, errorOptions || {});
	}
}