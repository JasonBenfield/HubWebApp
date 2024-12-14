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
		this.BeginDeleteAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('BeginDelete', 'Begin Delete');
		this.DeletedAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('Deleted', 'Deleted');
		this.GetInstallationDetailAction = this.createAction<number,IInstallationDetailModel>('GetInstallationDetail', 'Get Installation Detail');
		this.GetPendingDeletesAction = this.createAction<IGetPendingDeletesRequest,IAppVersionInstallationModel[]>('GetPendingDeletes', 'Get Pending Deletes');
		this.Index = this.createView<IInstallationQueryRequest>('Index');
		this.InstallationView = this.createView<IInstallationViewRequest>('InstallationView');
		this.RequestDeleteAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('RequestDelete', 'Request Delete');
	}
	
	readonly BeginDeleteAction: AppClientAction<IGetInstallationRequest,IEmptyActionResult>;
	readonly DeletedAction: AppClientAction<IGetInstallationRequest,IEmptyActionResult>;
	readonly GetInstallationDetailAction: AppClientAction<number,IInstallationDetailModel>;
	readonly GetPendingDeletesAction: AppClientAction<IGetPendingDeletesRequest,IAppVersionInstallationModel[]>;
	readonly Index: AppClientView<IInstallationQueryRequest>;
	readonly InstallationView: AppClientView<IInstallationViewRequest>;
	readonly RequestDeleteAction: AppClientAction<IGetInstallationRequest,IEmptyActionResult>;
	
	BeginDelete(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginDeleteAction.execute(model, errorOptions || {});
	}
	Deleted(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.DeletedAction.execute(model, errorOptions || {});
	}
	GetInstallationDetail(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationDetailAction.execute(model, errorOptions || {});
	}
	GetPendingDeletes(model: IGetPendingDeletesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetPendingDeletesAction.execute(model, errorOptions || {});
	}
	RequestDelete(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.RequestDeleteAction.execute(model, errorOptions || {});
	}
}