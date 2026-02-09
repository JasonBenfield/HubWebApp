// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class InstallationsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Installations');
		this.AddDeleteCommandAction = this.createAction<IInstallationIDRequest,IAppDeleteCommandDetailModel>('AddDeleteCommand', 'Add Delete Command');
		this.AddInstallCommandAction = this.createAction<IAddInstallCommandRequest,IAppInstallCommandDetailModel>('AddInstallCommand', 'Add Install Command');
		this.BeginCommandAction = this.createAction<IAppCommandIDRequest,IAppCommandModel>('BeginCommand', 'Begin Command');
		this.BeginCommandStepAction = this.createAction<IBeginAppCommandStepRequest,IAppCommandStepModel>('BeginCommandStep', 'Begin Command Step');
		this.BeginDeleteAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('BeginDelete', 'Begin Delete');
		this.BeginInstallationAction = this.createAction<IBeginInstallationRequest,IInstallationModel>('BeginInstallation', 'Begin Installation');
		this.CommandEndedAction = this.createAction<IAppCommandIDRequest,IEmptyActionResult>('CommandEnded', 'Command Ended');
		this.CommandStepEndedAction = this.createAction<IAppCommandStepEndedRequest,IEmptyRequest>('CommandStepEnded', 'Command Step Ended');
		this.DeletedAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('Deleted', 'Deleted');
		this.GetDeleteCommandDetailAction = this.createAction<IAppCommandIDRequest,IAppDeleteCommandDetailModel>('GetDeleteCommandDetail', 'Get Delete Command Detail');
		this.GetInstallationCommandDetailAction = this.createAction<IAppCommandIDRequest,IAppInstallCommandDetailModel>('GetInstallationCommandDetail', 'Get Installation Command Detail');
		this.GetInstallationDetailAction = this.createAction<IInstallationIDRequest,IInstallationDetailModel>('GetInstallationDetail', 'Get Installation Detail');
		this.GetPendingCommandsAction = this.createAction<IGetPendingCommandsRequest,IAppCommandModel[]>('GetPendingCommands', 'Get Pending Commands');
		this.Index = this.createView<IInstallationQueryRequest>('Index');
		this.Installation = this.createView<IInstallationViewRequest>('Installation');
		this.InstalledAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('Installed', 'Installed');
	}
	
	readonly AddDeleteCommandAction: AppClientAction<IInstallationIDRequest,IAppDeleteCommandDetailModel>;
	readonly AddInstallCommandAction: AppClientAction<IAddInstallCommandRequest,IAppInstallCommandDetailModel>;
	readonly BeginCommandAction: AppClientAction<IAppCommandIDRequest,IAppCommandModel>;
	readonly BeginCommandStepAction: AppClientAction<IBeginAppCommandStepRequest,IAppCommandStepModel>;
	readonly BeginDeleteAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly BeginInstallationAction: AppClientAction<IBeginInstallationRequest,IInstallationModel>;
	readonly CommandEndedAction: AppClientAction<IAppCommandIDRequest,IEmptyActionResult>;
	readonly CommandStepEndedAction: AppClientAction<IAppCommandStepEndedRequest,IEmptyRequest>;
	readonly DeletedAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly GetDeleteCommandDetailAction: AppClientAction<IAppCommandIDRequest,IAppDeleteCommandDetailModel>;
	readonly GetInstallationCommandDetailAction: AppClientAction<IAppCommandIDRequest,IAppInstallCommandDetailModel>;
	readonly GetInstallationDetailAction: AppClientAction<IInstallationIDRequest,IInstallationDetailModel>;
	readonly GetPendingCommandsAction: AppClientAction<IGetPendingCommandsRequest,IAppCommandModel[]>;
	readonly Index: AppClientView<IInstallationQueryRequest>;
	readonly Installation: AppClientView<IInstallationViewRequest>;
	readonly InstalledAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	
	AddDeleteCommand(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.AddDeleteCommandAction.execute(requestData, errorOptions || {});
	}
	AddInstallCommand(requestData: IAddInstallCommandRequest, errorOptions?: IActionErrorOptions) {
		return this.AddInstallCommandAction.execute(requestData, errorOptions || {});
	}
	BeginCommand(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginCommandAction.execute(requestData, errorOptions || {});
	}
	BeginCommandStep(requestData: IBeginAppCommandStepRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginCommandStepAction.execute(requestData, errorOptions || {});
	}
	BeginDelete(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginDeleteAction.execute(requestData, errorOptions || {});
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
	Deleted(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.DeletedAction.execute(requestData, errorOptions || {});
	}
	GetDeleteCommandDetail(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetDeleteCommandDetailAction.execute(requestData, errorOptions || {});
	}
	GetInstallationCommandDetail(requestData: IAppCommandIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationCommandDetailAction.execute(requestData, errorOptions || {});
	}
	GetInstallationDetail(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationDetailAction.execute(requestData, errorOptions || {});
	}
	GetPendingCommands(requestData: IGetPendingCommandsRequest, errorOptions?: IActionErrorOptions) {
		return this.GetPendingCommandsAction.execute(requestData, errorOptions || {});
	}
	Installed(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.InstalledAction.execute(requestData, errorOptions || {});
	}
}