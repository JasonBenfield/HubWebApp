// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Apps');
		this.GetAppDomainsAction = this.createAction<IEmptyRequest,IAppDomainModel[]>('GetAppDomains', 'Get App Domains');
		this.GetAppsAction = this.createAction<IEmptyRequest,IAppModel[]>('GetApps', 'Get Apps');
		this.Index = this.createView<IEmptyRequest>('Index');
	}
	
	readonly GetAppDomainsAction: AppClientAction<IEmptyRequest,IAppDomainModel[]>;
	readonly GetAppsAction: AppClientAction<IEmptyRequest,IAppModel[]>;
	readonly Index: AppClientView<IEmptyRequest>;
	
	GetAppDomains(errorOptions?: IActionErrorOptions) {
		return this.GetAppDomainsAction.execute({}, errorOptions || {});
	}
	GetApps(errorOptions?: IActionErrorOptions) {
		return this.GetAppsAction.execute({}, errorOptions || {});
	}
}