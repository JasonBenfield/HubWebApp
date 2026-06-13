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
		this.AddInstallCommandAction = this.createAction<IAddInstallCommandRequest,IAppInstallCommandDetailModel>('AddInstallCommand', 'Add Install Command');
		this.Index = this.createView<IInstallationQueryRequest>('Index');
	}
	
	readonly AddInstallCommandAction: AppClientAction<IAddInstallCommandRequest,IAppInstallCommandDetailModel>;
	readonly Index: AppClientView<IInstallationQueryRequest>;
	
	AddInstallCommand(requestData: IAddInstallCommandRequest, errorOptions?: IActionErrorOptions) {
		return this.AddInstallCommandAction.execute(requestData, errorOptions || {});
	}
}