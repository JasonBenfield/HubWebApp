// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class CommandsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Commands');
		this.GetCommandsInProgressAction = this.createAction<IEmptyRequest,IAppCommandSummaryModel[]>('GetCommandsInProgress', 'Get Commands In Progress');
		this.GetPendingCommandsAction = this.createAction<IGetPendingCommandsRequest,IAppCommandSummaryModel[]>('GetPendingCommands', 'Get Pending Commands');
		this.Index = this.createView<IAppCommandIDRequest>('Index');
	}
	
	readonly GetCommandsInProgressAction: AppClientAction<IEmptyRequest,IAppCommandSummaryModel[]>;
	readonly GetPendingCommandsAction: AppClientAction<IGetPendingCommandsRequest,IAppCommandSummaryModel[]>;
	readonly Index: AppClientView<IAppCommandIDRequest>;
	
	GetCommandsInProgress(errorOptions?: IActionErrorOptions) {
		return this.GetCommandsInProgressAction.execute({}, errorOptions || {});
	}
	GetPendingCommands(requestData: IGetPendingCommandsRequest, errorOptions?: IActionErrorOptions) {
		return this.GetPendingCommandsAction.execute(requestData, errorOptions || {});
	}
}