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
		this.BeginDeleteAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('BeginDelete', 'Begin Delete');
		this.BeginInstallationAction = this.createAction<IBeginInstallationRequest,IInstallationModel>('BeginInstallation', 'Begin Installation');
		this.BeginRequestedInstallationAction = this.createAction<IRequestedInstallationIDRequest,IRequestedInstallationModel>('BeginRequestedInstallation', 'Begin Requested Installation');
		this.BeginRequestedInstallationStepAction = this.createAction<IBeginRequestedInstallationStepRequest,IRequestedInstallationStepModel>('BeginRequestedInstallationStep', 'Begin Requested Installation Step');
		this.DeletedAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('Deleted', 'Deleted');
		this.GetInstallationActivitiesAction = this.createAction<IGetInstallationActivitiesRequest,IInstallationActivitiesResult>('GetInstallationActivities', 'Get Installation Activities');
		this.GetInstallationDetailAction = this.createAction<IInstallationIDRequest,IInstallationDetailModel>('GetInstallationDetail', 'Get Installation Detail');
		this.GetRequestedInstallationDetailAction = this.createAction<IRequestedInstallationIDRequest,IRequestedInstallationDetailModel>('GetRequestedInstallationDetail', 'Get Requested Installation Detail');
		this.Index = this.createView<IInstallationQueryRequest>('Index');
		this.Installation = this.createView<IInstallationViewRequest>('Installation');
		this.InstalledAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('Installed', 'Installed');
		this.RequestDeleteAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('RequestDelete', 'Request Delete');
		this.RequestedInstallationEndedAction = this.createAction<IRequestedInstallationIDRequest,IEmptyActionResult>('RequestedInstallationEnded', 'Requested Installation Ended');
		this.RequestedInstallationStepEndedAction = this.createAction<IRequestedInstallationStepEndedRequest,IEmptyRequest>('RequestedInstallationStepEnded', 'Requested Installation Step Ended');
		this.RequestInstallationAction = this.createAction<IRequestInstallationRequest,IRequestedInstallationDetailModel>('RequestInstallation', 'Request Installation');
	}
	
	readonly BeginDeleteAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly BeginInstallationAction: AppClientAction<IBeginInstallationRequest,IInstallationModel>;
	readonly BeginRequestedInstallationAction: AppClientAction<IRequestedInstallationIDRequest,IRequestedInstallationModel>;
	readonly BeginRequestedInstallationStepAction: AppClientAction<IBeginRequestedInstallationStepRequest,IRequestedInstallationStepModel>;
	readonly DeletedAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly GetInstallationActivitiesAction: AppClientAction<IGetInstallationActivitiesRequest,IInstallationActivitiesResult>;
	readonly GetInstallationDetailAction: AppClientAction<IInstallationIDRequest,IInstallationDetailModel>;
	readonly GetRequestedInstallationDetailAction: AppClientAction<IRequestedInstallationIDRequest,IRequestedInstallationDetailModel>;
	readonly Index: AppClientView<IInstallationQueryRequest>;
	readonly Installation: AppClientView<IInstallationViewRequest>;
	readonly InstalledAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly RequestDeleteAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly RequestedInstallationEndedAction: AppClientAction<IRequestedInstallationIDRequest,IEmptyActionResult>;
	readonly RequestedInstallationStepEndedAction: AppClientAction<IRequestedInstallationStepEndedRequest,IEmptyRequest>;
	readonly RequestInstallationAction: AppClientAction<IRequestInstallationRequest,IRequestedInstallationDetailModel>;
	
	BeginDelete(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginDeleteAction.execute(requestData, errorOptions || {});
	}
	BeginInstallation(requestData: IBeginInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginInstallationAction.execute(requestData, errorOptions || {});
	}
	BeginRequestedInstallation(requestData: IRequestedInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginRequestedInstallationAction.execute(requestData, errorOptions || {});
	}
	BeginRequestedInstallationStep(requestData: IBeginRequestedInstallationStepRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginRequestedInstallationStepAction.execute(requestData, errorOptions || {});
	}
	Deleted(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.DeletedAction.execute(requestData, errorOptions || {});
	}
	GetInstallationActivities(requestData: IGetInstallationActivitiesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationActivitiesAction.execute(requestData, errorOptions || {});
	}
	GetInstallationDetail(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationDetailAction.execute(requestData, errorOptions || {});
	}
	GetRequestedInstallationDetail(requestData: IRequestedInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetRequestedInstallationDetailAction.execute(requestData, errorOptions || {});
	}
	Installed(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.InstalledAction.execute(requestData, errorOptions || {});
	}
	RequestDelete(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.RequestDeleteAction.execute(requestData, errorOptions || {});
	}
	RequestedInstallationEnded(requestData: IRequestedInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.RequestedInstallationEndedAction.execute(requestData, errorOptions || {});
	}
	RequestedInstallationStepEnded(requestData: IRequestedInstallationStepEndedRequest, errorOptions?: IActionErrorOptions) {
		return this.RequestedInstallationStepEndedAction.execute(requestData, errorOptions || {});
	}
	RequestInstallation(requestData: IRequestInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.RequestInstallationAction.execute(requestData, errorOptions || {});
	}
}