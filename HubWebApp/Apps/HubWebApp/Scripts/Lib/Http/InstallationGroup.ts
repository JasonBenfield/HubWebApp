// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class InstallationGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Installation');
		this.AddDeleteCommandAction = this.createAction<IInstallationIDRequest,IAppDeleteCommandDetailModel>('AddDeleteCommand', 'Add Delete Command');
		this.BeginDeleteAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('BeginDelete', 'Begin Delete');
		this.DeletedAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('Deleted', 'Deleted');
		this.GetInstallationDetailAction = this.createAction<IInstallationIDRequest,IInstallationDetailModel>('GetInstallationDetail', 'Get Installation Detail');
		this.Index = this.createView<IInstallationViewRequest>('Index');
		this.InstalledAction = this.createAction<IInstallationIDRequest,IEmptyActionResult>('Installed', 'Installed');
	}
	
	readonly AddDeleteCommandAction: AppClientAction<IInstallationIDRequest,IAppDeleteCommandDetailModel>;
	readonly BeginDeleteAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly DeletedAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	readonly GetInstallationDetailAction: AppClientAction<IInstallationIDRequest,IInstallationDetailModel>;
	readonly Index: AppClientView<IInstallationViewRequest>;
	readonly InstalledAction: AppClientAction<IInstallationIDRequest,IEmptyActionResult>;
	
	AddDeleteCommand(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.AddDeleteCommandAction.execute(requestData, errorOptions || {});
	}
	BeginDelete(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginDeleteAction.execute(requestData, errorOptions || {});
	}
	Deleted(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.DeletedAction.execute(requestData, errorOptions || {});
	}
	GetInstallationDetail(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallationDetailAction.execute(requestData, errorOptions || {});
	}
	Installed(requestData: IInstallationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.InstalledAction.execute(requestData, errorOptions || {});
	}
}