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
		this.BeginCommandAction = this.createAction<IAppCommandIDRequest,IAppCommandModel>('BeginCommand', 'Begin Command');
		this.BeginCommandStepAction = this.createAction<IBeginAppCommandStepRequest,IAppCommandStepModel>('BeginCommandStep', 'Begin Command Step');
		this.BeginInstallationAction = this.createAction<IBeginInstallationRequest,IInstallationModel>('BeginInstallation', 'Begin Installation');
		this.CommandEndedAction = this.createAction<IAppCommandIDRequest,IEmptyActionResult>('CommandEnded', 'Command Ended');
		this.CommandStepEndedAction = this.createAction<IAppCommandStepEndedRequest,IEmptyRequest>('CommandStepEnded', 'Command Step Ended');
		this.GetCommandAction = this.createAction<IAppCommandIDRequest,IAppCommandModel>('GetCommand', 'Get Command');
		this.GetDeleteCommandDetailAction = this.createAction<IAppCommandIDRequest,IAppDeleteCommandDetailModel>('GetDeleteCommandDetail', 'Get Delete Command Detail');
		this.GetInstallationCommandDetailAction = this.createAction<IAppCommandIDRequest,IAppInstallCommandDetailModel>('GetInstallationCommandDetail', 'Get Installation Command Detail');
		this.Index = this.createView<IAppCommandIDRequest>('Index');
	}
	
	readonly BeginCommandAction: AppClientAction<IAppCommandIDRequest,IAppCommandModel>;
	readonly BeginCommandStepAction: AppClientAction<IBeginAppCommandStepRequest,IAppCommandStepModel>;
	readonly BeginInstallationAction: AppClientAction<IBeginInstallationRequest,IInstallationModel>;
	readonly CommandEndedAction: AppClientAction<IAppCommandIDRequest,IEmptyActionResult>;
	readonly CommandStepEndedAction: AppClientAction<IAppCommandStepEndedRequest,IEmptyRequest>;
	readonly GetCommandAction: AppClientAction<IAppCommandIDRequest,IAppCommandModel>;
	readonly GetDeleteCommandDetailAction: AppClientAction<IAppCommandIDRequest,IAppDeleteCommandDetailModel>;
	readonly GetInstallationCommandDetailAction: AppClientAction<IAppCommandIDRequest,IAppInstallCommandDetailModel>;
	readonly Index: AppClientView<IAppCommandIDRequest>;
	
	BeginCommand(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginCommandAction.execute(requestData, errorOptions || {});
	}
	BeginCommandStep(requestData: IBeginAppCommandStepRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginCommandStepAction.execute(requestData, errorOptions || {});
	}
	BeginInstallation(requestData: IBeginInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginInstallationAction.execute(requestData, errorOptions || {});
	}
	CommandEnded(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.CommandEndedAction.execute(requestData, errorOptions || {});
	}
	CommandStepEnded(requestData: IAppCommandStepEndedRequest, errorOptions?: IActionErrorOptions) {
		return this.CommandStepEndedAction.execute(requestData, errorOptions || {});
	}
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