// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class InstallationsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Installations');
		this.Index = this.createView<IInstallationQueryRequest>('Index');
		this.GetPendingDeletesAction = this.createAction<IGetPendingDeletesRequest,IAppVersionInstallationModel[]>('GetPendingDeletes', 'Get Pending Deletes');
		this.RequestDeleteAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('RequestDelete', 'Request Delete');
		this.BeginDeleteAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('BeginDelete', 'Begin Delete');
		this.DeletedAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('Deleted', 'Deleted');
	}
	
	readonly Index: AppApiView<IInstallationQueryRequest>;
	readonly GetPendingDeletesAction: AppApiAction<IGetPendingDeletesRequest,IAppVersionInstallationModel[]>;
	readonly RequestDeleteAction: AppApiAction<IGetInstallationRequest,IEmptyActionResult>;
	readonly BeginDeleteAction: AppApiAction<IGetInstallationRequest,IEmptyActionResult>;
	readonly DeletedAction: AppApiAction<IGetInstallationRequest,IEmptyActionResult>;
	
	GetPendingDeletes(model: IGetPendingDeletesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetPendingDeletesAction.execute(model, errorOptions || {});
	}
	RequestDelete(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.RequestDeleteAction.execute(model, errorOptions || {});
	}
	BeginDelete(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginDeleteAction.execute(model, errorOptions || {});
	}
	Deleted(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.DeletedAction.execute(model, errorOptions || {});
	}
}